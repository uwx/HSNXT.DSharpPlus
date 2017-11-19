/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using HSNXT.ComLib.Collections;

namespace HSNXT.ComLib.Models
{
    /// <summary>
    /// Collection of models.
    /// </summary>
    public class ModelContainer : GenericListBase<Model>
    {
        private readonly Dictionary<string, Model> _modelMap = new Dictionary<string, Model>();
        private IDictionary<string, List<Model>> _inheritanceChains;


        /// <summary>
        /// Map of all the models.
        /// </summary>
        public Dictionary<string, Model> ModelMap => _modelMap;


        /// <summary>
        /// Used to assign a collection of properties at once.
        /// </summary>
        public IList<Model> AllModels
        {
            get => this;
            set { Clear(); Add(value); }
        }


        private ModelBuilderSettings _settings = new ModelBuilderSettings();
        /// <summary>
        /// Settings for the model builder
        /// </summary>
        public ModelBuilderSettings Settings
        {
            get => _settings;
            set => _settings = value;
        }


        /// <summary>
        /// Additional settings to make it easy to add new settings dynamically.
        /// Also allows for inheritance.
        /// </summary>
        public Dictionary<string, object> ExtendedSettings { get; set; }


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            InitInheritanceChain();
        }


        #region List Members
        /// <summary>
        /// Add a single model to collection.
        /// </summary>
        /// <param name="model">The model to add.</param>
        public override void  Add(Model model)
        {
            base.Add(model);
            _modelMap[model.Name] = model;                
        }


        /// <summary>
        /// Add a collection of models.
        /// </summary>
        /// <param name="models">List of models to add.</param>
        public virtual void Add(IList<Model> models)
        {
            foreach (var model in models)
            {
                this.Add(model);
            }
        }


        /// <summary>
        /// Clear all the values.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            _modelMap.Clear();
        }


        /// <summary>
        /// Get / set by index.
        /// </summary>
        /// <param name="index">The index to model.</param>
        /// <returns>Model designated by the index.</returns>
        public override Model this[int index]
        {
	        get => base[index];
            set 
	        { 
                // Remove existing.
                var existing = base[index];

                // Remove from dictionary.
                _modelMap.Remove(existing.Name);

                // Add.
		        base[index] = value;
                _modelMap[value.Name] = value;
	        }
        }


        /// <summary>
        /// Get / set by name.
        /// </summary>
        /// <param name="name">The model name.</param>
        /// <returns>Model designated by name.</returns>
        public Model this[string name]
        {
            get => _modelMap[name];
            set
            {
                _modelMap[name] = value;
                Add(value);
            }
        }


        /// <summary>
        /// Determines if the model specified by modelname exists.
        /// </summary>
        /// <param name="modelName">The name of the model to check for.</param>
        /// <returns>True if the model exists.</returns>
        public bool Contains(string modelName)
        {
            return _modelMap.ContainsKey(modelName);
        }


        /// <summary>
        /// Remove the model from the container with the specified name.
        /// </summary>
        /// <param name="modelName">The name of the model to remove.</param>
        public void Remove(string modelName)
        {
            // Remove from the modellist.
            var model = (from m in this where string.Compare(m.Name, modelName, true) == 0 select m).First();
            Remove(model);   
        }


        /// <summary>
        /// Remove the model.
        /// </summary>
        /// <param name="model">The instance of the model to remove.</param>
        /// <returns>True if the model was successfully removed.</returns>
        public override bool Remove(Model model)
        {
            // Remove from the modelMap.
            _modelMap.Remove(model.Name);

            return base.Remove(model);
        }


        /// <summary>
        /// Remove model at the specified index.
        /// </summary>
        /// <param name="index">The index of the model to remove.</param>
        public override void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException("Index to remove in modelcontainer is out of range : " + index);

            // Remove from list and map.
            var model = this[index];
            this.RemoveAt(index);

            _modelMap.Remove(model.Name);
        }
        #endregion


        #region Iteration
        /// <summary>
        /// Iterates over the models and calls the action lamda if the predicate is true.
        /// </summary>
        /// <param name="condition">The predicate to determine if the action is to be called.</param>
        /// <param name="action">Action to be called.</param>
        public void Iterate(Predicate<Model> condition, Action<Model> action)
        {
            foreach (var model in this)
            {
                if (condition == null || (condition != null && condition(model) ))
                    action(model);
            }
        }


        /// <summary>
        /// Iterates over the models and calls the action lamda if the predicate is true.
        /// </summary>
        /// <param name="modelfilter">String with model filter.</param>
        /// <param name="condition">A predicate to determine if the action is to be called.</param>
        /// <param name="action">The action to be called.</param>
        public void Iterate(string modelfilter, Predicate<Model> condition, Action<Model> action)
        {
            var models = from m in this where m.Name.Contains(modelfilter) select m;

            if (models == null || models.Count() == 0) return;

            foreach (var model in models)
            {
                if (condition == null || (condition != null && condition(model)))
                    action(model);
            }
        }


        /// <summary>
        /// Iterates over the models and calls the action lamda if the predicate is true.
        /// </summary>
        /// <param name="condition">The predicate that deterimes if the action is to be called.</param>
        /// <param name="currentModel">The model to iterate over</param>
        /// <param name="inheritanceAction">The callback for the inheritance chain for this model.</param>
        /// <param name="includeAction">The call back for the included models</param>
        /// <param name="compositionAction">The call back for the composite models.</param>
        /// <param name="oneToOneRelationCallback">The call back for one to one relations on the model.</param>
        /// <param name="oneToManyRelationCallback">The call back for the one to many relations on the model.</param>
        public void Iterate(Predicate<Model> condition, 
                            Action<Model> currentModel,
                            Action<Model> inheritanceAction,
                            Action<Model, Include> includeAction,
                            Action<Model, Composition> compositionAction,
                            Action<Model, Relation> oneToOneRelationCallback,
                            Action<Model, Relation> oneToManyRelationCallback)
        {
            foreach (var model in this)
            {
                if (condition == null || (condition != null && condition(model)))
                {
                    currentModel(model);
                    Iterate(model.Name, inheritanceAction, includeAction, compositionAction, 
                        oneToOneRelationCallback, oneToManyRelationCallback);
                }
            }
        }


        /// <summary>
        /// Iterate over all the properties, includes, compositions of the model.
        /// </summary>
        /// <param name="modelName">The model to iterate over</param>
        /// <param name="inheritanceAction">The callback for the inheritance chain for this model.</param>
        /// <param name="includeAction">The call back for the included models</param>
        /// <param name="compositionAction">The call back for the composite models.</param>
        /// <param name="oneToOneRelationCallback">The call back for one to one relations on the model.</param>
        /// <param name="oneToManyRelationCallback">The call back for the one to many relations on the model.</param>
        public void Iterate(string modelName, 
                            Action<Model> inheritanceAction,
                            Action<Model, Include> includeAction, 
                            Action<Model, Composition> compositionAction,
                            Action<Model, Relation> oneToOneRelationCallback,
                            Action<Model, Relation> oneToManyRelationCallback)
        {
            var model = _modelMap[modelName];
            var inheritanceChain = InheritanceFor(model.Name);

            if (inheritanceAction != null)
            {
                // Build property mapping for each inherited model.
                foreach (var inheritedModel in inheritanceChain)
                    inheritanceAction(inheritedModel);
            }

            // Build property mapping for each inherited model.
            if (model.Includes != null && model.Includes.Count > 0 && includeAction != null)
            {
                foreach (var include in model.Includes)
                {
                    var includedModel = _modelMap[include.Name];
                    include.ModelRef = includedModel;
                    includeAction(includedModel, include);
                }
            }

            // Build property mapping for each Composed of model.
            if (model.ComposedOf != null && model.ComposedOf.Count > 0 && compositionAction != null)
            {
                foreach (var composition in model.ComposedOf)
                {
                    var composedModel = _modelMap[composition.Name];
                    composition.ModelRef = composedModel;
                    compositionAction(composedModel, composition);
                }
            }

            // One to One Relations
            if(model.OneToOne != null && model.OneToOne.Count > 0 && oneToOneRelationCallback != null)
            {
                 foreach(var rel in model.OneToOne)
                 {
                     var relationModel = _modelMap[rel.ModelName];                     
                     oneToOneRelationCallback(relationModel, rel);
                 }
            }

            // One to many Relations
            if (model.OneToMany != null && model.OneToMany.Count > 0 && oneToManyRelationCallback != null)
            {
                foreach (var rel in model.OneToMany)
                {
                    var relationModel = _modelMap[rel.ModelName];
                    oneToManyRelationCallback(relationModel, rel);
                }
            }
        }
        #endregion


        #region Inheritance
        /// <summary>
        /// Gets the Inheritance chain for the specified model name.
        /// </summary>
        /// <param name="modelName">The model name.</param>
        /// <returns>A list with the inheritance chain.</returns>
        public List<Model> InheritanceFor(string modelName)
        {
            if (!_inheritanceChains.ContainsKey(modelName))
                return null;

            return _inheritanceChains[modelName];
        }
        #endregion


        #region Private members
        /// <summary>
        /// Initialize by storing all the model chain inheritance paths for each model.
        /// </summary>
        private void InitInheritanceChain()
        {
            _inheritanceChains = new Dictionary<string, List<Model>>();

            foreach (var model in this)
            {
                // Create the database table for all the models.
                var modelChain = ModelUtils.GetModelInheritancePath(this, model.Name, true);
                _inheritanceChains[model.Name] = modelChain;
            }
        }
        #endregion
    }
}

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
using System.Collections.ObjectModel;
using System.Linq;

namespace HSNXT.ComLib.Environments
{            
    /// <summary>
    /// Environment service.
    /// </summary>
    public class EnvDef : IEnv
    {
        private EnvItem _selected;        
        private readonly List<EnvItem> _inheritedChainedEnvs = null;
        private IList<string> _availableEnvNames;
        private List<EnvItem> _availableEnvsList;
        private IDictionary<string, EnvItem> _availableEnvs;
        private string _inheritancePath;
        private string _refPath;


        /// <summary>
        /// Event handler when the active environment changes ( e.g. from Prod to Qa.
        /// </summary>
        public event EventHandler OnEnvironmentChange;


        /// <summary>
        /// Initializes this environment definition with the 
        /// collection of the environments(Prod, Qa, Dev, etc )
        /// and the selected environment ( Qa )
        /// </summary>
        /// <param name="selectedEnvironment"></param>
        /// <param name="available"></param>
        public EnvDef(string selectedEnvironment, List<EnvItem> available)
        {
            Init(selectedEnvironment, available);
        }
                

        #region Public Properties
        /// <summary>
        /// Name of the environment.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Return the current environment type.
        /// </summary>
        public EnvType EnvType => _selected.EnvType;


        /// <summary>
        /// The Top most environment in the inheritance chain ).
        /// E.g. If Prod inherits from Qa, Qa inherits from Dev
        /// This is the selected environment among Prod, Qa, Dev.
        /// </summary>
        public EnvItem Selected => _selected;


        /// <summary>
        /// Is production.
        /// </summary>
        public bool IsProd => _selected.EnvType == EnvType.Prod;


        /// <summary>
        /// Is Qa
        /// </summary>
        public bool IsQa => _selected.EnvType == EnvType.Qa;


        /// <summary>
        /// Is development.
        /// </summary>
        public bool IsDev => _selected.EnvType == EnvType.Dev;


        /// <summary>
        /// Is uat.
        /// </summary>
        public bool IsUat => _selected.EnvType == EnvType.Uat;


        /// <summary>
        /// Get the env entry associated with the name.
        /// </summary>
        /// <param name="envName"></param>
        /// <returns></returns>
        public EnvItem Get(string envName)
        {
            if (string.IsNullOrEmpty(envName)) return null;
            envName = envName.Trim().ToLower();
            return _availableEnvs[envName];
        }


        /// <summary>
        /// Get the env entry associated with the name.
        /// </summary>
        /// <param name="ndx"></param>
        /// <returns></returns>
        public EnvItem Get(int ndx)
        {
            return _availableEnvsList[ndx];
        }


        /// <summary>
        /// Get the number of available envs.
        /// </summary>
        public int Count => _availableEnvsList.Count;


        /// <summary>
        /// List of the inherited environments that make up this environment.
        /// </summary>
        public List<EnvItem> Inheritance => _inheritedChainedEnvs;


        /// <summary>
        /// Get the inheritance path, e.g. prod;qa;dev.
        /// </summary>
        public string Inherits => _inheritancePath;


        /// <summary>
        /// Get the reference path.
        /// </summary>
        public string RefPath => _refPath;


        /// <summary>
        /// Provides list of names of available environments than can be selected by user.
        /// </summary>
        public List<string> Available => EnvUtils.GetSelectableEnvironments(_availableEnvsList);

        #endregion


        #region Public Methods
        /// <summary>
        /// Get all the environments that are part of the EnvironmentContext
        /// for this definition.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<EnvItem> GetAll()
        {
            return new ReadOnlyCollection<EnvItem>(_availableEnvsList);
        }


        /// <summary>
        /// Initialize with list of available environments and the currently selected one.
        /// </summary>
        /// <param name="available"></param>
        /// <param name="selected"></param>
        public void Init(string selected, List<EnvItem> available)
        {
            _availableEnvsList = available;
            _availableEnvs = new Dictionary<string, EnvItem>();
            available.ForEach(env => _availableEnvs[env.Name.Trim().ToLower()] = env);
            Change(selected);
        }


        /// <summary>
        /// Set the current environment.
        /// </summary>
        /// <param name="envName"></param>
        public void Change(string envName)
        {
            envName = envName.ToLower().Trim();
            if (!_availableEnvs.ContainsKey(envName)) throw new ArgumentException("Environment " + envName + " does not exist.");

            // 1: Set the current environment and Name.
            _selected = _availableEnvs[envName];
            Name = envName;

            // 2. Get list of all available environment names.
            _availableEnvNames = (from env in _availableEnvsList select env.Name).ToList();

            // 3. Get the inheritance chain if applicable.
            //    e.g. if prod inherits from qa. List containing Prod,Qa
            _inheritancePath = EnvUtils.ConvertNestedToFlatInheritance(_selected, _availableEnvs);

            // 4. Get the ref path.
            //    If inherited, then combine all the ref paths.
            //    e.g. if prod(prod.config) inherits qa(qa.config) inherits dev(dev.config)
            //         refPath = "prod.config,qa.config,dev.config".
            if (string.IsNullOrEmpty(_selected.Inherits))
                _refPath = _selected.RefPath;
            else
            {
                var inheritanceChain = EnvUtils.LoadInheritance(_selected, _availableEnvs);
                _refPath = EnvUtils.CollectEnvironmentProps(inheritanceChain, ",", env => env.RefPath);                
            }

            // Notify.
            if (OnEnvironmentChange != null)
                OnEnvironmentChange(this, new EventArgs());
        }
        #endregion
    }

}

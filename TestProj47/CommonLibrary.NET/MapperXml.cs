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

using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using HSNXT.ComLib.Reflection;

namespace HSNXT.ComLib.MapperSupport
{
    /// <summary>
    /// Mapper for sourcing data from Ini files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MapperXml<T> : IMapper<T> where T : class, new()
    {
        private XmlDocument _doc;
        

        /// <summary>
        /// Get the supported format of the data source.
        /// </summary>
        public string SupportedFormat => "xml";


        /// <summary>
        /// Map the objects.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public IList<T> Map(object source, IErrors errors)
        {
            if (source == null || !(source is XmlDocument))
                return new List<T>();

            _doc = source as XmlDocument;
            return Map(errors);
        }


        /// <summary>
        /// Map the ini file
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public IList<T> Map(IErrors errors)
        {
            IList<T> items = new List<T>();

            // Check inputs.
            if (_doc == null || _doc.DocumentElement.ChildNodes == null 
                || _doc.DocumentElement.ChildNodes.Count == 0) 
                return items;

            var counter = 0;       
            var propMap = ReflectionUtils.GetPropertiesAsMap<T>(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, false);

            var rootNode = _doc.DocumentElement;
            var nodes = rootNode.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                // Represents single object.                                
                var item = new T();
                
                // Properties represented as xml child nodes.
                if(node.HasChildNodes)
                {   
                    foreach (XmlNode propNode in node.ChildNodes)
                    {
                        // Check if property exists
                        var propName = propNode.Name.ToLower();
                        if(propMap.ContainsKey(propName))
                        {
                            var prop = propMap[propName];
                            var val = propNode.InnerText;
                            MapperHelper.SetProperty(prop, item, counter, errors, val);
                        }
                    }
                }
                items.Add(item);
            }
            return items;
        }


        /// <summary>
        /// Map objects from the source and convert to list of type T. Collect errors into the IErrors.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public IList<T> MapFromFile(string filepath, IErrors errors)
        {
            _doc = new XmlDocument();
            _doc.Load(filepath);
            return Map(errors);
        }


        /// <summary>
        /// Map objects from the source and convert to list of type T. Collect errors into the IErrors.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public IList<T> MapFromText(string content, IErrors errors)
        {
            _doc = new XmlDocument();
            _doc.LoadXml(content);
            return Map(errors);
        }
    }
}

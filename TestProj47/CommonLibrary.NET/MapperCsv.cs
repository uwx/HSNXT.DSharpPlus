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
using System.Reflection;
using HSNXT.ComLib.CsvParse;
using HSNXT.ComLib.Reflection;

namespace HSNXT.ComLib.MapperSupport
{
    /// <summary>
    /// Mapper for sourcing data from Ini files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MapperCsv<T> : IMapper<T> where T : class, new()
    {
        private CsvDoc _doc;


        /// <summary>
        /// Initialize with csv doc.
        /// </summary>
        public MapperCsv()
        {
        }


        /// <summary>
        /// Initialize with csv doc.
        /// </summary>
        /// <param name="doc"></param>
        public MapperCsv(CsvDoc doc)
        {
            _doc = doc;
        }


        /// <summary>
        /// Get the supported format of the data source.
        /// </summary>
        public string SupportedFormat => "csv";


        /// <summary>
        /// Map the objects.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public IList<T> Map(object source, IErrors errors)
        {
            if (source == null || source.GetType() != typeof(CsvDoc))
                return new List<T>();

            _doc = source as CsvDoc;
            return Map(errors);
        }


        /// <summary>
        /// Map the ini file
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public IList<T> Map(IErrors errors)
        {
            IList<T> items = new List<T>();

            // Check inputs.
            if (_doc == null || _doc.Columns.Count == 0) return items;

            var propMap = ReflectionUtils.GetPropertiesAsMap<T>(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, false);
            var counter = 0;

            foreach(var record in _doc.Data)
            {
                // Represents single object.                                
                var item = new T();
                MapperHelper.MapTo(item, counter, propMap, record);
                items.Add(item);
                counter++;
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
            var doc = new CsvDoc(filepath, true);
            _doc = doc;
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
            var doc = new CsvDoc(content, false);
            _doc = doc;
            return Map(errors);
        }


        /// <summary>
        /// Map the items to a csv text.
        /// </summary>
        /// <param name="items">The items to map</param>
        /// <param name="excludeProps"></param>
        /// <param name="path"></param>
        /// <param name="errors">The errors to collect.</param>
        public void MapToFile(IList<T> items, IDictionary<string, string> excludeProps, string path, IErrors errors)
        {
            var props = new List<KeyValuePair<string, PropertyInfo>>();
            MapperHelper.GetProps(typeof(T), string.Empty, props);

            var records = new List<List<object>>();
            foreach (var item in items)
            {
                var rec = new List<object>();
                MapperHelper.MapFrom(item, excludeProps, props, (colname, val) =>
                {
                    if (val != null)
                        rec.Add(val);
                    else
                        rec.Add("");
                });
                records.Add(rec);
            }
            var cols = new List<string>();
            foreach(var pair in props) cols.Add(pair.Key);

            var writer = new CsvWriter(@"c:\temp\conference.csv", records, ",", cols, false, false, "\"", Environment.NewLine, false);
            writer.Write();
            writer.Dispose();
        }
    }
}

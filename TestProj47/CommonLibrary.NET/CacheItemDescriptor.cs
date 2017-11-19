
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

namespace HSNXT.ComLib.Caching
{
    /// <summary>
    /// Descriptor class to describe/display the
    /// contents of an item in the cache.
    /// </summary>
    public class CacheItemDescriptor
    {
        private readonly string _key;
        private readonly string _type;
        private readonly string _serializedData = string.Empty;
        

        /// <summary>
        /// Initialize the read-only properties.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public CacheItemDescriptor(string key, string type) : this ( key, type, string.Empty )
        {           
        }


        /// <summary>
        /// Initialize the read-only properties.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="serializedData"></param>
        public CacheItemDescriptor(string key, string type, string serializedData)
        {
            _key = key;
            _type = type;
            _serializedData = serializedData;
        }


        /// <summary>
        /// Key
        /// </summary>
        public string Key => _key;


        /// <summary>
        /// CacheItemType
        /// </summary>
        public string ItemType => _type;


        /// <summary>
        /// Get the serialzied data.
        /// </summary>
        public string Data => _serializedData;
    }

}

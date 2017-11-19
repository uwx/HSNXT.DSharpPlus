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

namespace HSNXT.ComLib.LocationSupport
{
    
    /// <summary>
    /// State 
    /// </summary>
    public class State : LocationCountryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        public State()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="countryId">The country id.</param>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="stateAbbr">The state abbr.</param>
        public State(string name, int countryId, string countryName, string stateAbbr)
        {
            this.Name = name;
            this.Abbreviation = stateAbbr;
            this.CountryId = countryId;
            this.CountryName = countryName;
            this.IsActive = true;
        } 
    }
}

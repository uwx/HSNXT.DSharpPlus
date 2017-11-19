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

namespace HSNXT.ComLib.Paging
{
    
    /// <summary>
    /// Page number to store page and css class for it.
    /// </summary>
    public class PageNumber
    {
        private readonly int _pageNumber;
        private readonly string _cssClass;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="number">Number of page.</param>
        /// <param name="cssClass">Css class.</param>
        public PageNumber(int number, string cssClass)
        {
            _pageNumber = number;
            _cssClass = cssClass;
        }


        /// <summary>
        /// page number
        /// </summary>
        public int Page => _pageNumber;


        /// <summary>
        /// Css class associated with this page.
        /// </summary>
        public string CssClass => _cssClass;
    }


    /// <summary>
    /// Pager for Ajax view model.
    /// </summary>
    public class PagerAjaxViewModel
    {
        /// <summary>
        /// The javascript object name associated with the pager.
        /// </summary>
        public string JSObjectName { get; set; }


        /// <summary>
        /// The size / number of items per page.
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// The css class used for the current page.
        /// </summary>
        public string CssClassCurrent { get; set; }


        /// <summary>
        /// The callback function to call when a page is selected.
        /// </summary>
        public string OnPageSelectedCallBack { get; set; }

    }
}
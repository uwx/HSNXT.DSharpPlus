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

using System.Threading;

namespace HSNXT.ComLib
{
    /// <summary>
    /// Rotates the pages.
    /// </summary>
    public class PageRotator
    {
        private int _currentPage;
        private int _totalRec;
        private int _recsPerPage;
        private bool _isMultipleEnabled;
        private int _totalPages;

        /// <summary>
        /// Consturctor.
        /// </summary>
        public PageRotator()
        {
            _totalRec = 0;
            _currentPage = 0;
        }


        /// <summary>
        /// total records
        /// </summary>
        public int TotalRec => _totalRec;


        /// <summary>
        /// Total pages.
        /// </summary>
        public int TotalPages => _totalPages;


        /// <summary>
        /// Are there any pages
        /// </summary>
        public bool HasPages => _totalPages > 0;


        /// <summary>
        /// records per page
        /// </summary>
        public int RecPerPage
        {
            get => _recsPerPage;
            set => _recsPerPage = value;
        }


        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage => _currentPage;


        /// <summary>
        /// Get/set whether multiple pages are enabled.
        /// </summary>
        public bool IsMultiplesEnabled
        {
            get => _isMultipleEnabled;
            set => _isMultipleEnabled = value;
        }


        /// <summary>
        /// Move to next page using new number of total records.
        /// </summary>
        /// <param name="totalRecords">Total number of records.</param>
        /// <returns>Record position after resetting the number of total records.</returns>
        public int MoveNext(int totalRecords)
        {
            _totalRec = totalRecords;
            return MoveNext();
        }


        /// <summary>
        /// Move to next page using existing total records.
        /// </summary>
        /// <returns>Current position.</returns>
        public int MoveNext()
        {
            // If there are no records. 
            if (_totalRec == 0)
            {
                _currentPage = 0;
                return _currentPage;
            }

            // Determine number of pages. 
            _totalPages = _totalRec / _recsPerPage;
            if (_totalPages == 0)
            {
                _currentPage = 0;
                return _currentPage;
            }
            // Determine if can move to next page. 
            if (CanMoveNext())
            {
                Increment();
            }
            else
            {
                _currentPage = 1;
            }

            return _currentPage;
        }


        private bool CanMoveNext()
        {
            return _currentPage < _totalPages;
        }


        private void Increment()
        {
            Interlocked.Increment(ref _currentPage);
        }
    } 
}

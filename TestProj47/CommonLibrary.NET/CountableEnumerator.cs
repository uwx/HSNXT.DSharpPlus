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

using System.Collections;
using System.Collections.Generic;

namespace HSNXT.GenericCode.Collections
{
    /// <summary>
    /// Extension to enumerator with extended methods to indicate if last or first item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountableEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// Indicates if the enumerator is empty ( it has 0 items ).
        /// </summary>
        bool IsEmpty { get; }


        /// <summary>
        /// Indicate if current item is first.
        /// </summary>
        /// <returns></returns>
        bool IsFirst();


        /// <summary>
        /// Indicate if current item is last.
        /// </summary>
        /// <returns></returns>
        bool IsLast();


        /// <summary>
        /// Get the index of the current item.
        /// </summary>
        /// <returns></returns>
        int CurrentIndex { get; }


        /// <summary>
        /// Get the total number of items.
        /// </summary>
        int Count { get; }
    }



    /// <summary>
    /// Extension to enumerator with extended methods to indicate if last or first item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CountableEnumerator<T> : ICountableEnumerator<T>
    {
        private readonly IList<T> _list;
        private int _currentIndex;


        /// <summary>
        /// Initialize the list.
        /// </summary>
        /// <param name="list"></param>
        public CountableEnumerator(IList<T> list)
        {
            _list = list;
            _currentIndex = -1;
        }


        #region IEnumerator<T> Members
        /// <summary>
        /// Get the current item.
        /// </summary>
        public T Current
        {
            get 
            { 
                if( _currentIndex > -1 && _currentIndex < _list.Count )
                    return _list[_currentIndex];

                return default;
            }
        }

        #endregion


        #region IDisposable Members
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {   
            // Nothing to dispose.
        }

        #endregion


        #region IEnumerator Members
        /// <summary>
        /// Get the current item in list.
        /// </summary>
        object IEnumerator.Current => _list[_currentIndex];


        /// <summary>
        /// Move to the next item.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_currentIndex < _list.Count - 1)
            {
                _currentIndex++;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Reset the iterator to first item.
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }
        #endregion


        #region ICountableEnumerator<T> Members
        /// <summary>
        /// Indicates if current item is first.
        /// </summary>
        /// <returns></returns>
        public bool IsFirst()
        {
            return _currentIndex == 0;
        }

        
        /// <summary>
        /// Indicates if current item is last.
        /// </summary>
        /// <returns></returns>
        public bool IsLast()
        {
            return _currentIndex == _list.Count - 1;
        }


        /// <summary>
        /// Get the index of the current item.
        /// </summary>
        /// <returns></returns>
        public int CurrentIndex => _currentIndex;


        /// <summary>
        /// Get the total items in the internal list.
        /// </summary>
        public int Count => _list.Count;


        /// <summary>
        /// Indicate if there are items to iterate over.
        /// </summary>
        public bool IsEmpty => (_list == null || _list.Count == 0);

        #endregion
    }
}

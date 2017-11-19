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

namespace HSNXT.ComLib.Collections
{
    /// <summary>
    /// Represents a set of indexes which will be iterated on.
    /// </summary>
    public class IndexSpan
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public IndexSpan(int start, int count)
        {
            StartIndex = start;
            Count = count;           
        }

        
        /// <summary>
        /// The start index.
        /// </summary>
        public int StartIndex;

        
        /// <summary>
        /// Number of items represented in this iteration.
        /// </summary>
        public int Count;


        /// <summary>
        /// The ending index, as calculated using the startIndex and count.
        /// </summary>
        public int EndIndex => (StartIndex + Count) - 1;
    }



    /// <summary>
    /// Class used to split indexes into iteration spans.
    /// </summary>
    public class IterationSplitter
    {
        /// <summary>
        /// Calculate how many items there will be in an iteration.
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="numberOfIterations"></param>
        /// <returns></returns>
        public static int CalculateItemsPerIteration(double totalCount, int numberOfIterations)
        {
            var itemsPerColumnD = totalCount / numberOfIterations;
            var itemsPerColumn = Convert.ToInt32(Math.Ceiling(itemsPerColumnD));
            return itemsPerColumn;
        }


        /// <summary>
        /// Splits the iteratons into parts(spans).
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="numberOfIterations"></param>
        /// <returns></returns>
        public static List<IndexSpan> SplitIterations(int totalCount, int numberOfIterations)
        {
            var itemsPerIteration = CalculateItemsPerIteration(totalCount, numberOfIterations);
            return Split(totalCount, itemsPerIteration);
        }        


        /// <summary>
        /// Splits the iteratons into parts(spans).
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="itemsPerIteration"></param>
        /// <returns></returns>
        public static List<IndexSpan> Split(int totalCount, int itemsPerIteration)
        {
            //Requirement.IsTrue(totalCount != 0, "Invalid indexes span specified.");
            if (totalCount == 0)
                return new List<IndexSpan>();

            var iterationSpans = new List<IndexSpan>();

            // Handle case where 5total = 5 per iteration.
            if (totalCount == itemsPerIteration)
            {
                iterationSpans.Add(new IndexSpan(0, itemsPerIteration));
                return iterationSpans;
            }

            // Handle case where 5total = 8 per iteration
            if (totalCount < itemsPerIteration)
            {
                iterationSpans.Add(new IndexSpan(0, totalCount));
                return iterationSpans;
            }

            // Now create the iterations.
            var nextStartingIndex = 0;
            var count = itemsPerIteration;
            var totalCountProcessed = 0;
            var leftOver = 0;

            while(totalCountProcessed < totalCount)
            {
                var span = new IndexSpan(nextStartingIndex, count);
                iterationSpans.Add(span);

                // Used to break the loop. We've finished all the items.
                totalCountProcessed += count;

                // Now calculate the next starting index.
                nextStartingIndex = span.EndIndex + 1;

                // Now determine left over items
                leftOver = totalCount - totalCountProcessed;
                if (leftOver >= itemsPerIteration)
                    count = itemsPerIteration;
                else
                    count = leftOver;                
            }

            return iterationSpans;
        }
    }

}

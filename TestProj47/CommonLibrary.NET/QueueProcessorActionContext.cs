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

namespace HSNXT.ComLib.Queue
{
    /// <summary>
    /// Action context when performing various actions on the processing queue.
    /// </summary>
    public class QueueProcessActionContext
    {
        /// <summary>
        /// Gets or sets the size of the batch.
        /// </summary>
        /// <value>The size of the batch.</value>
        public int BatchSize { get; set; }             
    }   
}

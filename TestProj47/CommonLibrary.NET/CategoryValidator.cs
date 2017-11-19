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

using HSNXT.ComLib.ValidationSupport;

namespace HSNXT.ComLib.Categories
{
    /// <summary> 
    /// Category validator. 
    /// </summary> 
    public class CategoryValidator : Validator
    {
        private readonly Category _category;


        /// <summary>
        /// Initializer.
        /// </summary>
        /// <param name="category"></param>
        public CategoryValidator(Category category)
        {
            _category = category;
        }


        #region IValidator<Category> Members
        /// <summary> 
        /// Validate the category. 
        /// </summary> 
        /// <param name="validationEvent"></param>
        /// <returns></returns> 
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            var target = validationEvent.Target;
            var results = validationEvent.Results;
            var useTarget = validationEvent.Target != null;
            
            var initialErrorCount = results.Count;
            var category = useTarget ? (Category)target : _category;
            ValidationUtils.Validate(string.IsNullOrEmpty(category.Name), results, "Title", "Category title can not be empty");
            return initialErrorCount == results.Count;
        }
        #endregion
    }
}

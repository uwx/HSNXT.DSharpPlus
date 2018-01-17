#if NetFX
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
using HSNXT.ComLib.Entities;

namespace HSNXT.ComLib.ImportExport
{

    /// <summary>
    /// Implementation of mport/export service using entity repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImportExportServiceEntityBased<T> : ImportExportService<T>, IImportExportService<T> where T: class, new()
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ImportExportServiceEntityBased()
        {
            Init(null, new[]{ "csv", "ini" });
        }


        /// <summary>
        /// Initialize with the supported formats.
        /// </summary>
        /// <param name="formatsDelimited"></param>
        public ImportExportServiceEntityBased(string formatsDelimited)
        {
            Init(null, formatsDelimited);
        }


        /// <summary>
        /// Initialize with the supported formats.
        /// </summary>
        /// <param name="formats"></param>
        public ImportExportServiceEntityBased(string[] formats)
        {
            Init(null, formats);
        }


        /// <summary>
        /// Import items into the entity repository.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public override BoolErrorsItem<IList<T>> Import(IList<T> items)
        {
            // Check for overriden handler.
            if (_importHandler != null) return base.Import(items);

            // Get the service for the enitity of type T.
            var service = EntityRegistration.GetService<T>();
            service.Create(items);
            var success = true;
            IValidationResults errors = new ValidationResults();
            var ndx = 1;
            foreach (var item in items)
            {
                var entity = item as IEntity;
                if (entity.Errors.HasAny)
                {
                    success = false;
                    // Now copy over the errors w/ context information( e.g. 1st entity imported ).
                    errors.Add("Item # [" + ndx + "] : ");
                    entity.Errors.EachFull(err => errors.Add(err));                    
                }
                ndx++;
            }
            var fullError = errors.Message();
            return new BoolErrorsItem<IList<T>>(items, success, fullError, errors);
        }


        /// <summary>
        /// Gets the total count of the items that can be exported.
        /// </summary>
        /// <returns></returns>
        public override int TotalExportable()
        {
            // Check for overriden handler.
            if (_totalHandler != null) return _totalHandler();

            var repo = EntityRegistration.GetRepository<T>();
            return repo.Count();
        }


        /// <summary>
        /// Exports items in a batch/page.
        /// </summary>
        /// <returns></returns>
        public override BoolMessageItem<IList<T>> Export(int page, int pageSize)
        {
            // Check for overriden handler.
            if (_exportPageHandler != null) return base.Export(page, pageSize);

            var service = EntityRegistration.GetService<T>();
            IList<T> items = service.Get(1, pageSize);
            return new BoolMessageItem<IList<T>>(items, true, string.Empty);
        }
    }
}
#endif
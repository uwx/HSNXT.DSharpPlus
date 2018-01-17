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

using System.IO;
using System.Web;
using System.Xml;

namespace HSNXT.CommonLibrary.Web.UI
{
    class Helpers
    {
        // Replicates the functionality of the internal Page.EnableLegacyRendering property
        public static bool EnableLegacyRendering()
        {
            // 2007-10-02: The following commented out code will NOT work in Medium Trust environments
            //Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //XhtmlConformanceSection xhtmlSection = (XhtmlConformanceSection) cfg.GetSection("system.web/xhtmlConformance");

            //return xhtmlSection.Mode == XhtmlConformanceMode.Legacy;


            // 2007-10-02: The following work around, provided by Michael Tobisch, works in
            //              Medium Trust by directly reading the Web.config file as XML.
            bool result;

            try
            {
                var webConfigFile = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config");
                var webConfigReader = new XmlTextReader(new StreamReader(webConfigFile));
                result = ((webConfigReader.ReadToFollowing("xhtmlConformance")) && (webConfigReader.GetAttribute("mode") == "Legacy"));
                webConfigReader.Close();
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
#endif
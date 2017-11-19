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

using System.Text.RegularExpressions;

namespace HSNXT.ComLib.Subs
{
    /// <summary>
    /// Utility methods for the subs.
    /// </summary>
    public class SubstitutionUtils
    {
        /// <summary>
        /// parse the substitution.
        /// </summary>
        /// <param name="funcCall">Substitution function.</param>
        /// <param name="subContainer">Substitution service.</param>
        /// <returns>Result of substitution.</returns>
        public static Substitution Parse(string funcCall, SubstitutionService subContainer)
        {
            var match = Regex.Match(funcCall, @"\$\{(?<name>[\S]+)\}");
            if (match == null || !match.Success)
                return new Substitution(string.Empty, funcCall, false, null);

            var name = match.Groups["name"].Value;
            var ndxDot = name.IndexOf(".");

            // No "." in func name.
            // e.g. Such as ${today} instead of ${datetime.today}.
            if (ndxDot < 0)
            {
                if (!subContainer._groups[""].ContainsKey(name))
                    return new Substitution(string.Empty, name, false, null);
                
                return new Substitution(string.Empty, name, true, null);
            }

            // Get the group name.
            var groupName = name.Substring(0, ndxDot);
            var funcName = name.Substring(ndxDot + 1);

            // Does the func call exist ?
            if (!subContainer._groups.ContainsKey(groupName) || !subContainer._groups[groupName].ContainsKey(funcName))
                return new Substitution(string.Empty, funcCall, false, null);

            // Not handling args for the time being.
            return new Substitution(groupName, funcName, true, null);
        }


        /// <summary>
        /// Evaluate the funcall.
        /// </summary>
        /// <param name="sub">Instance of substitution.</param>
        /// <param name="subContainer">Substitution service.</param>
        /// <returns>Result of substitution.</returns>
        public static string Eval(Substitution sub, SubstitutionService subContainer)
        {
            if (!sub.IsValid) return sub.FuncName;

            return subContainer._groups[sub.Groupname][sub.FuncName](sub.FuncName);
        }
    }
}

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
using System.Text;
using System.Text.RegularExpressions;

namespace HSNXT.ComLib.Web
{
    /// <summary>
    /// Url optimizer utility class.
    /// </summary>
    public class UrlSeoUtils
    {
        /// <summary>
        /// Map of invalid characters that should not appear in 
        /// an SEO optimized url.
        /// </summary>
        private static readonly IDictionary<char, bool> _invalidChars;


        /// <summary>
        /// String containing each invalid character.
        /// </summary>
        public const string InvalidSeoUrlChars = @"$%#@!*?;:~`_+=()[]{}|\'<>,/^&"".";


        /// <summary>
        /// Initialize the list of mappings.
        /// </summary>
        static UrlSeoUtils()
        {
            var invalidChars = InvalidSeoUrlChars.ToCharArray();
            _invalidChars = new Dictionary<char, bool>();

            // Store each invalid char.
            foreach (var invalidChar in invalidChars)
            {
                _invalidChars.Add(invalidChar, true);
            }
        }


        /// <summary>
        /// Same as BuildValidUrl but uses RegEx and is much, much slower.
        /// </summary>
        /// <param name="title">URL.</param>
        /// <returns>Valid URL.</returns>
        public static string BuildValidUrlUsingRegex(string title)
        {
            //Replace . with – dash
            var newTitle = Regex.Replace(title.Trim(), @"\W", "-");

            //Replace multiple “-” dashes with single “-” dash
            newTitle = Regex.Replace(newTitle, @"55+", "-").Trim('-');

            return newTitle.ToLower();
        }


        /// <summary>
        /// Generates an SEO optimized url.
        /// </summary>
        /// <param name="title">URL.</param>
        /// <returns>Valid URL.</returns>
        public static string BuildValidUrl(string title)
        {
            // Validate.
            if (string.IsNullOrEmpty(title)) return string.Empty;

            // Get all lowercase without spaces.
            title = title.ToLower().Trim();

            var buffer = new StringBuilder();
            char currentChar, lastAddedChar = 'a';

            // Now go through each character.
            for (var ndx = 0; ndx < title.Length; ndx++)
            {
                currentChar = title[ndx];

                // Invalid char ? Go to next one.
                if (_invalidChars.ContainsKey(currentChar))
                {
                    continue;
                }

                // Handle spaces or dashes.
                if (currentChar == ' ' || currentChar == '-')
                {
                    // Only add if the previous char was not a space or dash (' ', '-').
                    // This is to avoid having multiple "-" dashes in the url.
                    if (lastAddedChar != ' ' && lastAddedChar != '-')
                    {
                        buffer.Append('-');
                        lastAddedChar = '-';
                    }
                }
                else
                {
                    buffer.Append(currentChar);
                    lastAddedChar = currentChar;
                }
            }
            return buffer.ToString();
        }
    }
}

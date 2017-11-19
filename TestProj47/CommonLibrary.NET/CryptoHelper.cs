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
using System.Security.Cryptography;
using System.Text;

namespace HSNXT.ComLib.Cryptography
{

    /// <summary>
    /// Simple Cryptographic Services
    /// </summary>
    public class CryptographyUtils
    {
        /// <summary>
        /// Generates a cryptographic Hash Key for the provided text data.
        /// Basically a digital fingerprint
        /// </summary>
        /// <param name="dataToHash">text to hash</param>
        /// <param name="hashAlgorithm">e.g. new MD5CryptoServiceProvider();</param>
        /// <returns>Unique hash representing string</returns>
        public static string Encrypt(HashAlgorithm hashAlgorithm, String dataToHash)
        {
            var hexResult = "";
            var tabStringHex = new string[16];

            var data = Encoding.ASCII.GetBytes(dataToHash);
            var result = hashAlgorithm.ComputeHash(data);
            for (var i = 0; i < result.Length; i++)
            {
                tabStringHex[i] = (result[i]).ToString("x");
                hexResult += tabStringHex[i];
            }
            return hexResult;
        }


        /// <summary>
        /// Generates a cryptographic Hash Key for the provided text data.
        /// Basically a digital fingerprint
        /// </summary>
        /// <param name="hashAlgorithm">e.g. new MD5CryptoServiceProvider();</param>
        /// <param name="hashedText"></param>
        /// <param name="unhashedText"></param>
        /// <returns>Unique hash representing string</returns>
        public static bool IsHashMatch(HashAlgorithm hashAlgorithm, string hashedText, string unhashedText)
        {
            var hashedTextToCompare = Encrypt(hashAlgorithm, unhashedText);
            return (string.Compare(hashedText, hashedTextToCompare, false) == 0);
        }


        /// <summary>
        /// Encrypts text with Triple DES encryption using the supplied key
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="plaintext">The text to encrypt</param>
        /// <param name="key">Key to use for encryption</param>
        /// <returns>The encrypted string represented as base 64 text</returns>
        public static string Encrypt(SymmetricAlgorithm algorithm, string plaintext, string key)
        {
            //TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
            var hashMD5 = new MD5CryptoServiceProvider();
            algorithm.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            algorithm.Mode = CipherMode.ECB;
            var transformer = algorithm.CreateEncryptor();
            var Buffer = ASCIIEncoding.ASCII.GetBytes(plaintext);
            return Convert.ToBase64String(transformer.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        /// <summary>
        /// Decrypts supplied Triple DES encrypted text using the supplied key
        /// </summary>
        /// <param name="algorithm">The algorithm to use for decryption.</param>
        /// <param name="base64Text">Triple DES encrypted base64 text</param>
        /// <param name="key">Decryption Key</param>
        /// <returns>The decrypted string</returns>
        public static string Decrypt(SymmetricAlgorithm algorithm, string base64Text, string key)
        {
            var hashMD5 = new MD5CryptoServiceProvider();
            algorithm.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            algorithm.Mode = CipherMode.ECB;
            var transformer = algorithm.CreateDecryptor();
            var Buffer = Convert.FromBase64String(base64Text);
            return ASCIIEncoding.ASCII.GetString( transformer.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        /// <summary>
        /// Create new instance of symmetric algorithm using reflection by
        /// supplying the typename.
        /// </summary>
        /// <param name="fullyQualifiedTypeName"></param>
        /// <returns></returns>
        public static T CreateAlgo<T>(string fullyQualifiedTypeName) where T : class
        {
            var algo = Activator.CreateInstance(Type.GetType(fullyQualifiedTypeName));
            return algo as T;
        }


        /// <summary>
        /// Create triple des symmetric algorithm.
        /// </summary>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmAlgoTripleDes()
        {
            return new TripleDESCryptoServiceProvider();
        }


        /// <summary>
        /// Create MD5 hash algorithm.
        /// </summary>
        /// <returns></returns>
        public static HashAlgorithm CreateHashAlgoMd5()
        {
            return new MD5CryptoServiceProvider();
        }
    }
}

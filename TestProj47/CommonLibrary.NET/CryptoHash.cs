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

namespace HSNXT.ComLib.Cryptography
{

    /// <summary>
    /// Cryptography service to encrypt and decrypt strings.
    /// </summary>
    public class CryptoHash : ICrypto
	{
        /// <summary>
        /// Settings for encryption
        /// </summary>
        protected CryptoConfig _encryptionOptions;


        /// <summary>
        /// Hashing algorithm
        /// </summary>
        protected HashAlgorithm _algorithm;


        #region Constructors
        /// <summary>
        /// Default options
        /// </summary>
        public CryptoHash()
        {
            _encryptionOptions = new CryptoConfig();
            _algorithm = CryptographyUtils.CreateHashAlgoMd5();
        }


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="algorithm"></param>
        public CryptoHash(string key, HashAlgorithm algorithm)
        {
            _encryptionOptions = new CryptoConfig(true, key);
            _algorithm = algorithm;
        }


        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="algorithm"></param>
        public CryptoHash(CryptoConfig options, HashAlgorithm algorithm)
        {
            _encryptionOptions = options;
            _algorithm = algorithm;
        }
        #endregion


        /// <summary>
        /// Options for encryption.
        /// </summary>
        /// <value></value>
        public CryptoConfig Settings => _encryptionOptions;


		/// <summary>
        /// Set the creator for the algorithm.
        /// </summary>
        /// <param name="algorithm"></param>
        public void SetAlgorithm(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }


		/// <summary>
		/// Encrypts the plaintext using an internal private key.
		/// </summary>
		/// <param name="plaintext">The text to encrypt.</param>
		/// <returns>An encrypted string in base64 format.</returns>
		public string Encrypt( string plaintext )
		{
            if(!_encryptionOptions.Encrypt)
                return plaintext;

            var base64Text = CryptographyUtils.Encrypt(_algorithm, plaintext);
			return base64Text;
		}


		/// <summary>
		/// Decrypts the base64key using an internal private key.
		/// </summary>
		/// <param name="base64Text">The encrypted string in base64 format.</param>
		/// <returns>The plaintext string.</returns>
        public string Decrypt( string base64Text )
		{
            throw new NotSupportedException("Can not decrypt hash algorithm.");            
		}


        /// <summary>
        /// Determine if encrypted text can be matched to unencrypted text.
        /// </summary>
        /// <param name="encrypted"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public bool IsMatch(string encrypted, string plainText)
        {
            var encrypted2 = Encrypt(plainText);
            return string.Compare(encrypted, encrypted2, false) == 0;
        }
	}
}

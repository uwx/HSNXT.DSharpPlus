// Decompiled with JetBrains decompiler
// Type: TestProj47.SecurityExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System.Security.Cryptography;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>
        /// Encrypts data with the System.Security.Cryptography.RSA algorithm.
        /// </summary>
        /// <param name="source">The data to be encrypted.</param>
        /// <param name="key">Represents the key container name for System.Security.Cryptography.CspParameters.</param>
        /// <param name="fOAEP">true to perform direct RSA encryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
        /// <returns>The encrypted data.</returns>
        public static byte[] EncryptRSA(this byte[] source, string key = null, bool fOAEP = false)
        {
            if (source == null)
                return null;
            using (var cryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
            {
                KeyContainerName = key ?? string.Empty
            }))
                return cryptoServiceProvider.Encrypt(source, fOAEP);
        }

        /// <summary>
        /// Decrypts data with the System.Security.Cryptography.RSA algorithm.
        /// </summary>
        /// <param name="source">The data to be decrypted.</param>
        /// <param name="key">Represents the key container name for System.Security.Cryptography.CspParameters.</param>
        /// <param name="fOAEP">true to perform direct RSA encryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding.</param>
        /// <returns>The decrypted data, which is the original data before encryption.</returns>
        public static byte[] DecryptRSA(this byte[] source, string key = null, bool fOAEP = false)
        {
            if (source == null)
                return null;
            using (var cryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
            {
                KeyContainerName = key ?? string.Empty
            }))
                return cryptoServiceProvider.Decrypt(source, fOAEP);
        }

        /// <summary>
        /// Computes the hash value of the specified byte array using SHA512Managed algorithm, and signs the resulting hash value.
        /// </summary>
        /// <param name="source">The input data for which to compute the hash and to be signed.</param>
        /// <param name="privateKey">The private key for System.Security.Cryptography.RSA.</param>
        /// <returns>The System.Security.Cryptography.RSA signature for the specified data.</returns>
        public static byte[] SignDataRSA(this byte[] source, RSAParameters privateKey)
        {
            using (var cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.ImportParameters(privateKey);
                using (var shA512Managed = new SHA512Managed())
                    return cryptoServiceProvider.SignData(source, shA512Managed);
            }
        }

        /// <summary>
        /// Verifies the specified signature data by comparing it to the signature computed for the specified data.
        /// </summary>
        /// <param name="source">The original data to be verified.</param>
        /// <param name="signature">The signature data to be verified.</param>
        /// <param name="publicKey">The public key for System.Security.Cryptography.RSA.</param>
        /// <returns>true if the signature verifies as valid; otherwise, false.</returns>
        public static bool VerifyDataRSA(this byte[] source, byte[] signature, RSAParameters publicKey)
        {
            using (var cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.ImportParameters(publicKey);
                using (var shA512Managed = new SHA512Managed())
                    return cryptoServiceProvider.VerifyData(source, shA512Managed, signature);
            }
        }

        /// <summary>
        /// Computes the hash value of the specified byte array using SHA512Managed algorithm, and signs the resulting hash value.
        /// </summary>
        /// <param name="source">The input data for which to compute the hash and to be signed.</param>
        /// <param name="privateKey">A byte array that represents an RSA private key blob.</param>
        /// <returns>The System.Security.Cryptography.RSA signature for the specified data.</returns>
        public static byte[] SignDataRSA(this byte[] source, byte[] privateKey)
        {
            using (var cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.ImportCspBlob(privateKey);
                using (var shA512Managed = new SHA512Managed())
                    return cryptoServiceProvider.SignData(source, shA512Managed);
            }
        }

        /// <summary>
        /// Verifies the specified signature data by comparing it to the signature computed for the specified data.
        /// </summary>
        /// <param name="source">The original data to be verified.</param>
        /// <param name="signature">The signature data to be verified.</param>
        /// <param name="publicKey">A byte array that represents an RSA public key blob.</param>
        /// <returns>true if the signature verifies as valid; otherwise, false.</returns>
        public static bool VerifyDataRSA(this byte[] source, byte[] signature, byte[] publicKey)
        {
            using (var cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.ImportCspBlob(publicKey);
                using (var shA512Managed = new SHA512Managed())
                    return cryptoServiceProvider.VerifyData(source, shA512Managed, signature);
            }
        }

        /// <summary>
        /// Computes the hash value for the specified byte array with System.Security.Cryptography.MD5 hash algorithm.
        /// </summary>
        /// <param name="source">The input to compute the hash code for.</param>
        /// <returns>The computed hash code.</returns>
        public static byte[] HashMD5(this byte[] source)
        {
            using (var md5 = MD5.Create())
                return md5.ComputeHash(source);
        }
    }
}
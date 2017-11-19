using System;
using System.Security.Cryptography;
using HSNXT.ComLib.Application;
using HSNXT.ComLib.Cryptography;
using HSNXT.ComLib.Cryptography.DES;
using TripleDES = HSNXT.ComLib.Cryptography.DES.TripleDES;
//<doc:using>
//</doc:using>


namespace HSNXT.ComLib.Samples
{	
    /// <summary>
    /// Example for the Cryptography namespace.
    /// </summary>
    public class Example_Cryptography : App
    {
		/// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            //<doc:example>
			// 1. Encrypt using default provider. ( Symmetric TripleDes )
            var plainText = "www.knowledgedrink.com";
            var encrypted = Crypto.Encrypt(plainText);
            var decrypted = Crypto.Decrypt(encrypted);

            Console.WriteLine("====================================================");
            Console.WriteLine("CRYPTOGRAPHY ");
            Console.WriteLine("Encrypted : " + plainText + " to " + encrypted);
            Console.WriteLine("Decrypted : " + encrypted + " to " + decrypted);
            Console.WriteLine(Environment.NewLine);

            // 2. Use non-static encryption provider.
            ICrypto crypto = new CryptoHash("commonlib.net", new MD5CryptoServiceProvider());
            var hashed = crypto.Encrypt("my baby - 2002 honda accord ex coupe");
            Console.WriteLine(hashed);

            // 3. Change the crypto provider on the static helper.
            ICrypto crypto2 = new CryptoSym("new key", new TripleDESCryptoServiceProvider());
            Crypto.Init(crypto2);
            var encryptedWithNewKey = Crypto.Encrypt("www.knowledgedrink.com");
            Console.WriteLine("Encrypted text : using old key - {0}, using new key - {1}", encrypted, encryptedWithNewKey);

            // 4. Generate the check value of a 3DES key by encrypting 16 hexadecimal zeroes.
            var randomKey = new DESKey(DesKeyType.TripleLength);
            var keyCheckValue = TripleDES.Encrypt(randomKey, "0000000000000000");
            Console.WriteLine("3DES key: {0} with check value {1}", randomKey, keyCheckValue);
			
			//</doc:example>
            return BoolMessageItem.True;
        }		
    }
}

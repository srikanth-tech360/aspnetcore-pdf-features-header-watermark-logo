using System.Security.Cryptography;
using System.Text;


namespace RotativaPDFDemo.Utility{
    

    public static class PdfEncryptionHelper
    {
        private static readonly string EncryptionKey = "123456789"; // 16/24/32 chars for AES

        public static string Encrypt(string text)
        {     
            
            //AES-256
       
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;
            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor();
            var plainText = Encoding.UTF8.GetBytes(text);
            var cipherBytes = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

            // Combine IV + Cipher text
            var combined = new byte[iv.Length + cipherBytes.Length];
            Buffer.BlockCopy(iv, 0, combined, 0, iv.Length);
            Buffer.BlockCopy(cipherBytes, 0, combined, iv.Length, cipherBytes.Length);

            return Convert.ToBase64String(combined);
       
        }

        public static string EncryptShort(string text)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(16).Substring(0, 16)); // AES-128
            aes.IV = new byte[16]; // ⚠️ Fixed IV (can be derived later too)

            var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(text);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(cipherBytes); // No IV embedded, shorter
        }

        public static string DecryptShort(string cipherTextBase64)
        {
            using var aes = Aes.Create();

            // AES-128 key (ensure 16 bytes)
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(16).Substring(0, 16));

            // Static IV (must match the Encrypt method)
            aes.IV = new byte[16]; // All zeros, for example

            using var decryptor = aes.CreateDecryptor();

            var cipherBytes = Convert.FromBase64String(cipherTextBase64);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }



        public static string Decrypt(string encryptedText)
        {           
            var fullCipher = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(decryptedBytes);        
        }
    }

}



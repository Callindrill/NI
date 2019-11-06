using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class Symmetric
    {
        private readonly RijndaelManaged _provider;
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public Symmetric(string key = null, string iv = null)
        {
            _provider = new RijndaelManaged();
            if (key != null || iv != null) {
                _key = Convert.FromBase64String(key);
                _iv = Convert.FromBase64String(iv);
            } 
            else {
                using (var rngProvider = new RNGCryptoServiceProvider()) {
                    _key = new byte[_provider.KeySize / 8];
                    rngProvider.GetBytes(_key);
                    _iv = new byte[_provider.BlockSize / 8];
                    rngProvider.GetBytes(_iv);
                }
            }
        }

        public string Encrypt(string plaintext)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] cipherBytes;

            using (var buf = new MemoryStream()) {
                using (var stream = new CryptoStream(buf, _provider.CreateEncryptor(_key, _iv), CryptoStreamMode.Write)) {
                    stream.Write(clearBytes, 0, clearBytes.Length);
                    stream.FlushFinalBlock();
                }
                cipherBytes = buf.ToArray();
            }
            return Convert.ToBase64String(cipherBytes);
        }

        public string Decrypt(string ciphertext)
        {
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            byte[] clearBytes;

            using (var buf = new MemoryStream()) {
                using (var stream = new CryptoStream(buf, _provider.CreateDecryptor(_key, _iv), CryptoStreamMode.Write)) {
                    stream.Write(cipherBytes, 0, cipherBytes.Length);
                    stream.FlushFinalBlock();
                }
                clearBytes = buf.ToArray();
            }
            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}

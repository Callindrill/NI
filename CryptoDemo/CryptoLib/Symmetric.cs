using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class Symmetric
    {
        private readonly RijndaelManaged _provider;

        public byte[] Key { get; }
        public byte[] Iv { get; }

        public Symmetric(string key = null, string iv = null)
        {
            _provider = new RijndaelManaged();
            if (key != null || iv != null) {
                Key = Convert.FromBase64String(key);
                Iv = Convert.FromBase64String(iv);
            } 
            else {
                using (var rngProvider = new RNGCryptoServiceProvider()) {
                    Key = new byte[_provider.KeySize / 8];
                    rngProvider.GetBytes(Key);
                    Iv = new byte[_provider.BlockSize / 8];
                    rngProvider.GetBytes(Iv);
                }
            }
        }

        public string Encrypt(string plaintext)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] cipherBytes;

            using (var buf = new MemoryStream()) {
                using (var stream = new CryptoStream(buf, _provider.CreateEncryptor(Key, Iv), CryptoStreamMode.Write)) {
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
                using (var stream = new CryptoStream(buf, _provider.CreateDecryptor(Key, Iv), CryptoStreamMode.Write)) {
                    stream.Write(cipherBytes, 0, cipherBytes.Length);
                    stream.FlushFinalBlock();
                }
                clearBytes = buf.ToArray();
            }
            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}

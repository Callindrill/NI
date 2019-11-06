using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class Asymmetric
    {
        private RSACryptoServiceProvider _provider;
        private readonly string _publicKey;
        private readonly string _privateKey;

        public Asymmetric(string publicKey = null, string privateKey = null)
        {
            _provider = new RSACryptoServiceProvider();
            if (publicKey == null && privateKey == null) { 
                _publicKey = _provider.ToXmlString(false);
                _privateKey = _provider.ToXmlString(true);
            }
            else {
                _publicKey = publicKey;
                _privateKey = privateKey;
            }
        }

        public string Encrypt(string plaintext)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            _provider.FromXmlString(_publicKey);
            byte[] cipherBytes = _provider.Encrypt(plainBytes, true);
            return Convert.ToBase64String(cipherBytes);
        }

        public string Decrypt(string ciphertext)
        {
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            _provider.FromXmlString(_privateKey);
            byte[] clearBytes = _provider.Decrypt(cipherBytes, true);         
            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}

using CryptoLib;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // SymmetricDemo();
            // AsymmetricDemo();
            // HashDemo();
            HMACDemo();

            Console.ReadKey();
        }

        static void SymmetricDemo()
        {
            var sym = new Symmetric();

            Console.Write("Plaintext: ");
            string input = Console.ReadLine();

            string output = sym.Encrypt(input);
            Console.WriteLine($"Ciphertext: {output}");
            Console.WriteLine($"Plaintext: {sym.Decrypt(output)}");
        }

        static void AsymmetricDemo()
        {
            var sym = new Asymmetric();

            Console.Write("Plaintext: ");
            string input = Console.ReadLine();

            string output = sym.Encrypt(input);
            Console.WriteLine($"Ciphertext: {output}");
            Console.WriteLine($"Plaintext: {sym.Decrypt(output)}");
        }

        static void HashDemo()
        {
            while (true)
            {
                Console.Write("Input: ");
                string input = Console.ReadLine();

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                var sha = new SHA256Managed();
                byte[] digest = sha.ComputeHash(inputBytes);

                Console.WriteLine(Convert.ToBase64String(digest));
            }
        }

        static void HMACDemo()
        {
            Console.Write("Input: ");
            string input = Console.ReadLine();

            byte[] key = new byte[32];

            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(key);
            }

            string hmac = HmacGen.Gen(input, key);
            Console.WriteLine($"HMAC: {hmac}");

            bool isValid = HmacGen.Verify(input, hmac, key);
            Console.WriteLine($"IsValid: {isValid.ToString()}");
        }
    }
}

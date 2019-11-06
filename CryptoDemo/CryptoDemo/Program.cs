using CryptoLib;
using System;

namespace CryptoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SymmetricDemo();
            // AsymmetricDemo();

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
    }
}

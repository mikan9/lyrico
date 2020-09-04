using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lyrico.Utils
{
    public static class PKCEUtil
    {
        private const int MIN_VERIFIER_LENGTH = 43;
        private const int MAX_VERIFIER_LENGTH = 128;
        private const int DEFAULT_VERIFIER_LENGTH = 100;
        public static (string, string) GenerateCode(int length = DEFAULT_VERIFIER_LENGTH)
        {
            if (length < MIN_VERIFIER_LENGTH || length > MAX_VERIFIER_LENGTH)
                throw new ArgumentException(
                    $"Code verifier length must be between {MIN_VERIFIER_LENGTH} and {MAX_VERIFIER_LENGTH}",
                    nameof(length));

            string verifier = GenerateVerifier(length);
            return (verifier, GenerateChallenge(verifier));
        }

        private static string GenerateVerifier(int length)
        {
            string legalChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.-~";
            byte[] bytes = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(bytes);

            char[] legal = legalChars.ToCharArray();
            int legalLength = legal.Length;
            char[] chars = new char[length];
            for (int i = 0; i < length; ++i)
            {
                chars[i] = legal[bytes[i] % legalLength];
            }

            return new string(chars);
        }

        private static string GenerateChallenge(string verifier)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(verifier);
            string encoded = "";
            using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
            {
                byte[] hash = sha.ComputeHash(bytes);
                encoded = Convert.ToBase64String(hash);
            }

            return encoded;
        }
    }
}

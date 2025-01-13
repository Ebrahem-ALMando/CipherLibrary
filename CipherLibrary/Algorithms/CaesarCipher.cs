using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class CaesarCipher : ICipher
    {
        private int key;
        private bool isKeySet = false;
        private Helper helper;

        public CaesarCipher()
        {
            helper = Helper.Instance;
        }
        public void RemoveKey()
        {
            isKeySet = false;
        }
        // Set the encryption/decryption key
        public void SetKey(string key)
        {
            if (Int32.TryParse(key, out this.key))
            {
                isKeySet = true;
            }
            else
            {
                isKeySet = false;
                throw new ArgumentException("Invalid key format. Please provide a valid integer key.");
            }
        }

        // Ensure the key is set before performing operations
        private void EnsureKeyIsSet()
        {
            if (!isKeySet)
                throw new InvalidOperationException("Key is not set. Please set a valid key before encryption or decryption.");
        }

        // Decrypt a given cipherText
        public string Decrypt(string cipherText)
        {
            EnsureKeyIsSet();
            string plainText = "";
            helper.Loop(cipherText, ref plainText, key, '-');
            return plainText;
        }

        // Encrypt a given plainText
        public string Encrypt(string plainText)
        {
            EnsureKeyIsSet();
            string cipherText = "";
            helper.Loop(plainText, ref cipherText, key, '+');
            return cipherText;
        }
    }
}

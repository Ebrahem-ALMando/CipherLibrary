using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class VigenèreCipher:ICipher
    {
        private string key;
        private bool isKeySet = false;
        private Helper helper;

        public VigenèreCipher()
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
            if (helper.IsTextFreeOfNumbers(key))
            {
                this.key = key;
                isKeySet = true;
            }
            else
            {
                isKeySet = false;
                throw new ArgumentException("Invalid key format. Please provide a valid string key.");
            }
        }

        // Ensure the key is set before performing operations
        private void EnsureKeyIsSet()
        {
            if (!isKeySet)
                throw new InvalidOperationException("Key is not set. Please set a valid key before encryption or decryption.");
        }
        // Encrypt a given plainText
        public string Encrypt(string plainText)
        {
            EnsureKeyIsSet();
            string cipherText = "";

            key = helper.GetExtendedKey(plainText, key);

            for (int i = 0; i < plainText.Length; i++)
            {
                var ki = char.ToLower(key[i]); 
                var pi = plainText[i]; 

                bool isUpper = char.IsUpper(pi); 

         
                int piVal = isUpper ? pi - 'A' : pi - 'a';
                int kiVal = ki - 'a';


                var ci = (char)(helper.GetMode(piVal + kiVal) + (isUpper ? 'A' : 'a'));
                cipherText += ci;
            }

            return cipherText;
        }

        // Decrypt a given cipherText
        public string Decrypt(string cipherText)
        {
            EnsureKeyIsSet();
            string plainText = "";
            key=helper.GetExtendedKey(cipherText, key);
            for (int i = 0; i < cipherText.Length; i++)
            {
                var ki =char.ToLower( key[i]);
                var ci = cipherText[i];

                bool isUpper = char.IsUpper(ci);
               
                int ciVal = isUpper ? ci - 'A' : ci - 'a';
                int kiVal = ki - 'a';

                var pi = (char)(helper.GetMode(ciVal - kiVal + helper.lenChar) + (isUpper ? 'A' : 'a'));
            plainText += pi;
            }

            return plainText;
        }

    }
}

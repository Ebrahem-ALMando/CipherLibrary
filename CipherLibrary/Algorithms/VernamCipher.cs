using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class VernamCipher : ICipher
    {
        private string key;
        private bool isKeySet = false;
        private Helper helper;

        public VernamCipher()
        {
            helper = Helper.Instance;
        }
        public void RemoveKey()
        {
            isKeySet = false;
        }
        public void SetKey(string key)
        {
            if (key.Length>0)
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

        public string Encrypt(string plaintext)
        {
            EnsureKeyIsSet();
          
            string preparedText = plaintext.Replace(" ", "");

            if (preparedText.Length != key.Length)
            {
              key=helper.GetExtendedKey(plaintext,key);
            }
            byte[] cipherBytes = new byte[preparedText.Length];
            for (int i = 0; i < preparedText.Length; i++)
            {
                var c= char.ToLower(preparedText[i]);

                byte p = helper.ConvertCharToBinary(c);

                byte k = helper.ConvertCharToBinary(key[i]);
             
                cipherBytes[i] = helper.ConvertCharToBinary((char)(p ^ k));
            }
       
            return Convert.ToBase64String(cipherBytes);
        }
        public string Decrypt(string ciphertext)
        {
            EnsureKeyIsSet();
        /*    string preparedText = plaintext.Replace(" ", "");

            if (preparedText.Length != key.Length)
            {
                key = helper.GetExtendedKey(plaintext, key);
            }*/
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            char[] plainText = new char[cipherBytes.Length];

            for (int i = 0; i < plainText.Length; i++)
            {

                byte c = cipherBytes[i];

                byte k = helper.ConvertCharToBinary(key[i]);

                plainText[i] = helper.ConvertBinaryToChar((byte)(c ^ k));
            }

            return new string(plainText);
        }

      

       
    }
}

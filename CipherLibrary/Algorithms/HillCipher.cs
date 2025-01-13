using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class HillCipher : ICipher
    {
        Helper helper;
        private int[,] key;
        private int[,] inverseMatrix;
        private bool isKeySet;


        public HillCipher()
        {
            helper=Helper.Instance;
        }
        public void RemoveKey()
        {
            isKeySet = false;
        }
        private void EnsureKeyIsSet()
        {
            if (!isKeySet)
                throw new InvalidOperationException("Key is not set. Please set a valid key before encryption or decryption.");
        }
        public void SetKey(string key)
        {
            int a = -1, b = -1,c=-1,d=-1;

            try
            {
                string[] keyParts = key.Split(',');
            
                if (keyParts.Length == 4)
                {

                    if (!Int32.TryParse(keyParts[0], out a))
                    {
                        throw new Exception("Invalid value for 'a'.");
                    }
                    if (!Int32.TryParse(keyParts[1], out b))
                    {
                        throw new Exception("Invalid value for 'b'.");
                    }
                    if (!Int32.TryParse(keyParts[2], out c))
                    {
                        throw new Exception("Invalid value for 'c'.");
                    }
                    if (!Int32.TryParse(keyParts[3], out d))
                    {
                        throw new Exception("Invalid value for 'd'.");
                    }
                    int[,] keyMatrix =
                    {
                    {a,b},
                    {c,d}
                    };
                    if (helper.IsFoundInverseKeyMatrix(keyMatrix))
                    {

                        this.key = keyMatrix;
                        isKeySet = true;
                        inverseMatrix = helper.GetInverseMatrix(this.key);
                      
                    }
                    else
                    {
                        throw new Exception("Invalid key matrix, it must be coprime with 26. Please try again.");
                    }
                }
                else
                {
                    isKeySet = false;
                    throw new Exception("Key should contain foure values separated by key matrix comma.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting key: " + ex.Message);
            }


        }

        public string Encrypt(string plaintext)
        {
            EnsureKeyIsSet();
            plaintext = plaintext.Replace(" ", "").ToLower();
            if (plaintext.Length % 2 != 0)
            {
                plaintext = plaintext.PadRight(plaintext.Length + 2 - (plaintext.Length % 2), 'x');
            }
            int[] numericText = plaintext.Select(c => c - 'a').ToArray();
            int[] encryptedText = new int[numericText.Length];
            for (int i = 0; i < numericText.Length; i += 2)
            {
                for (int j = 0; j < 2; j++)
                {
                    encryptedText[i + j] = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        encryptedText[i + j] += key[j, k] * numericText[i + k];
                    }
                    encryptedText[i + j] %= 26;
                }
            }
            return new string(encryptedText.Select(n => (char)(n + 'a')).ToArray());
        }

        public string Decrypt(string ciphertext)
        {
            EnsureKeyIsSet();
            int[] numericText = ciphertext.Select(c => c - 'a').ToArray();
            int[] decryptedText = new int[numericText.Length];
            for (int i = 0; i < numericText.Length; i += 2)
            {
                for (int j = 0; j < 2; j++)
                {
                    decryptedText[i + j] = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        decryptedText[i + j] += inverseMatrix[j, k] * numericText[i + k];
                    }
                    decryptedText[i + j] = helper.GetMode(decryptedText[i + j]);
                }
            }
            return new string(decryptedText.Select(n => (char)(n + 'a')).ToArray());
        }
    }
}


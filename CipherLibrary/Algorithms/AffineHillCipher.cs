using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class AffineHillCipher : ICipher
    {
        Helper helper;
        private int[,] key;
        private int[,] keyB;

        private int[,] inverseMatrix;
        private bool isKeySet;
        private bool isKeyBSet=false;

        public AffineHillCipher()
        {
            helper = Helper.Instance;
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

        private bool EnsureKeyBIsSet()
        {
            return isKeyBSet;
               
        }
        public void SetKey(string key)
        {
            int a = -1, b = -1, c = -1, d = -1;

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
         
           int[,] plainIndex=helper.ConvertTextToTwoRowMatrix(plaintext);

           int[,] multResultMatrix = helper.MultiplyMatrices(key, plainIndex);

            if (!EnsureKeyBIsSet())
            {
                keyB = helper.GenerateKeyB(multResultMatrix);
                isKeyBSet = true;
            }

            int[,] addResultMatrix = helper.GetAddOrSubMatrix(multResultMatrix, keyB, '+');
            string cipherText = "";
            foreach (int c in addResultMatrix) 
            {
                cipherText += helper.alpa[helper.GetMode(c)];
            }


            return cipherText;
        }

        public string Decrypt(string ciphertext)
        {
            EnsureKeyIsSet();
          
            int[,] cipherIndex = helper.ConvertTextToTwoRowMatrix(ciphertext);
            if (!EnsureKeyBIsSet())
            {
                keyB = helper.GenerateKeyB(cipherIndex);
                isKeyBSet = true;

            }
            int[,] addResultMatrix = helper.GetAddOrSubMatrix(cipherIndex, keyB, '-');

            int[,] multResultMatrix = helper.MultiplyMatrices(inverseMatrix, addResultMatrix);

         

           
            string plainText = "";
            foreach (int c in multResultMatrix)
            {
                plainText += helper.alpa[helper.GetMode(c)];
            }


            return plainText;
        }

      
    }
}

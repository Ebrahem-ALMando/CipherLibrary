using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class AffineCipher : ICipher
    {
        public int inverseA { get; set; } = -1;
        private int[]key;
        private bool isKeySet = false;
        private Helper helper;
        public AffineCipher()
        {
            helper = Helper.Instance;
        }

        public void RemoveKey()
        {
            isKeySet=false;
        }
        public void SetKey(string key)
        {
            int a = -1, b = -1;

            try
            {
                string[] keyParts = key.Split(',');

                if (keyParts.Length == 2)
                {
      
                    if (Int32.TryParse(keyParts[0], out a))
                    {
                        if (helper.isFoundInverseA(a))
                        {
                            inverseA =helper.GetInverse(a);
                        }
                        else
                        {
                            throw new Exception("Invalid key 'a', it must be coprime with 26. Please try again.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid value for 'a'.");
                    }

          
                    if (!Int32.TryParse(keyParts[1], out b))
                    {
                        throw new Exception("Invalid value for 'b'.");
                    }


                    this.key = new int[] { a ,b };
                    isKeySet = true;
                }
                else
                {
                    isKeySet = false;
                    throw new Exception("Key should contain two values separated by a comma.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting key: " + ex.Message);
            }

            
        }
        private void EnsureKeyIsSet()
        {
            if (!isKeySet)
                throw new InvalidOperationException("Key is not set. Please set a valid key before encryption or decryption.");
        }
        /// <summary>
        /// calc Method ==>  CipherText=(a×PlainTextIndex+b)mod26
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns> 
        /// Cipher Text
        /// </returns>
        public string Encrypt(string plainText)
        {
            EnsureKeyIsSet();
            string cipherText = "";
            var alpa = helper.alpa;
            foreach (char c in plainText)
            {
                bool isUpper = char.IsUpper(c);
                var ch = char.ToLower(c);
                if (alpa.Contains(ch))
                {
                    int index = alpa.IndexOf(ch);
                    int cipherIndex = helper.GetMode((key[0] * index + key[1]));
                    cipherText += isUpper ? char.ToUpper( alpa[cipherIndex]) : alpa[cipherIndex];
                }
                else
                {
                    cipherText += c;
                }
            }
            return cipherText;
        }
        /// <summary>
        ///  PlainText=a^−1×(CipherTextIndex−b)mod26
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>
        ///  Plain Text
        /// </returns>
        public string Decrypt(string cipherText)
        {
            EnsureKeyIsSet();
            string plainText = "";
            var alpa = helper.alpa;
            foreach (char c in cipherText)
            {
                bool isUpper = char.IsUpper(c);
                var ch = char.ToLower(c);
                if (alpa.Contains(ch))
                {
                    int index = alpa.IndexOf(ch);
                    int plainIndex = helper.GetMode((inverseA * (index - key[1])));
                    plainText += isUpper ? char.ToUpper(alpa[plainIndex]): alpa[plainIndex];
                }
                else
                {
                    plainText += c;
                }
            }
            return plainText;
        }
    }
}



   










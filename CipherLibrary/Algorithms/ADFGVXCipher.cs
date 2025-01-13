using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CipherLibrary.Algorithms
{
    internal class ADFGVXCipher : ICipher
    {
        private Helper helper;
        private static readonly char[] indicators = { 'A', 'D', 'F', 'G', 'V', 'X' };
        private static readonly char[,] polybiusSquare = {
            { 'D', 'H', 'X', 'M', 'U', '4' },
            { 'P', '3', 'J', '6', 'A', 'O' },
            { 'I', 'B', 'Z', 'V', '9', 'W' },
            { '1', 'N', '7', 'O', 'Q', 'K' },
            { 'F', 'S', 'L', 'Y', 'C', '8' },
            { 'T', 'R', '5', 'E', '2', 'G' }
        };


        private bool isKeySet;
        private string Key;
        private string ADFGVXChars = "ADFGVX";

        public ADFGVXCipher()
        {
            helper = Helper.Instance;
        }
        private string Substitute(string plaintext)
        {
            StringBuilder substitutedText = new StringBuilder();
            plaintext = plaintext.ToUpper().Replace(" ", "");

            foreach (char c in plaintext)
            {
                bool found = false;
                for (int row = 0; row < 6 && !found; row++)
                {
                    for (int col = 0; col < 6 && !found; col++)
                    {
                        if (polybiusSquare[row, col] == c)
                        {
                            substitutedText.Append(indicators[row]);
                            substitutedText.Append(indicators[col]);
                            found = true;
                        }
                    }
                }
            }

            return substitutedText.ToString();
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
            Key = key;
            isKeySet = true;
      /*      PolybiusSquare = helper.CreatePolybiusSquare();*/
        }

        public string Encrypt(string plaintext)
        {
            EnsureKeyIsSet();
            string substitutedText = Substitute(plaintext);
            return helper.Transpose(substitutedText,Key);
        }

        public string Decrypt(string ciphertext)
        {
            EnsureKeyIsSet();
            int numCols = Key.Length;
            int numRows = (int)Math.Ceiling((double)ciphertext.Length / numCols);
            char[,] table = new char[numRows, numCols];
            // ملء الأعمدة بترتيبها الأصلي حسب الكلمة
            List<KeyValuePair<int, char>> sortedKey = new List<KeyValuePair<int, char>>();
            for (int i = 0; i < Key.Length; i++)
            {
                sortedKey.Add(new KeyValuePair<int, char>(i, Key[i]));
            }
            sortedKey.Sort((a, b) => a.Value.CompareTo(b.Value));

            int index = 0;
            foreach (var pair in sortedKey)
            {
                int col = pair.Key;
                for (int row = 0; row < numRows; row++)
                {
                    if (index < ciphertext.Length)
                    {
                        table[row, col] = ciphertext[index++];
                    }
                }
            }

            // إعادة تكوين النص بعد فك الترتيب
            StringBuilder transposedText = new StringBuilder();
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    transposedText.Append(table[row, col]);
                }
            }

            // فك الاستبدال
            string transposedTextStr = transposedText.ToString();
            StringBuilder decryptedText = new StringBuilder();

            for (int i = 0; i < transposedTextStr.Length; i += 2)
            {
                int row = Array.IndexOf(indicators, transposedTextStr[i]);
                int col = Array.IndexOf(indicators, transposedTextStr[i + 1]);
                decryptedText.Append(polybiusSquare[row, col]);
            }

            return decryptedText.ToString();
        }
    }
}

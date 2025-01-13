using CipherLibrary.Helpers;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Algorithms
{
    internal class PlayfairCipher:ICipher
    {
        private Helper helper;
        private char[,] keyTable;
        private const int TableSize = 5;
        private const char FillerChar = 'X';
        private bool isKeySet;
        public PlayfairCipher()
        {
            helper=Helper.Instance;
        }
        public void RemoveKey()
        {
            isKeySet = false;
        }
        public void SetKey(string key)
        {
            keyTable = GenerateKeyTable(key);
            isKeySet = true;
        }
        private void EnsureKeyIsSet()
        {
            if (!isKeySet)
                throw new InvalidOperationException("Key is not set. Please set a valid key before encryption or decryption.");
        }
        public string Encrypt(string plaintext)
        {
            var preparedText = helper.PrepareTextForPlayfair(plaintext);
            return ProcessText(preparedText, isEncrypt: true);
        }

        public string Decrypt(string ciphertext)
        {
            return ProcessText(ciphertext, isEncrypt: false);
        }

        private string ProcessText(string text, bool isEncrypt)
        {
            EnsureKeyIsSet();
            string result = string.Empty;
            for (int i = 0; i < text.Length; i += 2)
            {
                var pair = text.Substring(i, 2);
                var (row1, col1) = helper.FindPosition(keyTable, pair[0]);
                var (row2, col2) = helper.FindPosition(keyTable, pair[1]);

                if (row1 == row2)
                {
                    // نفس الصف
                    col1 = isEncrypt ? (col1 + 1) % TableSize : (col1 - 1 + TableSize) % TableSize;
                    col2 = isEncrypt ? (col2 + 1) % TableSize : (col2 - 1 + TableSize) % TableSize;
                }
                else if (col1 == col2)
                {
                    // نفس العمود
                    row1 = isEncrypt ? (row1 + 1) % TableSize : (row1 - 1 + TableSize) % TableSize;
                    row2 = isEncrypt ? (row2 + 1) % TableSize : (row2 - 1 + TableSize) % TableSize;
                }
                else
                {
                    // مستطيل
                    var temp = col1;
                    col1 = col2;
                    col2 = temp;
                }

                result += keyTable[row1, col1];
                result += keyTable[row2, col2];
            }
            return result;
        }

        private char[,] GenerateKeyTable(string key)
        {
            key = helper.RemoveDuplicates(key.Replace("J", "I").ToUpper());
            var alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();
            var keySet = new HashSet<char>(key);
            var table = new char[TableSize, TableSize];

            int index = 0;
            foreach (var c in key)
            {
                table[index / TableSize, index % TableSize] = c;
                index++;
            }

            foreach (var c in alphabet)
            {
                if (!keySet.Contains(c))
                {
                    table[index / TableSize, index % TableSize] = c;
                    index++;
                }
            }

            return table;
        }
    }
}

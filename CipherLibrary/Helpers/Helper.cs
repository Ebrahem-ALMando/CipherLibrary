using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Helpers
{
    internal class Helper
    {
        public readonly int lenChar = 26;
        public readonly string alpa = "abcdefghijklmnopqrstuvwxyz";

        private static Helper _instance;
        public static Helper Instance => _instance ?? (_instance = new Helper());
        private Helper() { }

        // Calculate mod to ensure index stays within bounds
        public int GetMode(int x)
        {
            return ((x % lenChar) + lenChar) % lenChar;
        }


        // Loop through text for encryption or decryption
        public void Loop(string text, ref string newText, int k, char op)
        {
            newText = "";
            for (int i = 0; i < text.Length; i++)
            {
                var charLower = char.ToLower(text[i]);
                bool isUpper = char.IsUpper(text[i]);
                if (alpa.Contains(charLower))
                {
                    var indexChar = op == '-' ? (alpa.IndexOf(charLower) - k) : (alpa.IndexOf(charLower) + k);
                    var cipherChar = alpa[GetMode(indexChar)];
                    newText += isUpper ? char.ToUpper(cipherChar) : cipherChar;
                }
                else
                {
                    newText += text[i]; // Preserve non-alphabetic characters
                }
            }
        }

        public int GetInverse(int a)
        {
            int t = 0, newT = 1;
            int r = lenChar, newR = a;

            while (newR != 0)
            {
                int quotient = r / newR;


                int tempT = t;
                t = newT;
                newT = tempT - quotient * newT;


                int tempR = r;
                r = newR;
                newR = tempR - quotient * newR;
            }

            if (r > 1)
            {
                return -1;
            }

            if (t < 0)
            {
                t = t + lenChar;
            }

            return t;
        }


        public int[,] GetInverseMatrix(int[,] matrix)
        {
            int determinant = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) % lenChar;
            determinant = determinant < 0 ? determinant + lenChar : determinant;
            int inverseDet = GetInverse(determinant);

            if (inverseDet == -1)
            {
                throw new ArgumentException("No modular inverse found for determinant.");
            }

            int[,] adjugateMatrix = {
            { matrix[1, 1], -matrix[0, 1] },
            { -matrix[1, 0], matrix[0, 0] }
        };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    adjugateMatrix[i, j] = adjugateMatrix[i, j] < 0 ? adjugateMatrix[i, j] + lenChar : adjugateMatrix[i, j];
                    adjugateMatrix[i, j] = (adjugateMatrix[i, j] * inverseDet) % lenChar;
                }
            }

            return adjugateMatrix;
        }

        public bool isFoundInverseA(int a)
        {
            return GetInverse(a) != -1;
        }
        public bool IsFoundInverseKeyMatrix(int[,] key)
        {
            try
            {
                int[,] inverse = GetInverseMatrix(key);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool IsTextFreeOfNumbers(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public string GetExtendedKey(string text, string key)
        {
            var TextLen = text.Length;
            var cipherKeyLen = key.Length;


            while (cipherKeyLen < TextLen)
            {
                key += key;
                cipherKeyLen = key.Length;
            }
            key = key.Substring(0, TextLen);
            return key;
        }

        public byte ConvertCharToBinary(char c)
        {
            return (byte)c;
        }
        public char ConvertBinaryToChar(byte b)
        {
            return (char)b;
        }

        public int[,] GetAddOrSubMatrix(int[,] m1, int[,] m2,char op)
        {

            var row=m1.GetLength(0);
            var col = m1.GetLength(1);

            int[,] result = new int[row,col];

            if (m1 == null || m2 == null)
                return null;

            if (m1.Length == m2.Length)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        result[i, j] = op=='+'?m1[i, j] + m2[i, j]: m1[i, j] - m2[i, j];
                    }
                }
                return result;
            }
            return null;

        }


        public int[,] GenerateKeyB(int[,] m)
        {
            var row = m.GetLength(0);
            var col = m.GetLength(1);

            int[,] b = new int[row, col];



            Random random = new Random();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    b[i, j] = random.Next(0, 25);
                }
            }

            /*    int[,] b =
                {
                     {1,0,1 },
                     {0,1,1 }
                 };*/

            return b; 
        }


        public int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

  
            if (colsA != rowsB)
            {
                throw new InvalidOperationException("عدد أعمدة المصفوفة الأولى يجب أن يساوي عدد صفوف المصفوفة الثانية.");
            }

   
            int[,] result = new int[rowsA, colsB];


            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    result[i, j] = 0; 
                    for (int k = 0; k < colsA; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return result;
        }

        public int[,] ConvertTextToTwoRowMatrix(string text)
        {
      
            text = text.Replace(" ", "").ToLower();

        
            if (text.Length % 2 != 0)
            {
                text += 'x';
            }

      
            int columns = text.Length / 2;

      
            int[,] matrix = new int[2, columns];

            int index=0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = text[index++]-'a';
                }
            }

            return matrix;
        }



        public  string PrepareTextForPlayfair(string text)
        {
            text = text.Replace(" ", "").Replace("J", "I").ToUpper();
            StringBuilder preparedText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (i < text.Length - 1 && text[i] == text[i + 1])
                {
                    preparedText.Append(text[i]).Append('X');
                }
                else
                {
                    preparedText.Append(text[i]);
                }
            }

            if (preparedText.Length % 2 != 0)
            {
                preparedText.Append('X');
            }

            return preparedText.ToString();
        }

        public  (int, int) FindPosition(char[,] table, char c)
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    if (table[row, col] == c)
                    {
                        return (row, col);
                    }
                }
            }
            throw new ArgumentException($"Character '{c}' not found in the key table.");
        }

        public  string RemoveDuplicates(string input)
        {
            var seen = new HashSet<char>();
            var result = new StringBuilder();

            foreach (var c in input)
            {
                if (seen.Add(c))
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

/*        public char[,] CreatePolybiusSquare()
        {
            char[,] square = new char[6, 6];
            HashSet<char> usedChars = new HashSet<char>();
            Random random = new Random();

            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int index = 0;

            foreach (char ch in characters.OrderBy(c => random.Next()))
            {
                if (!usedChars.Contains(ch))
                {
                    square[index / 6, index % 6] = ch;
                    usedChars.Add(ch);
                    index++;
                }
            }

            return square;
        }*/
        public string PrepareText(string text)
        {
            return new string(text.ToUpper().Where(char.IsLetterOrDigit).ToArray());
        }
 

        public string ReverseTranspose(string text, string key)
        {
            int keyLength = key.Length;
            int rows = (int)Math.Ceiling((double)text.Length / keyLength);
            char[,] grid = new char[rows, keyLength];
            StringBuilder original = new StringBuilder();

            // قم بتحديد ترتيب الأعمدة بناءً على ترتيب المفتاح
            List<int> columnOrder = key
                .Select((k, indeX) => new { KeyChar = k, Index = indeX })
                .OrderBy(x => x.KeyChar)
                .Select(x => x.Index)
                .ToList();

            int index = 0;

            // ملء الشبكة بالأحرف بناءً على ترتيب الأعمدة
            for (int col = 0; col < keyLength; col++)
            {
                int colIndex = columnOrder.IndexOf(col);
                for (int row = 0; row < rows; row++)
                {
                    if (index < text.Length)
                    {
                        grid[row, colIndex] = text[index++];
                    }
                }
            }

            // اقرأ البيانات بشكل صفوف لاستعادة النص الأصلي
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < keyLength; c++)
                {
                    original.Append(grid[r, c]);
                }
            }

            return original.ToString().TrimEnd('X'); // إزالة الحشو إذا كان موجودًا
        }
        public string Transpose(string text,string keyword)
        {
            int numCols = keyword.Length;
            int numRows = (int)Math.Ceiling((double)text.Length / numCols);
            char[,] table = new char[numRows, numCols];

            // ملء الجدول بالنص المشفر
            int index = 0;
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (index < text.Length)
                    {
                        table[row, col] = text[index++];
                    }
                    else
                    {
                        table[row, col] = 'Z'; // تعبئة الفراغات بـ X
                    }
                }
            }

            // ترتيب الأعمدة بناءً على الكلمة المفتاحية
            List<KeyValuePair<int, char>> sortedKey = new List<KeyValuePair<int, char>>();
            for (int i = 0; i < keyword.Length; i++)
            {
                sortedKey.Add(new KeyValuePair<int, char>(i, keyword[i]));
            }
            sortedKey.Sort((a, b) => a.Value.CompareTo(b.Value));

            StringBuilder transposedText = new StringBuilder();
            foreach (var pair in sortedKey)
            {
                int col = pair.Key;
                for (int row = 0; row < numRows; row++)
                {
                    transposedText.Append(table[row, col]);
                }
            }

            return transposedText.ToString();
        }



    }
}

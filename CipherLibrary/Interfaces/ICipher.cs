using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Interfaces
{
    public interface ICipher
    {
        void SetKey(string key);
        void RemoveKey();
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);
    }
}

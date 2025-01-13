using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CipherLibrary.Enums;
using CipherLibrary.Factories;
namespace CipherLibraryTests
{
    [TestClass]
    public class CaesarCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {
          
            var caesarCipher = CipherFactoryProvider.CreateCipher(CipherType.Caesar);

            caesarCipher.SetKey("3");
      
            string plaintext = "Hello World";
            string expectedCiphertext = "Khoor Zruog";

            string actualCiphertext = caesarCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var caesarCipher = CipherFactoryProvider.CreateCipher(CipherType.Caesar);

            caesarCipher.SetKey("3");

            string ciphertext = "Khoor Zruog";
            string expectedPlaintext = "Hello World";

            string actualPlaintext = caesarCipher.Decrypt(ciphertext);

            Assert.AreEqual(actualPlaintext, expectedPlaintext);
        }

    }
}

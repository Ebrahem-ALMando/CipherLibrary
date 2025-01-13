using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CipherLibraryTests.CipherTests
{
    [TestClass]
    public class ADFGVXTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var ADFGVXCipher = CipherFactoryProvider.CreateCipher(CipherType.ADFGVX);

            ADFGVXCipher.SetKey("secret");

            string plaintext = "attackat1200am";
            string expectedCiphertext = "GVXDDFADGGAGDDVA";

            string actualCiphertext = ADFGVXCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var ADFGVXCipher = CipherFactoryProvider.CreateCipher(CipherType.ADFGVX);

            ADFGVXCipher.SetKey("KEYWORD");

            string ciphertext = "GVXDDFADGGAGDDVA";
            string expectedPlaintext = "HELLO123";

            string actualPlaintext = ADFGVXCipher.Decrypt(ciphertext);

            Assert.AreEqual(actualPlaintext, expectedPlaintext);
        }

    }
}

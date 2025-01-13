using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CipherLibraryTests.CipherTests
{
    [TestClass]
    public class VernamCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var vernamCipher = CipherFactoryProvider.CreateCipher(CipherType.Vernam);

            vernamCipher.SetKey("xxckl");

            string plaintext = "HELLO";
            string expectedCiphertext = "EB0PBwM=";

            string actualCiphertext = vernamCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var vernamCipher = CipherFactoryProvider.CreateCipher(CipherType.Vernam);

            vernamCipher.SetKey("xxckl");

            string ciphertext = "EB0PBwM=";
            string expectedPlaintext = "hello";

            string actualPlaintext = vernamCipher.Decrypt(ciphertext);

            Assert.AreEqual(actualPlaintext, expectedPlaintext);
        }
    }
}

using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CipherLibraryTests.CipherTests
{
    /// <summary>
    /// Summary description for HillCipherTests
    /// </summary>
    [TestClass]
    public class HillCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var hillCipher = CipherFactoryProvider.CreateCipher(CipherType.Hill);

            hillCipher.SetKey("3,3,2,5");

            string plaintext = "heloo";
            string expectedCiphertext = "hixohn";

            string actualCiphertext = hillCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }
        [TestMethod]
        public void DecryptTest()
        {

            var hillCipher = CipherFactoryProvider.CreateCipher(CipherType.Hill);

            hillCipher.SetKey("3,3,2,5");

            string ciphertext = "hixohn";
            string expectedPlaintext = "heloox";


            string actualPlaintext = hillCipher.Decrypt(ciphertext);

            Assert.AreEqual(expectedPlaintext, actualPlaintext);
        }
    }
}

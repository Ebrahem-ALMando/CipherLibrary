using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CipherLibraryTests.CipherTests
{
    /// <summary>
    /// Summary description for VigenèreCipherTests
    /// </summary>
    [TestClass]
    public class VigenèreCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var vigenèreCipher = CipherFactoryProvider.CreateCipher(CipherType.Vigenère);

            vigenèreCipher.SetKey("keY");

            string plaintext = "HELLO";
            string expectedCiphertext = "RIJVS";

            string actualCiphertext = vigenèreCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }
        [TestMethod]
        public void DecryptTest()
        {

            var vigenèreCipher = CipherFactoryProvider.CreateCipher(CipherType.Vigenère);

            vigenèreCipher.SetKey("keY");

            string ciphertext = "RIJVS";
            string expectedPlaintext = "HELLO";


            string actualPlaintext = vigenèreCipher.Decrypt(ciphertext);

            Assert.AreEqual(expectedPlaintext, actualPlaintext);
        }
    }
}

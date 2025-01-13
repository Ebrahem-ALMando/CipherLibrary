using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CipherLibraryTests.CipherTests
{
    /// <summary>
    /// Summary description for AffineCipherTests
    /// </summary>
    [TestClass]
    public class AffineCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var affineCipher = CipherFactoryProvider.CreateCipher(CipherType.Affine);

            affineCipher.SetKey("7,2");

            string plaintext = "Hello World";
            string expectedCiphertext = "Zebbw Awrbx";

            string actualCiphertext = affineCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var affineCipher = CipherFactoryProvider.CreateCipher(CipherType.Affine);

            affineCipher.SetKey("7,2");

            string ciphertext = "Zebbw Awrbx";
            string expectedPlaintext = "Hello World";
        

            string actualPlaintext = affineCipher.Decrypt(ciphertext);

            Assert.AreEqual(expectedPlaintext, actualPlaintext);
        }
    }
}

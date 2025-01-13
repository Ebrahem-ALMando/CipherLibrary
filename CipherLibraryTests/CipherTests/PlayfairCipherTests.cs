using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CipherLibraryTests.CipherTests
{
    [TestClass]
    public class PlayfairCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var playFairCipher = CipherFactoryProvider.CreateCipher(CipherType.PlayFair);

            playFairCipher.SetKey("PlAYFAIR");

            string plaintext = "hello WORLD";
            string expectedCiphertext = "KGYVRVVQGRCZ";

            string actualCiphertext = playFairCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
            
                
        }

        [TestMethod]
        public void DecryptTest()
        {
            var playFairCipher = CipherFactoryProvider.CreateCipher(CipherType.PlayFair);

            playFairCipher.SetKey("PlAYFAIR");

            string ciphertext = "KGYVRVVQGRCZ";
            string expectedPlaintext = "HELXLOWORLDX";

            string actualPlaintext = playFairCipher.Decrypt(ciphertext);

            Assert.AreEqual(actualPlaintext, expectedPlaintext);
        }

    }
}

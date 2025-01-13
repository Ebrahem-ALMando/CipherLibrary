using CipherLibrary.Enums;
using CipherLibrary.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CipherLibraryTests.CipherTests
{
    [TestClass]
    public class AffineHillCipherTests
    {
        [TestMethod]
        public void EncryptTest()
        {

            var affineHillCipher = CipherFactoryProvider.CreateCipher(CipherType.AffineHill);

            affineHillCipher.SetKey("3,3,1,2");

            string plaintext = "ahmads";
            string expectedCiphertext = "benaox";

            string actualCiphertext = affineHillCipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);

            /*  string ciphertext = actualCiphertext;
              string expectedPlaintext = "ahmads";


              string actualPlaintext = affineHillCipher.Decrypt(ciphertext);*/

            /*         Assert.AreEqual(expectedPlaintext, actualPlaintext);*/
        }
        [TestMethod]
        public void DecryptTest()
        {

            var affineHillCipher = CipherFactoryProvider.CreateCipher(CipherType.AffineHill);

            affineHillCipher.SetKey("3,3,1,2");

            string ciphertext = "cpkhtz";
            string expectedPlaintext = "ahmads";


            string actualPlaintext = affineHillCipher.Decrypt(ciphertext);

            Assert.AreEqual(expectedPlaintext, actualPlaintext);
        }

    }
}

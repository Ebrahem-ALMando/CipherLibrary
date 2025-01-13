using CipherLibrary.Algorithms;
using CipherLibrary.Enums;
using CipherLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLibrary.Factories
{
    public static class CipherFactoryProvider
    {
        public static ICipher CreateCipher (CipherType cipherType)
        {
            switch (cipherType)
            {
                case CipherType.Caesar:
                    return new CaesarCipher();
                case CipherType.Affine:
                    return new AffineCipher();
                case CipherType.Hill:
                    return new HillCipher();
                case CipherType.Vigenère:
                    return new VigenèreCipher();
                case CipherType.Vernam:
                    return new VernamCipher();
                case CipherType.AffineHill:
                    return new AffineHillCipher();
                case CipherType.PlayFair:
                    return new PlayfairCipher();
                case CipherType.ADFGVX:
                    return new ADFGVXCipher();

                default:
                    throw new ArgumentException("Unsupported cipher type.");
            }
        }
    }
}

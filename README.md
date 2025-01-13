## CipherLibrary
- **CipherLibrary** is a C# class library designed to implement common encryption algorithms used in cryptography and cybersecurity. This library provides a collection of essential ciphers such as Caesar Cipher, Affine Cipher, Hill Cipher, Vigenère Cipher, Playfair Cipher, and more. The library offers a flexible and maintainable solution for integrating encryption functionality into various applications.

## Technologies Used:
- **C#:** The primary programming language used for implementing the algorithms and design patterns.
- **Class Library:** The library is structured as a C# class library, enabling easy integration into other C# projects.
## Design Patterns Used:
- **Singleton Pattern:** The Helper class follows the Singleton pattern to ensure that only one instance of the class is created throughout the application's lifecycle. This helps to manage shared resources and provide a centralized utility for the ciphers.

- **Factory Pattern:** The CipherFactoryProvider class uses the Factory pattern to create cipher instances dynamically based on the specified cipher type. This allows for easy extension and management of new cipher types without modifying the core logic.

### Library Structure:
```text
CipherLibrary
│
├── Algorithms
│   ├── CaesarCipher.cs
│   ├── AffineCipher.cs
│   ├── HillCipher.cs
│   ├── VigenèreCipher.cs
│   ├── VernamCipher.cs
│   ├── AffineHillCipher.cs
│   ├── PlayfairCipher.cs
│   └── ADFGVXCipher.cs
│
├── Enums
│   └── CipherType.cs
│
├── Factories
│   └── CipherFactoryProvider.cs
│
├── Helpers
│   └── Helper.cs
│
├── Interfaces
│   ├── ICipher.cs
│   └── IKeyedCipher.cs
│
└── Tests
    ├── ADFGVXTests.cs
    ├── AffineCipherTests.cs
    ├── AffineHillCipherTests.cs
    ├── CaesarCipherTests.cs
    ├── HillCipherTests.cs
    ├── PlayfairCipherTests.cs
    └── VigenèreCipherTests.cs
```
## Description of Components:


- **Algorithms:** This folder contains classes for the various encryption algorithms (e.g., CaesarCipher.cs, AffineCipher.cs, VigenèreCipher.cs). Each class implements the ICipher interface, ensuring consistency across different ciphers.

- **Enums:** The CipherType.cs enum defines the available cipher types that can be used in the library, such as Caesar, Affine, Vigenère, etc.

- **Factories:** The CipherFactoryProvider.cs file contains the logic for the Factory pattern, which dynamically creates cipher objects based on the selected cipher type.

- **Helpers:** The Helper.cs class contains utility functions used across the cipher classes. It follows the Singleton pattern, ensuring that only one instance is used throughout the application for efficiency.

- **Interfaces:** The ICipher.cs and IKeyedCipher.cs interfaces define the required methods for implementing encryption and decryption functions. This ensures that all cipher classes provide a standard set of methods such as Encrypt(), Decrypt(), SetKey(), and RemoveKey().

- **Tests:** The Tests folder contains unit tests for each cipher, ensuring that they function correctly under various scenarios.

## Features:
- **Maintainability: **The library is designed with maintainability in mind. It allows for easy addition of new ciphers in the future by simply adding new classes that implement the ICipher interface.

- **Test-Driven Development:** The library is supported by unit tests for each cipher algorithm to ensure that encryption and decryption operations are correct.

- **Extensibility:** The use of the Factory pattern allows for easy extension of cipher types. Adding a new cipher is as simple as creating a new class and updating the CipherFactoryProvider to support it.

## Example Usage:
- **Here’s an example of how to use the library to encrypt and decrypt a message using the Caesar cipher:**
```C#
using CipherLibrary;
using CipherLibrary.Factories;
using CipherLibrary.Enums;

namespace CipherLibraryTests
{
    public class CaesarCipherExample
    {
        public static void Main(string[] args)
        {
            // Create an instance of the Caesar cipher
            var caesarCipher = CipherFactoryProvider.CreateCipher(CipherType.Caesar);

            // Set the encryption key
            caesarCipher.SetKey("3");

            // Define plaintext
            string plaintext = "Hello World";
            Console.WriteLine("Plaintext: " + plaintext);

            // Encrypt the plaintext
            string ciphertext = caesarCipher.Encrypt(plaintext);
            Console.WriteLine("Ciphertext: " + ciphertext);

            // Decrypt the ciphertext
            string decryptedText = caesarCipher.Decrypt(ciphertext);
            Console.WriteLine("Decrypted Text: " + decryptedText);
        }
    }
}
```
## Sample Output:
```C#
Plaintext: Hello World
Ciphertext: Khoor Zruog
Decrypted Text: Hello World
```
## In this example:

- **We first create a CaesarCipher object using the Factory pattern.
We set the key for encryption.
We encrypt a message and print the result.
We decrypt the message and print the original message.**
## Getting Started:
Clone the repository:
[git clone https://github.com/Ebrahem-ALMando/CipherLibrary.git]
- **Install dependencies (if any).**

- **Build the project in Visual Studio or your preferred IDE.**

Start using the library by creating cipher objects using the CipherFactoryProvider to easily choose and apply different ciphers.

This library is ideal for anyone looking to implement basic encryption algorithms in a clean, well-organized C# project. It is suitable for use in educational settings, cryptography experiments, or any application that requires cryptographic functionality.

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace AES
{
	class Program
	{
		static void Main(string[] args)
		{
			string original = "Data to encrypt";

			using (Aes aes = Aes.Create())
			{
				
			}
		}

		static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			// Check parameters
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");
			
			byte[] encrypted;

			// Create Aes Object with specified key and IV
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				// Create an ecryptor to perform the stream transform
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for encryption
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							// Write all data to stream
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream
			return encrypted;
		}

		static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			string plainText = null;

			// Create Aes object with key and IV
			using (Aes AesAlg = Aes.Create())
			{
				AesAlg.Key = Key;
				AesAlg.IV = IV;

				// Create Decryptor to perform the stream transform
				ICryptoTransform decryptor = AesAlg.CreateDecryptor(AesAlg.Key, AesAlg.IV);

				// Create the streams used for decryption
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							// Read and decrypt bytes from decryption stream to string
							plainText = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			return plainText;
		}
	}
}

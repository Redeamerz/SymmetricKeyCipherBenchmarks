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

		static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
		{
			// Check parameters
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (key == null || key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (iv == null || iv.Length <= 0)
				throw new ArgumentNullException("IV");
			byte[] encrypted;

			// Create Aes Object with specified key and IV
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;

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
	}
}

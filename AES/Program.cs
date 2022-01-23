using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AES
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris non placerat magna. Donec placerat ac urna id semper. Quisque at egestas quam.";

			Stopwatch stopwatch = new Stopwatch();

			Process currentProcess = Process.GetCurrentProcess();

			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				stopwatch.Start();
				byte[] encrypted = null;
				// Encrypt string to array of bytes
				currentProcess.Refresh();
				float beginMemoryEncrytion = currentProcess.PrivateMemorySize64;
				for (int i = 0; i < 10000; i++)
				{
					encrypted = EncryptStringToBytes_Aes(original, aes.Key, aes.IV);
				}
				currentProcess.Refresh();
				float endMemoryEncryption = currentProcess.PrivateMemorySize64;
				Console.WriteLine("Encryption memory used: {0}KB", ((endMemoryEncryption - beginMemoryEncrytion) / 1000000).ToString("0.00"));

				int encryptionTime = (int)stopwatch.ElapsedMilliseconds;

				stopwatch.Reset();
				stopwatch.Start();
				string decrypted = null;
				// Decrypt bytes to string
				currentProcess.Refresh();
				float beginMemoryDencrytion = currentProcess.PrivateMemorySize64;
				for (int i = 0; i < 10000; i++)
				{
					decrypted = DecryptStringFromBytes_Aes(encrypted, aes.Key, aes.IV);
				}
				currentProcess.Refresh();
				float endMemoryDencryption = currentProcess.PrivateMemorySize64;
				Console.WriteLine("Encryption memory used: {0}KB", ((endMemoryDencryption - beginMemoryDencrytion) / 1000000).ToString("0.00"));

				stopwatch.Stop();

				int decryptionTime = (int)stopwatch.ElapsedMilliseconds;

				int totalTime = encryptionTime + decryptionTime;

				Console.WriteLine("");
				Console.WriteLine("Original: {0}", original);
				Console.WriteLine("");
				Console.WriteLine("Encrypted: {0}", Encoding.UTF8.GetString(encrypted));
				Console.WriteLine("");
				Console.WriteLine("Decrypted: {0}", decrypted);
				Console.WriteLine("");
				Console.WriteLine("Encryption time: {0} ms", encryptionTime);
				Console.WriteLine("Decryption time: {0} ms", decryptionTime);
				Console.WriteLine("Total time: {0} ms", totalTime);
			}
		}

		private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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

		private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
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
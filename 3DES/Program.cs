using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _3DES
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris non placerat magna. Donec placerat ac urna id semper. Quisque at egestas quam.";

			Stopwatch stopwatch = new Stopwatch();

			Process currentProcess = Process.GetCurrentProcess();

			using (TripleDES tdes = TripleDES.Create())
			{
				byte[] encrypted = null;
				float beginMemoryEncrytion = currentProcess.PrivateMemorySize64;
				stopwatch.Start();
				
				for (int i = 0; i < 1000000; i++)
				{
					encrypted = EncryptStringToBytes_3DES(original, tdes.Key, tdes.IV);
				}
				currentProcess.Refresh();
				float endMemoryEncryption = currentProcess.PrivateMemorySize64;
				Console.WriteLine("Encryption memory used: {0}KB", ((endMemoryEncryption - beginMemoryEncrytion) / 1000000).ToString("0.00"));

				int encryptionTime = (int)stopwatch.ElapsedMilliseconds;
				stopwatch.Reset();
				string decrypted = null;
				float beginMemoryDencrytion = currentProcess.PrivateMemorySize64;
				stopwatch.Start();

				for (int i = 0; i < 1000000; i++)
				{
					decrypted = DecryptStringToBytes_3DES(encrypted, tdes.Key, tdes.IV);
				}
				currentProcess.Refresh();
				float endMemoryDencryption = currentProcess.PrivateMemorySize64;
				Console.WriteLine("Encryption memory used: {0}KB", ((endMemoryDencryption - beginMemoryDencrytion) / 1000000).ToString("0.00"));

				int decryptionTime = (int)stopwatch.ElapsedMilliseconds;

				stopwatch.Stop();

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

		static byte[] EncryptStringToBytes_3DES(string plainText, byte[] Key, byte[] IV)
		{
			byte[] encrypted;

			// Create 3DES service provider
			using (TripleDES tdes = TripleDES.Create())
			{
				tdes.Key = Key;
				tdes.IV = IV;

				// Create encrypter
				ICryptoTransform encryptor = tdes.CreateEncryptor(Key, IV);

				// Create MemoryStream
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter sw = new StreamWriter(cs))
						{
							sw.Write(plainText);
						}
						encrypted = ms.ToArray();
					}
				}
			}
			return encrypted;
		}

		static string DecryptStringToBytes_3DES(byte[] cipherText, byte[] Key, byte[] IV)
		{
			string plainText = null;

			// Create 3DES service provider
			using (TripleDES tdes = TripleDES.Create())
			{
				tdes.Key = Key;
				tdes.IV = IV;
				// Create decryptor
				ICryptoTransform decryptor = tdes.CreateDecryptor(Key, IV);

				// Create MemoryStream
				using (MemoryStream ms = new MemoryStream(cipherText))
				{
					using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader reader = new StreamReader(cs))
						{
							plainText = reader.ReadToEnd();
						}
					}
				}
			}
			return plainText;
		}
	}
}

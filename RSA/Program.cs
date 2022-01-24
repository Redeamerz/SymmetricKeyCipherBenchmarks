using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace RSACS
{
	public class RSAEncryption
	{
		static void Main(string[] args)
		{
			string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris non placerat magna. Donec placerat ac urna id semper. Quisque at egestas quam.";

			Stopwatch stopwatch = new Stopwatch();

			Process currentProcess = Process.GetCurrentProcess();

			using (RSA rsa = RSA.Create())
			{
				string encrypted = null;
				float beginMemoryEncrytion = currentProcess.PrivateMemorySize64;
				stopwatch.Start();

				for (int i = 0; i < 10000; i++)
				{
					encrypted = EncryptStringToBytes_RSA(original, rsa.ExportParameters(false));
				}
				currentProcess.Refresh();
				float endMemoryEncryption = currentProcess.PrivateMemorySize64;
				Console.WriteLine("Encryption memory used: {0}KB", ((endMemoryEncryption - beginMemoryEncrytion) / 1000000).ToString("0.00"));

				int encryptionTime = (int)stopwatch.ElapsedMilliseconds;
				stopwatch.Reset();
				string decrypted = null;
				float beginMemoryDencrytion = currentProcess.PrivateMemorySize64;
				stopwatch.Start();

				for (int i = 0; i < 10000; i++)
				{
					decrypted = DecryptStringToBytes_RSA(encrypted, rsa.ExportParameters(true));
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
				Console.WriteLine("Encrypted: {0}", encrypted);
				Console.WriteLine("");
				Console.WriteLine("Decrypted: {0}", decrypted);
				Console.WriteLine("");
				Console.WriteLine("Encryption time: {0} ms", encryptionTime);
				Console.WriteLine("Decryption time: {0} ms", decryptionTime);
				Console.WriteLine("Total time: {0} ms", totalTime);
			}
		}

		public static string EncryptStringToBytes_RSA(string plainText, RSAParameters Key)
		{
			string encrypted = null;

			// Create RSA service provider
			using (RSA rsa = RSA.Create())
			{
				rsa.ImportParameters(Key);
				var byteData = Encoding.UTF8.GetBytes(plainText);
				var encryptedData = rsa.Encrypt(byteData, RSAEncryptionPadding.OaepSHA256);
				encrypted = Convert.ToBase64String(encryptedData);

			}
			return encrypted;
		}

		public static string DecryptStringToBytes_RSA(string cipherText, RSAParameters Key)
		{
			using (RSA rsa = RSA.Create())
			{
				rsa.ImportParameters(Key);
				var cipherByteData = Convert.FromBase64String(cipherText);
				var decryptedData = rsa.Decrypt(cipherByteData, RSAEncryptionPadding.OaepSHA256);
				return Encoding.UTF8.GetString(decryptedData);
			}
		}
	}
}

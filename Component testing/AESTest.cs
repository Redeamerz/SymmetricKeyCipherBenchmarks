using System;
using System.Security.Cryptography;
using Xunit;

namespace Component_testing
{
	public class AESTest
	{
		[Fact]
		public void CorrectDecryption()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (Aes aes = Aes.Create())
			{
				byte[] encryptedData = AES.AesEncryption.EncryptStringToBytes_Aes(original, aes.Key, aes.IV);
				string decryptedData = AES.AesEncryption.DecryptStringFromBytes_Aes(encryptedData, aes.Key, aes.IV);
				Assert.Equal(original, decryptedData);
			}
		}

		[Fact]
		public void TestIfEncryptedIsNotEqualsToOriginal()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (Aes aes = Aes.Create())
			{
				byte[] encryptedData = AES.AesEncryption.EncryptStringToBytes_Aes(original, aes.Key, aes.IV);
				Assert.False(original == encryptedData.ToString());
			}
		}

		[Fact]
		public void EmptyOriginalString()
		{
			string original = "";
			using (Aes aes = Aes.Create())
			{
				Assert.Throws<ArgumentNullException>(() => AES.AesEncryption.EncryptStringToBytes_Aes(original, aes.Key, aes.IV));
			}
		}
	}
}

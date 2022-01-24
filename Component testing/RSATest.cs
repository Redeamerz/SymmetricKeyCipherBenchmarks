using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Security.Cryptography;

namespace Component_testing
{
	public class RSATest
	{
		[Fact]
		public void CorrectDecryption()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (RSA rsa = RSA.Create())
			{
				string encryptedData = RSACS.RSAEncryption.EncryptStringToBytes_RSA(original, rsa.ExportParameters(false));
				string decryptedData = RSACS.RSAEncryption.DecryptStringToBytes_RSA(encryptedData, rsa.ExportParameters(true));
				Assert.Equal(original, decryptedData);
			}
		}

		[Fact]
		public void TestIfEncryptedIsNotEqualsToOriginal()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (RSA rsa = RSA.Create())
			{
				string encryptedData = RSACS.RSAEncryption.EncryptStringToBytes_RSA(original, rsa.ExportParameters(false));
				Assert.False(original == encryptedData);
			}
		}

		[Fact]
		public void EmptyOriginalString()
		{
			string original = "";
			using (RSA rsa = RSA.Create())
			{
				Assert.Throws<ArgumentNullException>(() => RSACS.RSAEncryption.EncryptStringToBytes_RSA(original, rsa.ExportParameters(false)));
			}
		}
	}
}

using _3DES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Component_testing
{
	public class _3DESTest
	{
		[Fact]
		public void CorrectDecryption()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (TripleDES tdes = TripleDES.Create())
			{
				byte[] encryptedData = TDESEncryption.EncryptStringToBytes_3DES(original, tdes.Key, tdes.IV);
				string decryptedData = TDESEncryption.DecryptStringToBytes_3DES(encryptedData, tdes.Key, tdes.IV);
				Assert.Equal(original, decryptedData);
			}
		}

		[Fact]
		public void TestIfEncryptedIsNotEqualsToOriginal()
		{
			string original = "Original Testing Text. Everything in here has to be correct in order for this test to complete successfully";
			using (TripleDES tdes = TripleDES.Create())
			{
				byte[] encryptedData = TDESEncryption.EncryptStringToBytes_3DES(original, tdes.Key, tdes.IV);
				Assert.False(original == encryptedData.ToString());
			}
		}

		[Fact]
		public void EmptyOriginalString()
		{
			string original = "";
			using (TripleDES tdes = TripleDES.Create())
			{
				Assert.Throws<ArgumentNullException>(() => TDESEncryption.EncryptStringToBytes_3DES(original, tdes.Key, tdes.IV));
			}
		}
	}
}

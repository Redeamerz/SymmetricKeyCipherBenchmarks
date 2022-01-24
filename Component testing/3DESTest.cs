using _3DES;
using System;
using System.Security.Cryptography;
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
		public void CorrectDecryptionLargeString()
		{
			string original = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla fermentum pellentesque lacus, nec pulvinar est tempus ut. Quisque pulvinar tortor ac augue mollis maximus. Aenean pretium malesuada diam, viverra euismod magna ultrices lobortis. Integer posuere dui eget efficitur tempor. Nam lacinia iaculis accumsan. Sed euismod consequat est, sed auctor nulla tempor id. Proin facilisis ligula ac elit rhoncus rhoncus sit amet a ligula.Sed commodo sollicitudin gravida. Aliquam porttitor est sem, nec scelerisque tellus consectetur sit amet.Phasellus finibus diam at metus dignissim porta.Lorem ipsum dolor sit amet, consectetur adipiscing elit.Aliquam volutpat tortor nisi, facilisis iaculis mi pretium id. Phasellus aliquam, enim nec dignissim molestie, sapien nibh faucibus tortor, ut sagittis libero ipsum nec erat.Duis ligula mi, viverra faucibus euismod vel, mattis vel velit. Vivamus laoreet rutrum quam, non facilisis enim blandit at.Quisque a ante at lorem volutpat malesuada ut sed lacus. Nulla at nisl mattis, rhoncus nisl vitae, vehicula risus.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Sed non cursus ligula. Cras sem tortor, hendrerit sed turpis non, commodo fringilla nisl. Maecenas interdum vestibulum volutpat. Ut id rutrum mauris. Maecenas erat lorem, venenatis egestas mauris quis, tristique vulputate risus. Mauris egestas sodales nisi, vel feugiat nisl venenatis quis. Nunc suscipit maximus ex non pulvinar. Mauris sagittis venenatis sollicitudin. In eget finibus turpis, in fermentum odio. Morbi quis ante quis orci rutrum ultricies id quis dolor. Ut vehicula sem et mi luctus, vel iaculis metus vulputate.Pellentesque eu nulla tellus. In at lacus sit amet lorem rutrum dignissim. Vivamus ante arcu, sagittis a nisl at, interdum iaculis ipsum. Aenean mattis malesuada orci in varius.Vestibulum leo ligula, blandit id suscipit non, pulvinar sed ligula. Aliquam ornare sapien nunc, eget rutrum ante consequat et. Etiam laoreet ultrices elit condimentum lacinia. Aliquam malesuada purus eu pellentesque varius. Pellentesque at purus placerat, pellentesque erat ac, pretium sem.Vestibulum mattis lectus quam, et tincidunt neque elementum sed. Quisque finibus dolor vel elementum feugiat. Integer id ipsum lorem. Nullam consectetur interdum arcu quis pellentesque. Donec sed felis rutrum, commodo nulla non, euismod ligula.Sed ac diam nec enim condimentum porta.Nulla eu dictum nulla. Integer scelerisque orci mollis ex maximus, eu consectetur sem suscipit.Sed rutrum tellus est. Aliquam nec turpis lectus. Praesent luctus sit amet tellus eu lobortis.Maecenas dictum scelerisque hendrerit. Suspendisse bibendum elementum diam, ut tincidunt velit eleifend eget. Curabitur pulvinar pulvinar urna, eu feugiat arcu finibus eget. Aenean lectus tellus, cursus ac risus eu, aliquet scelerisque urna. Donec mollis tincidunt nisl, ac consequat dui efficitur eget. Nunc vestibulum sit amet dolor vel fermentum.Mauris elementum augue ut sapien pellentesque, vel bibendum leo rhoncus.Praesent sagittis risus non tortor maximus facilisis.Nunc orci purus, placerat mollis lacus ac, aliquet tristique felis. Suspendisse posuere velit ac lacus aliquet suscipit.Curabitur volutpat tincidunt nisi, id egestas magna aliquet at.";
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
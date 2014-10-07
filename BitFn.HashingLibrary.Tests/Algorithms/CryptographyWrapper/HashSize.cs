using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;
using Crypto = System.Security.Cryptography;

namespace BitFn.HashingLibrary.Tests.Algorithms.CryptographyWrapper
{
	[TestClass]
	[CategorizeByConventionClass(typeof(HashSize))]
	public class HashSize
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenGotten_ShouldReturnFromAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var expected = fixture.Create<int>();

			var mockAlgorithm = new Mock<Crypto.HashAlgorithm>();
			mockAlgorithm.Setup(m => m.HashSize).Returns(expected);
			var algorithm = new HashingLibrary.Algorithms.CryptographyWrapper(mockAlgorithm.Object);

			// Act
			var actual = algorithm.HashSize;

			// Assert
			Assert.AreEqual(expected, actual);
			mockAlgorithm.VerifyAll();
		}
	}
}

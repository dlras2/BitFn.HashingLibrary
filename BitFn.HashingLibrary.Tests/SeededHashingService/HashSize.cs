using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.SeededHashingService
{
	[TestClass]
	[CategorizeByConventionClass(typeof(HashSize))]
	public class HashSize
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenGotten_ShouldMatchUnderlyingAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var expected = fixture.Create<int>();

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.HashSize).Returns(expected);
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = ((IHashAlgorithm)provider).HashSize;

			// Assert
			Assert.AreEqual(expected, actual);
			algorithm.VerifyAll();
		}
	}
}

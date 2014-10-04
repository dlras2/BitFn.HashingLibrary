using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashProvider.Tests.SeededHashProvider
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

			var algorithm = new Mock<IAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.HashSize).Returns(expected);
			var provider = new SeededHashProvider<int>(algorithm.Object);

			// Act
			var actual = ((IAlgorithm)provider).HashSize;

			// Assert
			Assert.AreEqual(expected, actual);
			algorithm.VerifyAll();
		}
	}
}
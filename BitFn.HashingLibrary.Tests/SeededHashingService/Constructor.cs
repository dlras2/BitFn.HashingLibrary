using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.SeededHashingService
{
	[TestClass]
	[CategorizeByConventionClass(typeof(Constructor))]
	public class Constructor
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenSeededWithBytes_ShouldMaintainLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<byte[]>();
			var expected = seed.Length;

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);

			// Act
			var provider = new SeededHashingService<int>(algorithm.Object, seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSeededWithInts_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<int[]>();
			var expected = seed.Length * sizeof(int);

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);

			// Act
			var provider = new SeededHashingService<int>(algorithm.Object, seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSeededWithStrings_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<string[]>();
			var expected = seed.Sum(s => s.Length) * sizeof(char);

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);

			// Act
			var provider = new SeededHashingService<int>(algorithm.Object, seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}
	}
}
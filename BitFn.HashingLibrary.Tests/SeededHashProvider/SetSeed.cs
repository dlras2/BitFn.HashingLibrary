using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.SeededHashProvider
{
	[TestClass]
	[CategorizeByConventionClass(typeof(SetSeed))]
	public class SetSeed
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenSetToBytes_ShouldMaintainLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<byte[]>();
			var expected = seed.Length;

			var algorithm = new Mock<IAlgorithm<int>>(MockBehavior.Strict);
			var provider = new SeededHashProvider<int>(algorithm.Object);

			// Act
			provider.SetSeed(seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSetToInts_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<int[]>();
			var expected = seed.Length * sizeof(int);

			var algorithm = new Mock<IAlgorithm<int>>(MockBehavior.Strict);
			var provider = new SeededHashProvider<int>(algorithm.Object);

			// Act
			provider.SetSeed(seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSetToStrings_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<string[]>();
			var expected = seed.Sum(s => s.Length) * sizeof(char);

			var algorithm = new Mock<IAlgorithm<int>>(MockBehavior.Strict);
			var provider = new SeededHashProvider<int>(algorithm.Object);

			// Act
			provider.SetSeed(seed);

			// Assert
			Assert.AreEqual(expected, provider.Seed.Count);
			algorithm.VerifyAll();
		}
	}
}

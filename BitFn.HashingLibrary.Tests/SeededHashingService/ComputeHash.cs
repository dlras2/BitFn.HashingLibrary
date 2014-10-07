using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.SeededHashingService
{
	[TestClass]
	[CategorizeByConventionClass(typeof(ComputeHash))]
	public class ComputeHash
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenCastAsIHashAlgorithm_ShouldReturnFromAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var expected = fixture.Create<byte[]>();

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.As<IHashAlgorithm>()
				.Setup(m => m.ComputeHash(It.IsNotNull<byte[]>()))
				.Returns(() => expected)
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = ((IHashAlgorithm)provider).ComputeHash(values);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSeedGiven_ShouldIncreaseValuesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var seed = fixture.Create<byte[]>();
			var values = fixture.Create<byte[]>();

			Contract.Assume(seed != null);
			Contract.Assume(values != null);
			var expected = seed.Length + values.Length;

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.Is<byte[]>(b => b != null && b.Length == expected)))
				.Returns(() => default(int))
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object, seed);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenSeedNotGiven_ShouldMaintainValuesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var expected = values.Length;

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.Is<byte[]>(b => b != null && b.Length == expected)))
				.Returns(() => default(int))
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(new byte[expected]);

			// Assert
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenValuesGivenAsBytes_ShouldReturnFromAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var expected = fixture.Create<int>();

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.IsNotNull<byte[]>()))
				.Returns(() => expected)
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			Assert.AreEqual(expected, actual);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenValuesGivenAsInts_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<int[]>();
			var expected = values.Length * sizeof(int);

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.Is<byte[]>(b => b != null && b.Length == expected)))
				.Returns(() => default(int))
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenValuesGivenAsInts_ShouldReturnFromAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<int[]>();
			var expected = fixture.Create<int>();

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.IsNotNull<byte[]>()))
				.Returns(() => expected)
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			Assert.AreEqual(expected, actual);
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenValuesGivenAsStrings_ShouldMaintainBytesLength()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<string[]>();
			var expected = values.Sum(s => s.Length) * sizeof(char);

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.Is<byte[]>(b => b != null && b.Length == expected)))
				.Returns(() => default(int))
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			algorithm.VerifyAll();
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenValuesGivenAsStrings_ShouldReturnFromAlgorithm()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<string[]>();
			var expected = fixture.Create<int>();

			var algorithm = new Mock<IHashAlgorithm<int>>(MockBehavior.Strict);
			algorithm.Setup(m => m.ComputeHash(It.IsNotNull<byte[]>()))
				.Returns(() => expected)
				.Verifiable();
			var provider = new SeededHashingService<int>(algorithm.Object);

			// Act
			var actual = provider.ComputeHash(values);

			// Assert
			Assert.AreEqual(expected, actual);
			algorithm.VerifyAll();
		}
	}
}
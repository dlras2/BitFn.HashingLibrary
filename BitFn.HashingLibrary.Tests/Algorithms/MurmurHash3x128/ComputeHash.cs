using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.Algorithms.MurmurHash3x128
{
	[TestClass]
	[CategorizeByConventionClass(typeof(ComputeHash))]
	public class ComputeHash
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenCastAsIAlgorithmInts_ShouldMatchByteResult()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var algorithm = new HashingLibrary.Algorithms.MurmurHash3x128();

			var expected = algorithm.ComputeHash(values);
			var expectedBytes = new Byte[expected.Length * sizeof(int)];
			Buffer.BlockCopy(expected, 0, expectedBytes, 0, expectedBytes.Length);

			// Act
			var actual = ((IAlgorithm)algorithm).ComputeHash(values);

			// Assert
			CollectionAssert.AreEqual(expectedBytes, actual);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenCastAsIAlgorithmTuple_ShouldMatchIntsResult()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var algorithm = new HashingLibrary.Algorithms.MurmurHash3x128();

			var expected = algorithm.ComputeHash(values);

			// Act
			var actual = ((IAlgorithm<Tuple<int, int, int, int>>)algorithm).ComputeHash(values);

			// Assert
			Assert.AreEqual(expected[0], actual.Item1);
			Assert.AreEqual(expected[1], actual.Item2);
			Assert.AreEqual(expected[2], actual.Item3);
			Assert.AreEqual(expected[3], actual.Item4);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenCertainValuesGiven_ShouldReachIntMaxValue()
		{
			// Arrange
			const int expected = int.MaxValue;

			var values = new byte[]
			{
				0xDA, 0xDA, 0x3B, 0xA8, 0x5B, 0x23, 0x54, 0x78, 0x11, 0xF8, 0x5B, 0xEB, 0x5B, 0xCC, 0xFF, 0x46, 0x81, 0xEB, 0x12,
				0x59, 0xB5, 0xB4, 0x13, 0xA4, 0x2D, 0x75, 0xD0, 0xA9, 0x51, 0x48, 0xDF, 0x32, 0x77, 0x2F, 0xE7, 0x27, 0x91, 0xF3,
				0x8C, 0xD0, 0xC6, 0xE8, 0xA0, 0xCF, 0x81, 0x32, 0x7B, 0x19, 0xEA, 0x44, 0xD8, 0x25, 0x9A, 0xC3, 0x3E, 0x25, 0xC7,
				0x96, 0x7B, 0xE5, 0x53, 0xAA, 0x26, 0xC9, 0x19, 0x28, 0x73, 0x2C, 0xE5, 0xD8, 0xB5, 0x8E, 0x5E, 0x42, 0x92, 0xCD,
				0x14, 0x43, 0x23
			};
			var algorithm = new HashingLibrary.Algorithms.MurmurHash3x128();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
			CollectionAssert.Contains(actual, expected);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenCertainValuesGiven_ShouldReachIntMinValue()
		{
			// Arrange
			const int expected = int.MinValue;

			var values = new byte[]
			{
				0x03, 0xA4, 0xF3, 0xED, 0x38, 0xA8, 0x0D, 0xF8, 0x01, 0xAC, 0x3D, 0xDF, 0xAD, 0xF0, 0xC4, 0x57, 0x0D, 0x24, 0xF0,
				0x9B, 0x17, 0xE3, 0xF3, 0xED, 0x81, 0x49, 0x51, 0xB6, 0xDC, 0x08, 0xC3, 0xB8, 0xFE, 0x2C, 0xFB, 0xA7, 0xD3, 0x6C,
				0x54, 0xEB, 0x22, 0x6D, 0x7E, 0x51, 0x56, 0xC4, 0x15, 0x5D, 0x56, 0xE4, 0xBB, 0x0D, 0x3C, 0x77, 0xDB, 0x20, 0xB0,
				0x05, 0x6B, 0xEE, 0x57, 0x56, 0x1C, 0xF9, 0xE9, 0x85, 0xE1, 0x80, 0xF4, 0x1D, 0x83, 0xA0, 0xD0, 0x05, 0x78, 0xAA,
				0x64, 0xA2, 0x97
			};
			var algorithm = new HashingLibrary.Algorithms.MurmurHash3x128();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
			CollectionAssert.Contains(actual, expected);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenGivenZeroBytes_ShouldReturnExpectedBits()
		{
			// Arrange
			const int expectedSize = 128;

			var values = new byte[] {};
			var algorithm = new HashingLibrary.Algorithms.MurmurHash3x128();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
			Assert.AreEqual(expectedSize, actual.Length * sizeof(int) * 8);
		}
	}
}

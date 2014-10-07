using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Run00.MsTest;

namespace BitFn.HashingLibrary.Tests.Algorithms.ReSharper
{
	[TestClass]
	[CategorizeByConventionClass(typeof(ComputeHash))]
	public class ComputeHash
	{
		[TestMethod]
		[CategorizeByConvention]
		public void WhenCastAsIHashAlgorithmInt_ShouldMatchByteResult()
		{
			// Arrange
			var fixture = new Fixture();
			var values = fixture.Create<byte[]>();
			var algorithm = new HashingLibrary.Algorithms.ReSharper();

			var expected = algorithm.ComputeHash(values);
			var expectedBytes = BitConverter.GetBytes(expected);

			// Act
			var actual = ((IHashAlgorithm)algorithm).ComputeHash(values);

			// Assert
			CollectionAssert.AreEqual(expectedBytes, actual);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenCertainValuesGiven_ShouldReachIntMaxValue()
		{
			// Arrange
			const int expected = int.MaxValue;

			var values = new byte[]
			{
				0x1A, 0x1F, 0x67, 0xE8, 0x5D, 0xBB, 0x77, 0x93, 0x56, 0x96, 0xC2, 0x2F, 0x50, 0x94, 0xC7, 0x00, 0x6C, 0x22, 0xC6,
				0x5A, 0xC5, 0x7C, 0x41, 0xE5, 0xC4, 0xD0, 0x27, 0x91, 0xAE, 0xFB, 0x5E, 0xAD, 0x23, 0xE1, 0x81, 0xB7, 0xB3, 0x20,
				0x9C, 0x30, 0xF0, 0x33, 0x1E, 0xDA, 0x80, 0xA0, 0xA1, 0xB5, 0xC6, 0xF4, 0xE6, 0x27, 0x22, 0x00, 0x5A, 0x9E, 0xDE,
				0x81, 0x24, 0x8C, 0x93, 0xE6, 0xE4
			};
			var algorithm = new HashingLibrary.Algorithms.ReSharper();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenCertainValuesGiven_ShouldReachIntMinValue()
		{
			// Arrange
			const int expected = int.MinValue;

			var values = new byte[]
			{
				0x58, 0xA8, 0x47, 0x7E, 0x3C, 0xC4, 0x7B, 0x2C, 0xF1, 0x54, 0x92, 0x08, 0x91, 0x5D, 0x72, 0x39, 0x1D, 0x05, 0xC5,
				0x75, 0x84, 0x71, 0x1D, 0xCD, 0x57, 0x5D, 0x84, 0x61, 0x6A, 0x0D, 0xD4, 0x5D, 0xBF, 0xE8, 0x4C, 0x3F, 0x39, 0xE6,
				0x7E, 0x7A, 0x41, 0xFB, 0x1C, 0xA7, 0x05, 0x30, 0x34, 0x5F, 0xE1, 0xFF, 0x9B, 0xD0, 0x09, 0x2E, 0xF2, 0xE6, 0x8A,
				0x79, 0x27, 0xDE, 0x40, 0x1A, 0xC1
			};
			var algorithm = new HashingLibrary.Algorithms.ReSharper();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[CategorizeByConvention]
		public void WhenGivenZeroBytes_ShouldReturn()
		{
			// Arrange
			var values = new byte[] {};
			var algorithm = new HashingLibrary.Algorithms.ReSharper();

			// Act
			var actual = algorithm.ComputeHash(values);

			// Assert
		}
	}
}

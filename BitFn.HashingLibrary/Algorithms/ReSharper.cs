using System;

namespace BitFn.HashingLibrary.Algorithms
{
	/// <summary>
	///     A non-cryptographic 32-bit hash algorithm replicating the <see cref="ReSharper" /> generated GetHashCode.
	/// </summary>
	/// <remarks>
	///     http://www.jetbrains.com/resharper/
	/// </remarks>
	public sealed class ReSharper : IAlgorithm<int>
	{
		int IAlgorithm.HashSize
		{
			get { return sizeof(int) * 8; }
		}

		public int ComputeHash(byte[] values)
		{
			if (values.Length == 0)
				return 0;

			// Create an array of N integers and convert the bytes
			var count = (values.Length + sizeof(int) - 1) / sizeof(int);
			var ints = new int[count];
			Buffer.BlockCopy(values, 0, ints, 0, values.Length);

			// Aggregate each value according to ReSharper's GetHashCode convention
			unchecked
			{
				var result = ints[0];
				for (var i = 1; i < ints.Length; i++)
				{
					result = (result * 397) ^ ints[i];
				}
				return result;
			}
		}

		byte[] IAlgorithm.ComputeHash(byte[] values)
		{
			var result = ComputeHash(values);
			var bytes = BitConverter.GetBytes(result);
			return bytes;
		}
	}
}

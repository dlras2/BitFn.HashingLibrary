using System;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary.Algorithms
{
	/// <summary>
	///     A non-cryptographic 32-bit hash algorithm implementing <see cref="MurmurHash2" /> in managed code.
	/// </summary>
	/// <remarks>
	///     https://code.google.com/p/smhasher/
	/// </remarks>
	public sealed class MurmurHash2 : IAlgorithm<int>
	{
		int IAlgorithm.HashSize
		{
			get { return sizeof(int) * 8; }
		}

		[ContractVerification(false)]
		public int ComputeHash(byte[] values)
		{
			if (values.Length < BlockSize)
			{
				var buffer = new byte[BlockSize];
				Buffer.BlockCopy(values, 0, buffer, 0, values.Length);
				values = buffer;
			}

			unchecked
			{
				const uint m = 0x5BD1E995;
				const int r = 24;

				var h = BitConverter.ToUInt32(values, 0);
				h ^= (uint)(values.Length - BlockSize);

				var blocks = values.Length / BlockSize;

				// Mix 4 bytes into the hash at a time
				for (var i = 1; i < blocks; i ++)
				{
					var k = BitConverter.ToUInt32(values, i * BlockSize);

					k *= m;
					k ^= k >> r;
					k *= m;

					h *= m;
					h ^= k;
				}

				// Mix the remaining 3, 2, or 1 bytes
				switch (values.Length % BlockSize)
				{
					case 3:
						h ^= (uint)(values[blocks * BlockSize + 2] << 16);
						goto case 2;
					case 2:
						h ^= (uint)(values[blocks * BlockSize + 1] << 8);
						goto case 1;
					case 1:
						h ^= (values[blocks * BlockSize]);
						h *= m;
						break;
				}

				h ^= h >> 13;
				h *= m;
				h ^= h >> 15;

				return (int)h;
			}
		}

		byte[] IAlgorithm.ComputeHash(byte[] values)
		{
			var result = ComputeHash(values);
			return BitConverter.GetBytes(result);
		}

		private const int BlockSize = sizeof(uint);
	}
}

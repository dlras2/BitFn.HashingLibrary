using System;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary.Algorithms
{
	/// <summary>
	///     A non-cryptographic 32-bit hash algorithm implementing <see cref="MurmurHash3" /> in managed code.
	/// </summary>
	/// <remarks>
	///     https://code.google.com/p/smhasher/
	/// </remarks>
	public sealed class MurmurHash3 : IHashAlgorithm<int>
	{
		int IHashAlgorithm.HashSize
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
				var h = BitConverter.ToUInt32(values, 0);

				const uint c1 = 0xCC9E2D51;
				const uint c2 = 0x1B873593;
				const int r1 = 15;
				const int r2 = 13;
				const uint m = 5;
				const uint n = 0xE6546B64;

				var blocks = values.Length / BlockSize;

				// Mix 4 bytes into the hash at a time
				for (var i = 1; i < blocks; i++)
				{
					var k1 = BitConverter.ToUInt32(values, i * BlockSize);

					k1 *= c1;
					k1 = (k1 << r1) | (k1 >> (32 - r1));
					k1 *= c2;

					h ^= k1;
					h = (h << r2) | (h >> (32 - r2));
					h = h * m + n;
				}

				// Mix the remaining 3, 2, or 1 bytes
				uint k2 = 0;
				switch (values.Length % BlockSize)
				{
					case 3:
						k2 ^= (uint)(values[blocks * BlockSize + 2] << 16);
						goto case 2;
					case 2:
						k2 ^= (uint)(values[blocks * BlockSize + 1] << 8);
						goto case 1;
					case 1:
						k2 ^= (values[blocks * BlockSize]);
						k2 *= c1;
						k2 = (k2 << r1) | (k2 >> (32 - r1));
						k2 *= c2;
						h ^= k2;
						break;
				}

				h ^= (uint)(values.Length - BlockSize);

				h ^= h >> 16;
				h *= 0x85EBCA6B;
				h ^= h >> 13;
				h *= 0xC2B2AE35;
				h ^= h >> 16;

				return (int)h;
			}
		}

		byte[] IHashAlgorithm.ComputeHash(byte[] values)
		{
			var result = ComputeHash(values);
			return BitConverter.GetBytes(result);
		}

		private const int BlockSize = sizeof(uint);
	}
}

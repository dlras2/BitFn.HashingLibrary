using System;
using System.Diagnostics.Contracts;

namespace BitFn.HashProvider.Algorithms
{
	/// <summary>
	///     A non-cryptographic 128-bit hash algorithm implementing <see cref="MurmurHash3" /> in managed code.
	/// </summary>
	/// <remarks>
	///     https://code.google.com/p/smhasher/
	/// </remarks>
	// ReSharper disable InconsistentNaming
	public sealed class MurmurHash3x128 : IAlgorithm<int[]>, IAlgorithm<Tuple<int, int, int, int>>
		// ReSharper restore InconsistentNaming
	{
		int IAlgorithm.HashSize
		{
			get { return sizeof(int) * ResultLength * 8; }
		}

		[ContractVerification(false)]
		public int[] ComputeHash(byte[] values)
		{
			Contract.Ensures(Contract.Result<int[]>().Length == ResultLength);

			if (values.Length < BlockSize)
			{
				var buffer = new byte[BlockSize];
				Buffer.BlockCopy(values, 0, buffer, 0, values.Length);
				values = buffer;
			}

			unchecked
			{
				var h1 = BitConverter.ToUInt64(values, 0);
				var h2 = BitConverter.ToUInt64(values, sizeof(ulong));
				const int r1 = 31;
				const int r2 = 33;
				const int r3 = 27;
				const ulong c1 = 0x87c37b91114253d5;
				const ulong c2 = 0x4cf5ad432745937f;
				const int c3 = 0x52dce729;
				const int c4 = 0x38495ab5;
				const ulong c5 = 0xff51afd7ed558ccd;
				const ulong c6 = 0xc4ceb9fe1a85ec53;

				var blocks = values.Length / BlockSize;

				// Mix 16 bytes into the hash at a time
				for (var i = 1; i < blocks; i++)
				{
					var k1 = BitConverter.ToUInt64(values, i * BlockSize);
					var k2 = BitConverter.ToUInt64(values, i * BlockSize + 1);

					k1 *= c1;
					k1 = (k1 << r1) | (k1 >> (64 - r1));
					k1 *= c2;
					h1 ^= k1;

					h1 = (h1 << r3) | (h1 >> (64 - r3));
					h1 += h2;
					h1 = h1 * 5 + c3;

					k2 *= c2;
					k2 = (k2 << r2) | (k2 >> (64 - r2));
					k2 *= c1;
					h2 ^= k2;

					h2 = (h2 << r1) | (h2 >> (64 - r1));
					h2 += h1;
					h2 = h2 * 5 + c4;
				}

				// Mix the remaining 15 or so bytes
				ulong k3 = 0;
				ulong k4 = 0;
				switch (values.Length % BlockSize)
				{
					case 15:
						k4 ^= (uint)(values[blocks * BlockSize + 14] << 48);
						goto case 14;
					case 14:
						k4 ^= (uint)(values[blocks * BlockSize + 13] << 40);
						goto case 13;
					case 13:
						k4 ^= (uint)(values[blocks * BlockSize + 12] << 32);
						goto case 12;
					case 12:
						k4 ^= (uint)(values[blocks * BlockSize + 11] << 24);
						goto case 11;
					case 11:
						k4 ^= (uint)(values[blocks * BlockSize + 10] << 16);
						goto case 10;
					case 10:
						k4 ^= (uint)(values[blocks * BlockSize + 9] << 8);
						goto case 9;
					case 9:
						k4 ^= (uint)(values[blocks * BlockSize + 8] << 0);
						k4 *= c2;
						k4 = (k4 << r2) | (k4 >> (64 - r2));
						k4 *= c1;
						h2 ^= k4;
						goto case 8;
					case 8:
						k3 ^= (uint)(values[blocks * BlockSize + 7] << 56);
						goto case 7;
					case 7:
						k3 ^= (uint)(values[blocks * BlockSize + 6] << 48);
						goto case 6;
					case 6:
						k3 ^= (uint)(values[blocks * BlockSize + 5] << 40);
						goto case 5;
					case 5:
						k3 ^= (uint)(values[blocks * BlockSize + 4] << 32);
						goto case 4;
					case 4:
						k3 ^= (uint)(values[blocks * BlockSize + 3] << 24);
						goto case 3;
					case 3:
						k3 ^= (uint)(values[blocks * BlockSize + 2] << 16);
						goto case 2;
					case 2:
						k3 ^= (uint)(values[blocks * BlockSize + 1] << 8);
						goto case 1;
					case 1:
						k3 ^= (uint)(values[blocks * BlockSize + 0] << 0);
						k3 *= c1;
						k3 = (k3 << r1) | (k3 >> (64 - r1));
						k3 *= c2;
						h1 ^= k3;
						break;
				}


				h1 ^= (ulong)(values.Length - BlockSize);
				h2 ^= (ulong)(values.Length - BlockSize);

				h1 += h2;
				h2 += h1;

				h1 ^= h1 >> r2;
				h1 *= c5;
				h1 ^= h1 >> r2;
				h1 *= c6;
				h1 ^= h1 >> r2;

				h2 ^= h2 >> r2;
				h2 *= c5;
				h2 ^= h2 >> r2;
				h2 *= c6;
				h2 ^= h2 >> r2;

				h1 += h2;
				h2 += h1;


				var i1 = (int)h1;
				var i2 = (int)((h1 >> 32) & 0xFFFFFFFF);
				var i3 = (int)h2;
				var i4 = (int)((h2 >> 32) & 0xFFFFFFFF);
				return new[] {i1, i2, i3, i4};
			}
		}

		byte[] IAlgorithm.ComputeHash(byte[] values)
		{
			var result = ComputeHash(values);
			var bytes = new byte[result.Length * sizeof(int)];
			Buffer.BlockCopy(result, 0, bytes, 0, bytes.Length);
			return bytes;
		}

		Tuple<int, int, int, int> IAlgorithm<Tuple<int, int, int, int>>.ComputeHash(byte[] values)
		{
			var result = ComputeHash(values);
			return new Tuple<int, int, int, int>(result[0], result[1], result[2], result[3]);
		}

		private const int BlockSize = sizeof(ulong) * 2;
		private const int ResultLength = 4;
	}
}
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace BitFn.HashingLibrary
{
	public sealed class SeededHashingService<T> : ISeededHashingService<T>
	{
		public ReadOnlyCollection<byte> Seed
		{
			get { return new ReadOnlyCollection<byte>(_seed); }
		}

		int IHashAlgorithm.HashSize
		{
			get { return _algorithm.HashSize; }
		}

		public T ComputeHash(byte[] values)
		{
			var byteValues = new byte[_seed.Length + values.Length];
			Buffer.BlockCopy(_seed, 0, byteValues, 0, _seed.Length);
			Buffer.BlockCopy(values, 0, byteValues, _seed.Length, values.Length);
			return _algorithm.ComputeHash(byteValues);
		}

		public T ComputeHash(params int[] values)
		{
			var byteValues = new byte[_seed.Length + values.Length * sizeof(int)];
			Buffer.BlockCopy(_seed, 0, byteValues, 0, _seed.Length);
			Buffer.BlockCopy(values, 0, byteValues, _seed.Length, values.Length * sizeof(int));
			return _algorithm.ComputeHash(byteValues);
		}

		public T ComputeHash(params string[] values)
		{
			var length = values.Sum(s => s != null ? s.Length : 0);
			Contract.Assume(length >= 0);

			var byteValues = new byte[_seed.Length + length * sizeof(char)];
			Buffer.BlockCopy(_seed, 0, byteValues, 0, _seed.Length);
			var offset = _seed.Length;
			foreach (var source in values.Where(s => s != null).Select(Encoding.Unicode.GetBytes))
			{
				Contract.Assume(source != null);
				Buffer.BlockCopy(source, 0, byteValues, offset, source.Length);
				offset += source.Length;
			}
			return _algorithm.ComputeHash(byteValues);
		}

		public void SetSeed(byte[] seed)
		{
			_seed = new byte[seed.Length];
			Buffer.BlockCopy(_seed, 0, seed, 0, seed.Length);
		}

		public void SetSeed(params int[] seed)
		{
			_seed = new byte[seed.Length * sizeof(int)];
			Buffer.BlockCopy(_seed, 0, seed, 0, seed.Length * sizeof(int));
		}

		public void SetSeed(params string[] seed)
		{
			var length = seed.Sum(s => s != null ? s.Length : 0);
			Contract.Assume(length >= 0);

			_seed = new byte[length * sizeof(char)];
			var offset = 0;
			foreach (var source in seed.Where(s => s != null).Select(Encoding.Unicode.GetBytes))
			{
				Contract.Assume(source != null);
				Buffer.BlockCopy(source, 0, _seed, offset, source.Length);
				offset += source.Length;
			}
		}

		byte[] IHashAlgorithm.ComputeHash(byte[] values)
		{
			var byteValues = new byte[_seed.Length + values.Length];
			Buffer.BlockCopy(_seed, 0, byteValues, 0, _seed.Length);
			Buffer.BlockCopy(values, 0, byteValues, _seed.Length, values.Length);
			return ((IHashAlgorithm)_algorithm).ComputeHash(byteValues);
		}

		public SeededHashingService(IHashAlgorithm<T> algorithm)
		{
			Contract.Requires(algorithm != null);

			_algorithm = algorithm;
			_seed = new byte[0];
		}

		public SeededHashingService(IHashAlgorithm<T> algorithm, byte[] seed)
		{
			Contract.Requires(algorithm != null);
			Contract.Requires(seed != null);

			_algorithm = algorithm;
			SetSeed(seed);
		}

		public SeededHashingService(IHashAlgorithm<T> algorithm, params int[] seed)
		{
			Contract.Requires(algorithm != null);
			Contract.Requires(seed != null);

			_algorithm = algorithm;
			SetSeed(seed);
		}

		public SeededHashingService(IHashAlgorithm<T> algorithm, params string[] seed)
		{
			Contract.Requires(algorithm != null);
			Contract.Requires(seed != null);

			_algorithm = algorithm;
			SetSeed(seed);
		}

		[ExcludeFromCodeCoverage]
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_algorithm != null);
			Contract.Invariant(_seed != null);
		}

		private readonly IHashAlgorithm<T> _algorithm;
		private byte[] _seed;
	}
}

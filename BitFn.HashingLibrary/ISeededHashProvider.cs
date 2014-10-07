using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary
{
	[ContractClass(typeof(SeededHashProviderContract<>))]
	public interface ISeededHashProvider<out T> : IHashProvider<T>
	{
		/// <summary>
		///     Gets the seed value to be prepended before computing each hash.
		/// </summary>
		ReadOnlyCollection<byte> Seed { [Pure] get; }

		/// <summary>
		///     Sets the seed value to be prepended before computing each hash.
		/// </summary>
		/// <param name="seed">The seed value to prepend.</param>
		void SetSeed(byte[] seed);

		/// <summary>
		///     Sets the seed value to be prepended before computing each hash.
		/// </summary>
		/// <param name="seed">The seed value to prepend.</param>
		void SetSeed(params int[] seed);

		/// <summary>
		///     Sets the seed value to be prepended before computing each hash.
		/// </summary>
		/// <param name="seed">The seed value to prepend.</param>
		void SetSeed(params string[] seed);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(ISeededHashProvider<>))]
	internal abstract class SeededHashProviderContract<T> : ISeededHashProvider<T>
	{
		public abstract int HashSize { get; }

		ReadOnlyCollection<byte> ISeededHashProvider<T>.Seed
		{
			get
			{
				Contract.Ensures(Contract.Result<ReadOnlyCollection<byte>>() != null);

				throw new NotImplementedException();
			}
		}

		byte[] IAlgorithm.ComputeHash(byte[] values)
		{
			throw new NotImplementedException();
		}

		T IAlgorithm<T>.ComputeHash(byte[] values)
		{
			throw new NotImplementedException();
		}

		T IHashProvider<T>.ComputeHash(params int[] values)
		{
			throw new NotImplementedException();
		}

		T IHashProvider<T>.ComputeHash(params string[] values)
		{
			throw new NotImplementedException();
		}

		void ISeededHashProvider<T>.SetSeed(byte[] seed)
		{
			Contract.Requires(seed != null);

			throw new NotImplementedException();
		}

		void ISeededHashProvider<T>.SetSeed(params int[] seed)
		{
			Contract.Requires(seed != null);

			throw new NotImplementedException();
		}

		void ISeededHashProvider<T>.SetSeed(params string[] seed)
		{
			Contract.Requires(seed != null);

			throw new NotImplementedException();
		}
	}
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashProvider
{
	[ContractClass(typeof(HashProviderContract<>))]
	public interface IHashProvider<out T> : IAlgorithm<T>
	{
		/// <summary>
		///     Computes the hash value for the specified integer array.
		/// </summary>
		/// <param name="values">The integer array to hash.</param>
		/// <returns>The hash value result.</returns>
		[Pure]
		T ComputeHash(params int[] values);

		/// <summary>
		///     Computes the hash value for the specified string array.
		/// </summary>
		/// <param name="values">The string array to hash.</param>
		/// <returns>The hash value result.</returns>
		[Pure]
		T ComputeHash(params string[] values);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(IHashProvider<>))]
	internal abstract class HashProviderContract<T> : IHashProvider<T>
	{
		int IAlgorithm.HashSize
		{
			get { throw new NotImplementedException(); }
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
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}

		T IHashProvider<T>.ComputeHash(params string[] values)
		{
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}
	}
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary
{
	[ContractClass(typeof(HashingServiceContract<>))]
	public interface IHashingService<out T> : IHashAlgorithm<T>
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
	[ContractClassFor(typeof(IHashingService<>))]
	internal abstract class HashingServiceContract<T> : IHashingService<T>
	{
		int IHashAlgorithm.HashSize
		{
			get { throw new NotImplementedException(); }
		}

		byte[] IHashAlgorithm.ComputeHash(byte[] values)
		{
			throw new NotImplementedException();
		}

		T IHashAlgorithm<T>.ComputeHash(byte[] values)
		{
			throw new NotImplementedException();
		}

		T IHashingService<T>.ComputeHash(params int[] values)
		{
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}

		T IHashingService<T>.ComputeHash(params string[] values)
		{
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}
	}
}

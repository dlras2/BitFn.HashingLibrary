using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary
{
	[ContractClass(typeof(HashAlgorithmContract<>))]
	public interface IHashAlgorithm<out T> : IHashAlgorithm
	{
		/// <summary>
		///     Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="values">The byte array to hash.</param>
		/// <returns>The hash value result.</returns>
		[Pure]
		new T ComputeHash(byte[] values);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(IHashAlgorithm<>))]
	internal abstract class HashAlgorithmContract<T> : IHashAlgorithm<T>
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
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}
	}
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary
{
	[ContractClass(typeof(AlgorithmContract<>))]
	public interface IAlgorithm<out T> : IAlgorithm
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
	[ContractClassFor(typeof(IAlgorithm<>))]
	internal abstract class AlgorithmContract<T> : IAlgorithm<T>
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
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<T>() != null);

			throw new NotImplementedException();
		}
	}
}

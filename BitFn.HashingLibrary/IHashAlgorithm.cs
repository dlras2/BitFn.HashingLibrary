using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace BitFn.HashingLibrary
{
	[ContractClass(typeof(HashAlgorithmContract))]
	public interface IHashAlgorithm
	{
		/// <summary>
		///     Gets the size, in bits, of the computed hash code.
		/// </summary>
		int HashSize { [Pure] get; }

		/// <summary>
		///     Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="values">The byte array to hash.</param>
		/// <returns>The hash value result.</returns>
		[Pure]
		byte[] ComputeHash(byte[] values);
	}

	[ExcludeFromCodeCoverage]
	[ContractClassFor(typeof(IHashAlgorithm))]
	internal abstract class HashAlgorithmContract : IHashAlgorithm
	{
		int IHashAlgorithm.HashSize
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() >= 0);

				throw new NotImplementedException();
			}
		}

		byte[] IHashAlgorithm.ComputeHash(byte[] values)
		{
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<byte[]>() != null);
			Contract.Ensures(Contract.Result<byte[]>().Length == ((IHashAlgorithm)this).HashSize * 8);

			throw new NotImplementedException();
		}
	}
}

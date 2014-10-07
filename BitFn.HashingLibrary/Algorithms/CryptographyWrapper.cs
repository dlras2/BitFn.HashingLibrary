using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace BitFn.HashingLibrary.Algorithms
{
	/// <summary>
	///     A class for thinly wrapping implementations of <see cref="System.Security.Cryptography.HashAlgorithm" /> for use as
	///     <see cref="IAlgorithm" />.
	/// </summary>
	public sealed class CryptographyWrapper : IAlgorithm<byte[]>, IDisposable
	{
		/// <summary>
		///     Gets the size, in bits, of the computed hash code. Equal to the hash size of the underlying
		///     <see cref="System.Security.Cryptography.HashAlgorithm" /> implementation.
		/// </summary>
		public int HashSize
		{
			get { return _underlyingAlgorithm.HashSize; }
		}

		public byte[] ComputeHash(byte[] values)
		{
			return _underlyingAlgorithm.ComputeHash(values);
		}

		public void Dispose()
		{
			_underlyingAlgorithm.Dispose();
		}

		byte[] IAlgorithm.ComputeHash(byte[] values)
		{
			return ComputeHash(values);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="CryptographyWrapper" /> class, using the given
		///     <paramref name="algorithm" /> as it's underlying <see cref="System.Security.Cryptography.HashAlgorithm" />
		///     implementation.
		/// </summary>
		/// <param name="algorithm">The underlying <see cref="System.Security.Cryptography.HashAlgorithm" /> implementation.</param>
		public CryptographyWrapper(HashAlgorithm algorithm)
		{
			Contract.Requires(algorithm != null);

			_underlyingAlgorithm = algorithm;
		}

		[ExcludeFromCodeCoverage]
		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_underlyingAlgorithm != null);
		}

		private readonly HashAlgorithm _underlyingAlgorithm;
	}
}

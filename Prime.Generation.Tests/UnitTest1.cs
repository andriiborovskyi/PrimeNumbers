using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prime.Generation;

namespace Prime.Generation.Tests {
	[TestClass]
	public class PrimeGeneratorsTests {

		private static List<int> PrimeNumbersEthalon = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541 };

		[TestMethod]
		public void SoEPrimegeneratorTest() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.SieveOfEratosthenes );
			List<int> excpected = PrimeNumbersEthalon.Take( 10 ).ToList();

			List<int> result = primeGenerator.Generate( 10 ).ToList();

			Assert.AreEqual( 10, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void SqrtPrimegeneratorTest() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Sqrt );
			List<int> excpected = PrimeNumbersEthalon.Take( 10 ).ToList();

			List<int> result = primeGenerator.Generate( 10 );

			Assert.AreEqual( 10, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void StaticPrimegeneratorTest() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Static );
			List<int> excpected = PrimeNumbersEthalon.Take( 10 ).ToList();

			List<int> result = primeGenerator.Generate( 10 );

			Assert.AreEqual( 10, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void ParallerPrimegeneratorTest() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Parallel );
			List<int> excpected = PrimeNumbersEthalon.Take( 10 ).ToList();

			List<int> result = primeGenerator.Generate( 10 );

			Assert.AreEqual( 10, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void SoEPrimegeneratorTestMax() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.SieveOfEratosthenes );
			List<int> excpected = PrimeNumbersEthalon;

			List<int> result = primeGenerator.Generate( 100 );

			Assert.AreEqual( excpected.Count, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void SqrtPrimegeneratorTestMax() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Sqrt );
			List<int> excpected = PrimeNumbersEthalon;

			List<int> result = primeGenerator.Generate( 100 );

			Assert.AreEqual( excpected.Count, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void StaticPrimegeneratorTestMax() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Static );
			List<int> excpected = PrimeNumbersEthalon;

			List<int> result = primeGenerator.Generate( 100 );

			Assert.AreEqual( excpected.Count, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}

		[TestMethod]
		public void ParallerPrimegeneratorTestMax() {
			IPrimeGenerator primeGenerator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Parallel );
			List<int> excpected = PrimeNumbersEthalon;

			List<int> result = primeGenerator.Generate( 100 );

			Assert.AreEqual( excpected.Count, result.Count );
			CollectionAssert.AreEquivalent( excpected, result );
		}
	}
}

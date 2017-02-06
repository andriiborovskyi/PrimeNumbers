using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.Generation {
	internal class SqrtPrimeGenerator : IPrimeGenerator {
		List<int> IPrimeGenerator.Generate( int quantity ) {
			List<int> primes = new List<int>();
			primes.Add( 2 );
			int nextPrime = 3;
			while( primes.Count < quantity ) {
				int sqrt = (int)Math.Sqrt( nextPrime );
				bool isPrime = true;
				for( int i = 0; primes[i] <= sqrt; i++ ) {
					if( nextPrime % primes[i] == 0 ) {
						isPrime = false;
						break;
					}
				}
				if( isPrime ) {
					primes.Add( nextPrime );
				}
				nextPrime += 2;
			}
			return primes;
		}
	}
}

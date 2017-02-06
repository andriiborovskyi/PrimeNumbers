using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.Generation {
	public interface IPrimeGenerator {
		List<int> Generate( int quantity );
	}
}

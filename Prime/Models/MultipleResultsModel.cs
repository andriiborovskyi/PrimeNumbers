using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prime.Models {
	public class MultipleResultsModel {
		public List<int> SieveOfEratosthenesResult { get ;set;}
		public List<int> SqrtResult { get ;set;}
		public List<int> StaticResult  {get ;set;}
	}
}
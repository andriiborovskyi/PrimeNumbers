using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Prime.Generation;
using Prime.Logging;
using Prime.Models;

namespace Prime.Controllers {
	public class HomeController : AsyncController {

		private ResourceMonitor resourceMonitor = new ResourceMonitor();

		public ActionResult Index() {
			LogResources();
			return View();
		}

		public void GeneretePrimeAsync( int algorithmType, int quantity ) {
			LogResources();
			AsyncManager.OutstandingOperations.Increment( 1 );
			PrimeGeneratorType generatorType = (PrimeGeneratorType)algorithmType;
			PerformanseEventSourse.Log.MethodStarted(
				string.Format("GeneretePrime - {0} [{1}]", generatorType.ToString(), DateTime.UtcNow.ToLongTimeString() )
			);

			IPrimeGenerator generator = PrimeGeneratorFactory.Create( generatorType );
			LogResources();
			List<int> data = generator.Generate( quantity );

			AsyncManager.Parameters["data"] = data;
			AsyncManager.OutstandingOperations.Decrement();

			PerformanseEventSourse.Log.MethodCompleted(
				string.Format( "GeneretePrime - {0} [{1}]", generatorType.ToString(), DateTime.UtcNow.ToLongTimeString() )
			);
			LogResources();
		}

		public ActionResult GeneretePrimeCompleted(List<int> data) {

			return View( "~/Views/Home/PrimeNumbers.cshtml", data );
		}

		public void GeneratePrimeParallelAsync( int quantity ) {
			LogResources();
			AsyncManager.OutstandingOperations.Increment( 3 );
			PerformanseEventSourse.Log.MethodStarted(
				string.Format( "GeneretePrimeParaller - [{0}]", DateTime.UtcNow.ToLongTimeString() )
			);

			Task.Run( () => {
				LogResources();
				IPrimeGenerator generator = PrimeGeneratorFactory.Create( PrimeGeneratorType.SieveOfEratosthenes );
				AsyncManager.Parameters["sieveOfEratosthenesResult"] = generator.Generate( quantity );
				AsyncManager.OutstandingOperations.Decrement();
				LogResources();
			} );

			Task.Run( () => {
				LogResources();
				IPrimeGenerator generator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Sqrt );
				AsyncManager.Parameters["sqrtResult"] = generator.Generate( quantity );
				AsyncManager.OutstandingOperations.Decrement();
				LogResources();
			} );

			Task.Run( () => {
				LogResources();
				IPrimeGenerator generator = PrimeGeneratorFactory.Create( PrimeGeneratorType.Static );
				AsyncManager.Parameters["staticResult"] = generator.Generate( quantity );
				AsyncManager.OutstandingOperations.Decrement();
				LogResources();
			} );
			LogResources();
		}

		public ActionResult GeneratePrimeParallelCompleted(
			List<int> sieveOfEratosthenesResult,
			List<int> sqrtResult,
			List<int> staticResult
		) {

			PerformanseEventSourse.Log.MethodCompleted(
				string.Format( "GeneretePrimeParallel - [{0}]", DateTime.UtcNow.ToLongTimeString() )
			);

			return View( "~/Views/Home/PrimeNumbersParallel.cshtml", new MultipleResultsModel {
				SieveOfEratosthenesResult = sieveOfEratosthenesResult,
				SqrtResult = sqrtResult,
				StaticResult = staticResult
			} );
		}

		private void LogResources() {
			PerformanseEventSourse.Log.ResourcesStatus(
				DateTime.UtcNow.ToLongTimeString(),
				resourceMonitor.MemorySize,
				resourceMonitor.CpuLoad
			);
		}
	}
}
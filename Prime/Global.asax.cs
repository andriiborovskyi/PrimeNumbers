using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Prime.Logging;

namespace Prime {
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes( RouteTable.Routes );
			BundleConfig.RegisterBundles( BundleTable.Bundles );
			PerformanseEventSourse.Log.Start();
		}

		protected void Application_End() {
			PerformanseEventSourse.Log.Stop();
		}

	}
}

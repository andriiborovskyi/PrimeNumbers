using System.Diagnostics.Tracing;

namespace Prime.Logging {

	[EventSource( Name = "Prime-ResourceMonitor" )]     // This is the name of my eventSource outside my program.  
	public class PerformanseEventSourse : EventSource {
		// Notice that the bodies of the events follow a pattern:  WriteEvent(ID, <args>) where ID is a unique ID 
		// Starting at 1 and giving each different event a unque one, and passing all the payload arguments on to be sent out.
		public void Start() { WriteEvent( 1 ); }
		public void MethodStarted( string MethodName ) { WriteEvent( 2, MethodName ); }

		public void MethodCompleted( string MethodName ) { WriteEvent( 3, MethodName ); }

		public void ResourcesStatus( string Time, long MemorySize, float CpuLoad ) {
			WriteEvent( 4, Time, CpuLoad, MemorySize );
		}

		public void Stop() { WriteEvent( 5 ); }

		// Typically you only create one EventSource and use it throughout your program.  Thus a static field makes sense.  
		public static PerformanseEventSourse Log = new PerformanseEventSourse();
	}
}

using System;
using System.IO;
using Diagnostics.Tracing;
using Diagnostics.Tracing.Parsers;

namespace Prime.ResourceMonitorConsole {
	class Program {
		static int Main( string[] args ) {

			var providerName = "Prime-ResourceMonitor";

			var providerGuid = TraceEventSession.GetEventSourceGuidFromName( providerName );

			if( !( TraceEventSession.IsElevated() ?? false ) ) {
				Console.WriteLine( "To turn on ETW events you need to be Administrator, please run from an Admin process." );
				return -1;
			}

			Console.WriteLine( "Starting Listening for events" );
			Console.WriteLine( "Press Ctrl+C for exit" );

			FileStream outStream;
			StreamWriter newWriter;
			TextWriter oldOut = Console.Out;
			outStream = new FileStream( "MonitoringResults_"+Guid.NewGuid()+".txt", FileMode.OpenOrCreate, FileAccess.Write );
			newWriter = new StreamWriter( outStream );
			Console.SetOut( newWriter );

			var sessionName = "My Session";
			using( var session = new TraceEventSession( sessionName, null ) ) {
				session.StopOnDispose = true;

				Console.CancelKeyPress += delegate ( object sender, ConsoleCancelEventArgs e ) { session.Dispose(); };

				using( var source = new ETWTraceEventSource( sessionName, TraceEventSourceType.Session ) ) {
					double lastMyEventMSec = 0;

					var parser = new DynamicTraceEventParser( source );
					parser.All += delegate ( TraceEvent data ) {
						if( data.ProviderGuid == providerGuid ) {
							if( data.EventName == "MethodStarted" ) {
								Console.WriteLine(
									"Started {0}",
									data.PayloadByName( "MethodName" )
								);
								lastMyEventMSec = data.TimeStampRelativeMSec;
							} else if( data.EventName == "MethodCompleted" ) {
								Console.WriteLine(
									"Completed {0}",
									data.PayloadByName( "MethodName" )
								);
								Console.WriteLine( "   > Execution time = {0:f3} MSec", data.TimeStampRelativeMSec - lastMyEventMSec );
							} else if( data.EventName == "ResourcesStatus" ) {
								Console.WriteLine(
									"[{0}] CPU {1}% RAM {2}",
									data.PayloadByName( "Time" ),
									data.PayloadByName( "CpuLoad" ),
									data.PayloadByName( "MemorySize" )
								);
							} else if( data.EventName == "Stop" ) {
								source.Close();
							}
						}
					};

					session.EnableProvider( providerGuid );

					Console.WriteLine( "Starting Listening for events" );
					source.Process();
					Console.WriteLine();
					Console.WriteLine( "Stopping the collection of events." );
					Console.SetOut( oldOut );
					newWriter.Close();
					outStream.Close();
				}
			}
			return 0;

		}
	}
}

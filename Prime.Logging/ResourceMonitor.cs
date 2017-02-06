using System.Diagnostics;

namespace Prime.Logging {
	public class ResourceMonitor {
		public float CpuLoad {
			get {
				PerformanceCounter cpuCounter = new PerformanceCounter();
				cpuCounter.CategoryName = "Processor";
				cpuCounter.CounterName = "% Processor Time";
				cpuCounter.InstanceName = "_Total";
				cpuCounter.NextValue();
				return cpuCounter.NextValue();
			}
		}

		public long MemorySize {
			get {
				Process currentProcess = Process.GetCurrentProcess();
				string processName = Process.GetCurrentProcess().ProcessName;
				PerformanceCounter ramCounter = new PerformanceCounter( "Process", "Working Set - Private", processName );
				return ramCounter.RawValue / 1024 / 1024;
			}
		} 
	}
}

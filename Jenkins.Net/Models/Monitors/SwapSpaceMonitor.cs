using System;
using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Wraps hudson.node_monitors.SwapSpaceMonitor
    /// </summary>
    public class SwapSpaceMonitor : JenkinsMonitorData
    {
        public SwapSpaceMonitor(XNode node): base (node) { }

        public Int64? AvailablePhysicalMemory => Node?.TryGetValue<Int64?>("availablePhysicalMemory");

        public Int64? AvailableSwapSpace => Node?.TryGetValue<Int64?>("availableSwapSpace");

        public Int64? TotalPhysicalMemory => Node?.TryGetValue<Int64?>("totalPhysicalMemory");

        public Int64? TotalSwapSpace => Node?.TryGetValue<Int64?>("totalSwapSpace");
    }
}

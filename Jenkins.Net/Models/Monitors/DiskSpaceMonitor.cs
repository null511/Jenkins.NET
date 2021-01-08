using System;
using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Wraps hudson.node_monitors.DiskSpaceMonitor
    /// </summary>
    public class DiskSpaceMonitor : JenkinsMonitorData
    {
        public DiskSpaceMonitor(XNode node) : base(node) { }

        public Int64? Timestamp => Node?.TryGetValue<Int64?>("timestamp");

        public string Path => Node?.TryGetValue<string>("path");

        public Int64? Size => Node?.TryGetValue<Int64?>("size");
    }
}

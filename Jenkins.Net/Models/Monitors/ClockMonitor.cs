using System;
using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Wraps hudson.node_monitors.ClockMonitor
    /// </summary>
    public class ClockMonitor : JenkinsMonitorData
    {
        public ClockMonitor(XNode node) : base(node) { }

        public Int64? Diff => Node?.TryGetValue<Int64?>("diff");
    }
}

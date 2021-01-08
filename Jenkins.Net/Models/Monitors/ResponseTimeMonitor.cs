using System;
using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Wraps hudson.node_monitors.ResponseTimeMonitor
    /// </summary>
    public class ResponseTimeMonitor : JenkinsMonitorData
    {
        public ResponseTimeMonitor(XNode node) : base(node) { }

        public Int64? Timestamp => Node?.TryGetValue<Int64?>("timestamp");

        public Int64? Average => Node?.TryGetValue<Int64?>("average");
    }
}

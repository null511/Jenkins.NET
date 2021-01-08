using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Wraps hudson.node_monitors.ArchitectureMonitor
    /// </summary>
    public class ArchitectureMonitor : JenkinsMonitorData
    {
        public ArchitectureMonitor(XNode node) : base(node) { }

        public string OperatingSystem => ((XElement) Node).Value;

    }
}

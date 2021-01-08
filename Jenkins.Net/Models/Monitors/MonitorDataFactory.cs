using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    public static class MonitorDataFactory
    {
        public static IMonitorData GetInstance(XNode node)
        {
            switch (((XElement)node).Name.LocalName)
            {
                case "hudson.node_monitors.SwapSpaceMonitor":
                    return new SwapSpaceMonitor(node);
                case "hudson.node_monitors.DiskSpaceMonitor":
                    return new DiskSpaceMonitor(node);
                case "hudson.node_monitors.TemporarySpaceMonitor":
                    return new TemporarySpaceMonitor(node);
                case "hudson.node_monitors.ArchitectureMonitor":
                    return new ArchitectureMonitor(node);
                case "hudson.node_monitors.ResponseTimeMonitor":
                    return new ResponseTimeMonitor(node);
                case "hudson.node_monitors.ClockMonitor":
                    return new ClockMonitor(node);
                default:
                    return new JenkinsMonitorData(node);
            }
        }
        
    }
}
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    public interface IMonitorData
    {
        XNode Node { get; }

        string Name { get; }

        string Class { get; }
    }
}

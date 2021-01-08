using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models.Monitors
{
    /// <summary>
    /// Describes the Jenkins Label
    /// </summary>
    public class JenkinsMonitorData : IMonitorData
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node { get; }

        /// <summary>
        /// Gets the Name of the Label.
        /// </summary>
        public string Name => ((XElement) Node).Name.LocalName;

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Creates a new Label using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the AssignedLabel.</param>
        public JenkinsMonitorData(XNode node)
        {
            this.Node = node;
        }
    }
}
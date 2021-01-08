using System.Collections.Generic;
using JenkinsNET.Internal;
using System.Xml.Linq;
using JenkinsNET.Models.Monitors;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes the Jenkins Computer Executors Node
    /// </summary>
    public class JenkinsNode
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node { get; }

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the name of the Node.
        /// </summary>
        public string DisplayName => Node?.TryGetValue<string>("displayName");

        /// <summary>
        /// Gets the name of the Node.
        /// </summary>
        public string Description => Node?.TryGetValue<string>("description");

        /// <summary>
        /// Gets the number of executors.
        /// </summary>
        public int? NumExecutors => Node?.TryGetValue<int>("numExecutors");

        /// <summary>
        /// Gets if this computer has some idle executors that can take more workload.
        /// </summary>
        public bool? Idle => Node?.TryGetValue<bool>("idle");

        /// <summary>
        /// Gets if this computer is offline.
        /// </summary>
        public bool? Offline => Node?.TryGetValue<bool>("offline");

        /// <summary>
        /// Gets the cause that puts a computer offline.
        /// </summary>
        public string OfflineCauseReason => Node?.TryGetValue<string>("offlineCauseReason");

        /// <summary>
        /// Gets a collection of Actions attached to the Job.
        /// </summary>
        public IEnumerable<JenkinsAction> Actions => Node?.WrapGroup("action", n => new JenkinsAction(n));

        /// <summary>
        /// Gets a collection of Actions attached to the Job.
        /// </summary>
        public IEnumerable<JenkinsAssignedLabel> AssignedLabels => Node?.WrapGroup("assignedLabel", n => new JenkinsAssignedLabel(n));

        public IEnumerable<IMonitorData> MonitorData => Node?.WrapGroup("monitorData/*", n => MonitorDataFactory.GetInstance(n));

        //TODO
        //public IEnumerable<JenkinsExecutors> Executors => Node?.WrapGroup("executors", n => new JenkinsExecutors(n));

        /// <summary>
        /// Creates a new base Node using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Node.</param>
        public JenkinsNode(XNode node)
        {
            this.Node = node;
        }
    }
}

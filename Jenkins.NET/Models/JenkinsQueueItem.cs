using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes an item in the Jenkins Build Queue.
    /// </summary>
    public sealed class JenkinsQueueItem
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public IEnumerable<JenkinsAction> Actions => Node.WrapGroup("action", n => new JenkinsAction(n));

        public JenkinsTask Task => Node.Wrap("task", n => new JenkinsTask(n));

        public JenkinsExecutable Executable => Node.Wrap("executable", n => new JenkinsExecutable(n));

        public string Class => Node?.TryGetValue<string>("@_class");

        public int? Id => Node?.TryGetValue<int?>("id");

        /// <summary>
        /// Gets whether the Queue Item is currently blocked.
        /// </summary>
        public bool? Blocked => Node?.TryGetValue<bool?>("blocked");

        public bool? Buildable => Node?.TryGetValue<bool?>("buildable");

        public long? InQueueSince => Node?.TryGetValue<long?>("inQueueSince");

        public string Params => Node?.TryGetValue<string>("params");

        /// <summary>
        /// Gets whether the Queue Item is currently stuck.
        /// </summary>
        public bool? Stuck => Node?.TryGetValue<bool?>("stuck");

        /// <summary>
        /// Gets the URL of the Queue Item.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets whether the Queue Item has been cancelled.
        /// </summary>
        public bool? Cancelled => Node?.TryGetValue<bool?>("cancelled");


        internal JenkinsQueueItem(XNode node)
        {
            this.Node = node;
        }
    }
}

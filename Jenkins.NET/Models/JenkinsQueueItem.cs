using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsQueueItem
    {
        private readonly XNode node;

        public IEnumerable<JenkinsAction> Actions => node.WrapGroup("action", n => new JenkinsAction(n));
        public JenkinsTask Task => node.Wrap("task", n => new JenkinsTask(n));
        public JenkinsExecutable Executable => node.Wrap("executable", n => new JenkinsExecutable(n));

        public string Class => node?.TryGetValue<string>("@_class");
        public int? Id => node?.TryGetValue<int?>("id");
        public bool? Blocked => node?.TryGetValue<bool?>("blocked");
        public bool? Buildable => node?.TryGetValue<bool?>("buildable");
        public long? InQueueSince => node?.TryGetValue<long?>("inQueueSince");
        public string Params => node?.TryGetValue<string>("params");
        public bool? Stuck => node?.TryGetValue<bool?>("stuck");
        public string Url => node?.TryGetValue<string>("url");
        public bool? Cancelled => node?.TryGetValue<bool?>("cancelled");


        internal JenkinsQueueItem(XNode node)
        {
            this.node = node;
        }
    }
}

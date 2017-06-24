using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class Jenkins
    {
        private readonly XNode node;

        public IEnumerable<JenkinsJob> Jobs => node.WrapGroup("job", n => new JenkinsJob(n));
        public IEnumerable<JenkinsView> Views => node.WrapGroup("view", n => new JenkinsView(n));

        public string Class => node?.TryGetValue<string>("@_class");
        public string Mode => node?.TryGetValue<string>("mode");
        public string NodeName => node?.TryGetValue<string>("nodeName");
        public string NodeDescription => node?.TryGetValue<string>("nodeDescription");
        public int? NumExecutors => node?.TryGetValue<int?>("numExecutors");
        public bool? QuietingDown => node?.TryGetValue<bool?>("quietingDown");
        public int? SlaveAgentPort => node?.TryGetValue<int?>("slaveAgentPort");
        public bool? UseCrumbs => node?.TryGetValue<bool?>("useCrumbs");
        public bool? UseSecurity => node?.TryGetValue<bool?>("useSecurity");


        internal Jenkins(XNode node)
        {
            this.node = node;
        }
    }
}

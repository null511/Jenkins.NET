using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins instance.
    /// </summary>
    public class Jenkins
    {
        private readonly XNode node;

        /// <summary>
        /// Gets a collection of all Jobs available on the Jenkins instance.
        /// </summary>
        public IEnumerable<JenkinsJob> Jobs => node.WrapGroup("job", n => new JenkinsJob(n));

        /// <summary>
        /// Gets a collection of all Views available on the Jenkins instance.
        /// </summary>
        public IEnumerable<JenkinsView> Views => node.WrapGroup("view", n => new JenkinsView(n));

        public string Class => node?.TryGetValue<string>("@_class");

        public string Mode => node?.TryGetValue<string>("mode");

        public string NodeName => node?.TryGetValue<string>("nodeName");

        public string NodeDescription => node?.TryGetValue<string>("nodeDescription");

        /// <summary>
        /// Gets the number of available executors on the Jenkins instance.
        /// </summary>
        public int? NumExecutors => node?.TryGetValue<int?>("numExecutors");

        public bool? QuietingDown => node?.TryGetValue<bool?>("quietingDown");

        public int? SlaveAgentPort => node?.TryGetValue<int?>("slaveAgentPort");

        public bool? UseCrumbs => node?.TryGetValue<bool?>("useCrumbs");

        /// <summary>
        /// Gets whether security is enabled on the Jenkins instance.
        /// </summary>
        public bool? UseSecurity => node?.TryGetValue<bool?>("useSecurity");


        internal Jenkins(XNode node)
        {
            this.node = node;
        }
    }
}

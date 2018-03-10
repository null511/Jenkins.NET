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
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// Gets a collection of all Jobs available on the Jenkins instance.
        /// </summary>
        public IEnumerable<JenkinsJob> Jobs => Node.WrapGroup("job", n => new JenkinsJob(n));

        /// <summary>
        /// Gets a collection of all Views available on the Jenkins instance.
        /// </summary>
        public IEnumerable<JenkinsView> Views => Node.WrapGroup("view", n => new JenkinsView(n));

        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the Mode of the Jenkins instance.
        /// See <see cref="JenkinsMode"/>.
        /// </summary>
        public string Mode => Node?.TryGetValue<string>("mode");

        /// <summary>
        /// Gets the name of the Jenkins node.
        /// </summary>
        public string NodeName => Node?.TryGetValue<string>("nodeName");

        /// <summary>
        /// Gets the description of the Jenkins node.
        /// </summary>
        public string NodeDescription => Node?.TryGetValue<string>("nodeDescription");

        /// <summary>
        /// Gets the number of available executors on the Jenkins instance.
        /// </summary>
        public int? NumExecutors => Node?.TryGetValue<int?>("numExecutors");

        /// <summary>
        /// Gets whether the Jenkins instance is in 'Quiet Down' mode.
        /// </summary>
        public bool? QuietingDown => Node?.TryGetValue<bool?>("quietingDown");

        /// <summary>
        /// Gets the port number used for Slave Agents on the Jenkins instance.
        /// </summary>
        public int? SlaveAgentPort => Node?.TryGetValue<int?>("slaveAgentPort");

        /// <summary>
        /// Gets whether Crumb Validation is enabled on the Jenkins instance.
        /// </summary>
        public bool? UseCrumbs => Node?.TryGetValue<bool?>("useCrumbs");

        /// <summary>
        /// Gets whether security is enabled on the Jenkins instance.
        /// </summary>
        public bool? UseSecurity => Node?.TryGetValue<bool?>("useSecurity");


        internal Jenkins(XNode node)
        {
            this.Node = node;
        }
    }
}

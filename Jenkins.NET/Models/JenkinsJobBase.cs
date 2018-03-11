using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes the base of a Jenkins Job.
    /// </summary>
    public class JenkinsJobBase
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        public IEnumerable<JenkinsAction> Actions => Node?.WrapGroup("action", n => new JenkinsAction(n));

        public string DisplayName => Node?.TryGetValue<string>("displayName");

        public string DisplayNameOrNull => Node?.TryGetValue<string>("displayNameOrNull");

        public string FullDisplayName => Node?.TryGetValue<string>("fullDisplayName");

        public string FullName => Node?.TryGetValue<string>("fullName");

        public string Name => Node?.TryGetValue<string>("name");

        public string Url => Node?.TryGetValue<string>("url");

        //public JenkinsHealthReport HealthReport => Node?.TryGetValue<JenkinsHealthReport>("healthReport");


        /// <summary>
        /// Creates a new base Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Jenkins Job.</param>
        public JenkinsJobBase(XNode node)
        {
            this.Node = node;
        }
    }
}

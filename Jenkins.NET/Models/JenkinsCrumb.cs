using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Crumb.
    /// </summary>
    public class JenkinsCrumb
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        public string Crumb => Node?.TryGetValue<string>("crumb");

        public string CrumbRequestField => Node?.TryGetValue<string>("crumbRequestField");


        internal JenkinsCrumb(XNode node)
        {
            this.Node = node;
        }
    }
}

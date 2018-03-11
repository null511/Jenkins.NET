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

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the security crumb token.
        /// </summary>
        public string Crumb => Node?.TryGetValue<string>("crumb");

        /// <summary>
        /// Gets the name of the HTTP request header to be used when sending crumbs.
        /// </summary>
        public string CrumbRequestField => Node?.TryGetValue<string>("crumbRequestField");


        internal JenkinsCrumb(XNode node)
        {
            this.Node = node;
        }
    }
}

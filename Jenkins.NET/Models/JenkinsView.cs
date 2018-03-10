using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins View.
    /// </summary>
    public class JenkinsView
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// The name of the Jenkins View.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// The URL of the Jenkins View.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");


        internal JenkinsView(XNode node)
        {
            this.Node = node;
        }
    }
}

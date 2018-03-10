using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a previous Jenkins Job build.
    /// </summary>
    public sealed class JenkinsPreviousBuild
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// The number of the Jenkins Build.
        /// </summary>
        public int? Number => Node?.TryGetValue<int?>("number");

        /// <summary>
        /// The URL of the Jenkins Build status.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");


        internal JenkinsPreviousBuild(XNode node)
        {
            this.Node = node;
        }
    }
}

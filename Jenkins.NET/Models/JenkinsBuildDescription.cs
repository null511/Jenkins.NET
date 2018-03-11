using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Short description of a Jenkins build.
    /// </summary>
    public class JenkinsBuildDescription
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the Number of the Jenkins Build.
        /// </summary>
        public int? Number => Node?.TryGetValue<int?>("number");

        /// <summary>
        /// Gets the URL of the Jenkins Build.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");


        internal JenkinsBuildDescription(XNode node)
        {
            this.Node = node;
        }
    }
}

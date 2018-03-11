using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Executable.
    /// </summary>
    public class JenkinsExecutable
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
        /// Gets the Number of the Executable.
        /// </summary>
        public int? Number => Node?.TryGetValue<int?>("number");

        /// <summary>
        /// Gets the URL of the Executable.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");


        internal JenkinsExecutable(XNode node)
        {
            this.Node = node;
        }
    }
}

using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Project.
    /// </summary>
    public class JenkinsProject
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the name of the Job.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the full URL of the Job description.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets the status color of the Job.
        /// </summary>
        public string Color => Node?.TryGetValue<string>("color");


        /// <summary>
        /// Creates a new Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Jenkins Job.</param>
        public JenkinsProject(XNode node)
        {
            this.Node = node;
        }
    }
}

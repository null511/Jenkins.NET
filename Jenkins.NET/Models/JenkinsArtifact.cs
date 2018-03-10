using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Artifact.
    /// </summary>
    public sealed class JenkinsArtifact
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// Gets the Display Path of the Jenkins Artifact.
        /// </summary>
        public string DisplayPath => Node?.TryGetValue<string>("displayPath");

        /// <summary>
        /// Gets the Relative Path of the Jenkins Artifact.
        /// </summary>
        public string RelativePath => Node?.TryGetValue<string>("relativePath");

        /// <summary>
        /// Gets the File Name of the Jenkins Artifact.
        /// </summary>
        public string FileName => Node?.TryGetValue<string>("fileName");


        internal JenkinsArtifact(XNode node)
        {
            this.Node = node;
        }
    }
}

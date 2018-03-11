using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Task.
    /// </summary>
    public class JenkinsTask
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
        /// Gets the Name of the Task.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the URL of the Task.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets the Color used when displaying the Task.
        /// </summary>
        public string Color => Node?.TryGetValue<string>("color");


        internal JenkinsTask(XNode node)
        {
            this.Node = node;
        }
    }
}

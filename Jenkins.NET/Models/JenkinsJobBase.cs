using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes the base of a Jenkins Job.
    /// </summary>
    public class JenkinsJobBase : IJenkinsJob
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
        /// Gets a collection of Actions attached to the Job.
        /// </summary>
        public IEnumerable<JenkinsAction> Actions => Node?.WrapGroup("action", n => new JenkinsAction(n));

        /// <summary>
        /// Gets the Display Name of the Job.
        /// </summary>
        public string DisplayName => Node?.TryGetValue<string>("displayName");

        /// <summary>
        /// Gets the Nullable Display Name of the Job.
        /// </summary>
        public string DisplayNameOrNull => Node?.TryGetValue<string>("displayNameOrNull");

        /// <summary>
        /// Gets the Full Display Name of the Job.
        /// </summary>
        public string FullDisplayName => Node?.TryGetValue<string>("fullDisplayName");

        /// <summary>
        /// Gets the Full Name of the Job.
        /// </summary>
        public string FullName => Node?.TryGetValue<string>("fullName");

        /// <summary>
        /// Gets the Name of the Job.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the URL of the Job.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");

        //public JenkinsHealthReport HealthReport => Node?.TryGetValue<JenkinsHealthReport>("healthReport");


        /// <summary>
        /// Creates a new base Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Job.</param>
        public JenkinsJobBase(XNode node)
        {
            this.Node = node;
        }
    }
}

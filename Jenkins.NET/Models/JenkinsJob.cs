using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Job.
    /// </summary>
    public sealed class JenkinsJob
    {
        private readonly XNode node;

        public string Class => node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the name of the Job.
        /// </summary>
        public string Name => node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the full URL of the Job description.
        /// </summary>
        public string Url => node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets the status color of the Job.
        /// </summary>
        public string Color => node?.TryGetValue<string>("color");


        internal JenkinsJob(XNode node)
        {
            this.node = node;
        }
    }
}

using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins View.
    /// </summary>
    public class JenkinsView
    {
        private readonly XNode node;

        /// <summary>
        /// The name of the Jenkins View.
        /// </summary>
        public string Name => node?.TryGetValue<string>("name");

        /// <summary>
        /// The URL of the Jenkins View.
        /// </summary>
        public string Url => node?.TryGetValue<string>("url");


        internal JenkinsView(XNode node)
        {
            this.node = node;
        }
    }
}

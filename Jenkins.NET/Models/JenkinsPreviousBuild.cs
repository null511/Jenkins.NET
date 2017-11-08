using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a previous Jenkins Job build.
    /// </summary>
    public sealed class JenkinsPreviousBuild
    {
        private readonly XNode node;

        /// <summary>
        /// The number of the Jenkins Build.
        /// </summary>
        public int? Number => node?.TryGetValue<int?>("number");

        /// <summary>
        /// The URL of the Jenkins Build status.
        /// </summary>
        public string Url => node?.TryGetValue<string>("url");


        internal JenkinsPreviousBuild(XNode node)
        {
            this.node = node;
        }
    }
}

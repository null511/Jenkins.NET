using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Job Parameter.
    /// </summary>
    public sealed class JenkinsParameter
    {
        private readonly XNode node;

        /// <summary>
        /// Gets the name of the Job parameter.
        /// </summary>
        public string Name => node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the value of the Job parameter.
        /// </summary>
        public string Value => node?.TryGetValue<string>("value");


        internal JenkinsParameter(XNode node)
        {
            this.node = node;
        }
    }
}

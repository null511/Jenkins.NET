using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Job Parameter.
    /// </summary>
    public sealed class JenkinsParameter
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// Gets the name of the Job parameter.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// Gets the value of the Job parameter.
        /// </summary>
        public string Value => Node?.TryGetValue<string>("value");


        internal JenkinsParameter(XNode node)
        {
            this.Node = node;
        }
    }
}

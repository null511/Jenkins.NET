using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Job Parameter.
    /// </summary>
    public sealed class JenkinsProperty
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");


        internal JenkinsProperty(XNode node)
        {
            this.Node = node;
        }
    }
}

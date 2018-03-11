using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Action.
    /// </summary>
    public class JenkinsAction
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        public string Class => Node?.TryGetValue<string>("@_class");

        public IEnumerable<JenkinsParameter> Parameters => Node.WrapGroup("parameter", n => new JenkinsParameter(n));


        internal JenkinsAction(XNode node)
        {
            this.Node = node;
        }
    }
}

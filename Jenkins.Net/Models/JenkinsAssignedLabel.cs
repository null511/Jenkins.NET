using System.Collections.Generic;
using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes the Jenkins Label
    /// </summary>
    public class JenkinsAssignedLabel
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node { get; }

        /// <summary>
        /// Gets the Name of the Label.
        /// </summary>
        public string Name => Node?.TryGetValue<string>("name");

        /// <summary>
        /// Creates a new Label using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the AssignedLabel.</param>
        public JenkinsAssignedLabel(XNode node)
        {
            this.Node = node;
        }
    }
}
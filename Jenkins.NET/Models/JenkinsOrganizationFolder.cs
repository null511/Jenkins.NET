using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Organization Folder.
    /// </summary>
    public class JenkinsOrganizationFolder : JenkinsJobBase
    {
        public IEnumerable<JenkinsProject> Jobs => Node?.WrapGroup("job", n => new JenkinsProject(n));

        public JenkinsView PrimaryView => Node?.Wrap("primaryView", n => new JenkinsView(n));

        public IEnumerable<JenkinsView> Views => Node?.WrapGroup("view", n => new JenkinsView(n));


        /// <summary>
        /// Creates a new OrganizationFolder Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Jenkins Workflow Job.</param>
        public JenkinsOrganizationFolder(XNode node) : base(node) {}
    }
}

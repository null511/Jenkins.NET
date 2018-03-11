using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Workflow Build.
    /// </summary>
    public class JenkinsWorkflowBuild : JenkinsBuildBase
    {
        /// <summary>
        /// Gets the previously run Build of the Jenkins Job, if any.
        /// </summary>
        public JenkinsBuildDescription PreviousBuild => Node?.Wrap("previousBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets the Build of the next Jenkins Job run, if any.
        /// </summary>
        public JenkinsBuildDescription NextBuild => Node?.Wrap("nextBuild", n => new JenkinsBuildDescription(n));


        internal JenkinsWorkflowBuild(XNode node) : base(node) {}
    }
}

using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Workflow Job.
    /// </summary>
    public class JenkinsWorkflowJob : JenkinsJobBase
    {
        /// <summary>
        /// Gets the Job Description.
        /// </summary>
        public string Description => Node?.TryGetValue<string>("description");

        public bool? Buildable => Node?.TryGetValue<bool?>("buildable");

        public IEnumerable<JenkinsBuildDescription> Builds => Node?.WrapGroup("build", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription FirstBuild => Node?.Wrap("firstBuild", n => new JenkinsBuildDescription(n));

        public bool? InQueue => Node?.TryGetValue<bool?>("inQueue");

        public bool? KeepDependencies => Node?.TryGetValue<bool?>("keepDependencies");

        public JenkinsBuildDescription LastBuild => Node?.Wrap("lastBuild", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription LastCompletedBuild => Node?.Wrap("lastCompletedBuild", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription LastFailedBuild => Node?.Wrap("lastFailedBuild", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription LastStableBuild => Node?.Wrap("lastStableBuild", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription LastSuccessfulBuild => Node?.Wrap("lastSuccessfulBuild", n => new JenkinsBuildDescription(n));

        public JenkinsBuildDescription LastUnsuccessfulBuild => Node?.Wrap("lastUnsuccessfulBuild", n => new JenkinsBuildDescription(n));

        public int? NextBuildNumber => Node?.TryGetValue<int?>("nextBuildNumber");

        public IEnumerable<JenkinsProperty> Properties => Node?.WrapGroup("property", n => new JenkinsProperty(n));

        public bool? ConcurrentBuild => Node?.TryGetValue<bool?>("concurrentBuild");

        public bool? ResumeBlocked => Node?.TryGetValue<bool?>("resumeBlocked");


        /// <summary>
        /// Creates a new Workflow Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Jenkins Workflow Job.</param>
        public JenkinsWorkflowJob(XNode node) : base(node) {}
    }
}

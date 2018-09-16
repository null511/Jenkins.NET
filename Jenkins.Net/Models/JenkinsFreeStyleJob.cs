using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins FreeStyle Job.
    /// </summary>
    public class JenkinsFreeStyleJob : JenkinsJobBase
    {
        /// <summary>
        /// Gets the Job Description.
        /// </summary>
        public string Description => Node?.TryGetValue<string>("description");

        public bool? Buildable => Node?.TryGetValue<bool?>("buildable");

        /// <summary>
        /// Gets a collection of Builds attached to the Job.
        /// </summary>
        public IEnumerable<JenkinsBuildDescription> Builds => Node?.WrapGroup("build", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the First Build.
        /// </summary>
        public JenkinsBuildDescription FirstBuild => Node?.Wrap("firstBuild", n => new JenkinsBuildDescription(n));

        public bool? InQueue => Node?.TryGetValue<bool?>("inQueue");

        public bool? KeepDependencies => Node?.TryGetValue<bool?>("keepDependencies");

        /// <summary>
        /// Gets a description of the Last Build.
        /// </summary>
        public JenkinsBuildDescription LastBuild => Node?.Wrap("lastBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the Last Completed Build.
        /// </summary>
        public JenkinsBuildDescription LastCompletedBuild => Node?.Wrap("lastCompletedBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the Last Failed Build.
        /// </summary>
        public JenkinsBuildDescription LastFailedBuild => Node?.Wrap("lastFailedBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the Last Stable Build.
        /// </summary>
        public JenkinsBuildDescription LastStableBuild => Node?.Wrap("lastStableBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the Last Successful Build.
        /// </summary>
        public JenkinsBuildDescription LastSuccessfulBuild => Node?.Wrap("lastSuccessfulBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets a description of the Last Unsuccessful Build.
        /// </summary>
        public JenkinsBuildDescription LastUnsuccessfulBuild => Node?.Wrap("lastUnsuccessfulBuild", n => new JenkinsBuildDescription(n));

        /// <summary>
        /// Gets the next Build Number.
        /// </summary>
        public int? NextBuildNumber => Node?.TryGetValue<int?>("nextBuildNumber");

        /// <summary>
        /// Gets a collection of properties attached to the Job.
        /// </summary>
        public IEnumerable<JenkinsProperty> Properties => Node?.WrapGroup("property", n => new JenkinsProperty(n));

        /// <summary>
        /// Gets whether concurrent builds are enabled.
        /// </summary>
        public bool? ConcurrentBuild => Node?.TryGetValue<bool?>("concurrentBuild");

        public bool? ResumeBlocked => Node?.TryGetValue<bool?>("resumeBlocked");

        /// <summary>
        /// Gets the current Ball Color of the Job.
        /// </summary>
        public string Color => Node?.TryGetValue<string>("color");

        /// <summary>
        /// Gets the full Java class name of the SCM.
        /// </summary>
        public string SCM_Class => Node?.TryGetValue<string>("scm/@_class");


        /// <summary>
        /// Creates a new FreeStyle Job using the provided XML node.
        /// </summary>
        /// <param name="node">An XML node describing the Jenkins FreeStyle Job.</param>
        public JenkinsFreeStyleJob(XNode node) : base(node) {}
    }
}

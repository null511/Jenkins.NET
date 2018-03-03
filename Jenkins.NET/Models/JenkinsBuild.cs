using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins build.
    /// </summary>
    public sealed class JenkinsBuild
    {
        private readonly XNode node;

        /// <summary>
        /// Gets a collection of actions attached to the Jenkins Job Build.
        /// </summary>
        public IEnumerable<JenkinsAction> Actions => node.WrapGroup("action", n => new JenkinsAction(n));

        /// <summary>
        /// Gets a collection of artifacts attached to the Jenkins Job Build.
        /// </summary>
        public IEnumerable<JenkinsArtifact> Artifacts => node.WrapGroup("artifact", n => new JenkinsArtifact(n));

        /// <summary>
        /// Gets the previously run Build of the Jenkins Job, if any.
        /// </summary>
        public JenkinsPreviousBuild PreviousBuild => node.Wrap("previousBuild", n => new JenkinsPreviousBuild(n));

        public string Class => node?.TryGetValue<string>("@_class");

        public int? Id => node?.TryGetValue<int?>("id");

        /// <summary>
        /// The build-number of the Jenkins Build.
        /// </summary>
        public int? Number => node?.TryGetValue<int?>("number");

        public int? QueueId => node?.TryGetValue<int?>("queueId");

        public bool? KeepLog => node?.TryGetValue<bool?>("keepLog");

        public bool? Building => node?.TryGetValue<bool?>("building");

        /// <summary>
        /// Gets the URL of the Jenkins Build.
        /// </summary>
        public string Url => node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets the Display Name of the Jenkins Build.
        /// </summary>
        public string DisplayName => node?.TryGetValue<string>("displayName");

        /// <summary>
        /// Gets the Full Display Name of the Jenkins Build.
        /// </summary>
        public string FullDisplayName => node?.TryGetValue<string>("fullDisplayName");

        /// <summary>
        /// Gets the result of the Jenkins Build.
        /// </summary>
        public string Result => node?.TryGetValue<string>("result");

        /// <summary>
        /// Gets the duration of the Jenkins Build, in milliseconds.
        /// </summary>
        public long? Duration => node?.TryGetValue<long?>("duration");

        /// <summary>
        /// Gets the estimated duration of the Jenkins Build, in milliseconds.
        /// </summary>
        public long? EstimatedDuration => node?.TryGetValue<long?>("estimatedDuration");

        public long? TimeStamp => node?.TryGetValue<long?>("timestamp");


        internal JenkinsBuild(XNode node)
        {
            this.node = node;
        }
    }
}

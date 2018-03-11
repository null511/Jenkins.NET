using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins build.
    /// </summary>
    public class JenkinsBuildBase : IJenkinsBuild
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the unique ID of the Build.
        /// </summary>
        public int? Id => Node?.TryGetValue<int?>("id");

        /// <summary>
        /// Gets the Display Name of the Jenkins Build.
        /// </summary>
        public string DisplayName => Node?.TryGetValue<string>("displayName");

        /// <summary>
        /// Gets the Full Display Name of the Jenkins Build.
        /// </summary>
        public string FullDisplayName => Node?.TryGetValue<string>("fullDisplayName");

        /// <summary>
        /// Gets the Number of the Jenkins Build.
        /// </summary>
        public int? Number => Node?.TryGetValue<int?>("number");

        /// <summary>
        /// Gets the Queue ID of the Jenkins Build.
        /// </summary>
        public int? QueueId => Node?.TryGetValue<int?>("queueId");

        /// <summary>
        /// Gets the URL of the Jenkins Build.
        /// </summary>
        public string Url => Node?.TryGetValue<string>("url");

        /// <summary>
        /// Gets a collection of actions attached to the Jenkins Job Build.
        /// </summary>
        public IEnumerable<JenkinsAction> Actions => Node?.WrapGroup("action", n => new JenkinsAction(n));

        /// <summary>
        /// Gets a collection of artifacts attached to the Jenkins Job Build.
        /// </summary>
        public IEnumerable<JenkinsArtifact> Artifacts => Node?.WrapGroup("artifact", n => new JenkinsArtifact(n));

        public bool? KeepLog => Node?.TryGetValue<bool?>("keepLog");

        /// <summary>
        /// Gets whether the Jenkins Build is currently in-progress.
        /// </summary>
        public bool? Building => Node?.TryGetValue<bool?>("building");

        /// <summary>
        /// Gets the duration of the Jenkins Build, in milliseconds.
        /// </summary>
        public long? Duration => Node?.TryGetValue<long?>("duration");

        /// <summary>
        /// Gets the estimated duration of the Jenkins Build, in milliseconds.
        /// </summary>
        public long? EstimatedDuration => Node?.TryGetValue<long?>("estimatedDuration");

        /// <summary>
        /// Gets the time that the Jenkins Build was started.
        /// </summary>
        public long? TimeStamp => Node?.TryGetValue<long?>("timestamp");

        /// <summary>
        /// Gets the result of the Jenkins Build.
        /// </summary>
        public string Result => Node?.TryGetValue<string>("result");


        public JenkinsBuildBase(XNode node)
        {
            this.Node = node;
        }
    }
}

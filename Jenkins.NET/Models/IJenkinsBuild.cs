using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins build.
    /// </summary>
    public interface IJenkinsBuild
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        XNode Node {get;}

        string Class {get;}

        int? Id {get;}

        /// <summary>
        /// Gets the Display Name of the Jenkins Build.
        /// </summary>
        string DisplayName {get;}

        /// <summary>
        /// Gets the Full Display Name of the Jenkins Build.
        /// </summary>
        string FullDisplayName {get;}

        /// <summary>
        /// Gets the Number of the Jenkins Build.
        /// </summary>
        int? Number {get;}

        /// <summary>
        /// Gets the Queue ID of the Jenkins Build.
        /// </summary>
        int? QueueId {get;}

        /// <summary>
        /// Gets the URL of the Jenkins Build.
        /// </summary>
        string Url {get;}

        /// <summary>
        /// Gets a collection of actions attached to the Jenkins Job Build.
        /// </summary>
        IEnumerable<JenkinsAction> Actions {get;}

        /// <summary>
        /// Gets a collection of artifacts attached to the Jenkins Job Build.
        /// </summary>
        IEnumerable<JenkinsArtifact> Artifacts {get;}

        bool? KeepLog {get;}

        /// <summary>
        /// Gets whether the Jenkins Build is currently in-progress.
        /// </summary>
        bool? Building {get;}

        /// <summary>
        /// Gets the duration of the Jenkins Build, in milliseconds.
        /// </summary>
        long? Duration {get;}

        /// <summary>
        /// Gets the estimated duration of the Jenkins Build, in milliseconds.
        /// </summary>
        long? EstimatedDuration {get;}

        /// <summary>
        /// Gets the time that the Jenkins Build was started.
        /// </summary>
        long? TimeStamp {get;}

        /// <summary>
        /// Gets the result of the Jenkins Build.
        /// </summary>
        string Result {get;}
    }
}

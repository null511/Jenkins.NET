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

        /// <summary>
        /// Gets the full Java class name.
        /// </summary>
        string Class {get;}

        /// <summary>
        /// Gets the unique ID of the Build.
        /// </summary>
        int? Id {get;}

        /// <summary>
        /// Gets the Display Name of the Build.
        /// </summary>
        string DisplayName {get;}

        /// <summary>
        /// Gets the Full Display Name of the Build.
        /// </summary>
        string FullDisplayName {get;}

        /// <summary>
        /// Gets the Number of the Build.
        /// </summary>
        int? Number {get;}

        /// <summary>
        /// Gets the Queue ID of the Build.
        /// </summary>
        int? QueueId {get;}

        /// <summary>
        /// Gets the URL of the Build.
        /// </summary>
        string Url {get;}

        /// <summary>
        /// Gets a collection of Actions attached to the Build.
        /// </summary>
        IEnumerable<JenkinsAction> Actions {get;}

        /// <summary>
        /// Gets a collection of Artifacts attached to the Build.
        /// </summary>
        IEnumerable<JenkinsArtifact> Artifacts {get;}

        bool? KeepLog {get;}

        /// <summary>
        /// Gets whether the Build is currently in-progress.
        /// </summary>
        bool? Building {get;}

        /// <summary>
        /// Gets the duration of the Build, in milliseconds.
        /// </summary>
        long? Duration {get;}

        /// <summary>
        /// Gets the estimated duration of the Build, in milliseconds.
        /// </summary>
        long? EstimatedDuration {get;}

        /// <summary>
        /// Gets the time that the Build was started.
        /// </summary>
        long? TimeStamp {get;}

        /// <summary>
        /// Gets the result of the Build.
        /// </summary>
        string Result {get;}
    }
}

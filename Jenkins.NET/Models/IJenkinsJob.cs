using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes the base of a Jenkins Job.
    /// </summary>
    public interface IJenkinsJob
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
        /// Gets a collection of Actions attached to the Job.
        /// </summary>
        IEnumerable<JenkinsAction> Actions {get;}

        /// <summary>
        /// Gets the Display Name of the Job.
        /// </summary>
        string DisplayName {get;}

        /// <summary>
        /// Gets the Nullable Display Name of the Job.
        /// </summary>
        string DisplayNameOrNull {get;}

        /// <summary>
        /// Gets the Full Display Name of the Job.
        /// </summary>
        string FullDisplayName {get;}

        /// <summary>
        /// Gets the Full Name of the Job.
        /// </summary>
        string FullName {get;}

        /// <summary>
        /// Gets the Name of the Job.
        /// </summary>
        string Name {get;}

        /// <summary>
        /// Gets the URL of the Job.
        /// </summary>
        string Url {get;}

        //JenkinsHealthReport HealthReport {get;}
    }
}

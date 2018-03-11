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

        string Class {get;}

        IEnumerable<JenkinsAction> Actions {get;}

        string DisplayName {get;}

        string DisplayNameOrNull {get;}

        string FullDisplayName {get;}

        string FullName {get;}

        string Name {get;}

        string Url {get;}

        //JenkinsHealthReport HealthReport {get;}
    }
}

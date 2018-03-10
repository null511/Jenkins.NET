using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    /// <summary>
    /// Describes a Jenkins Cause.
    /// Used to identify why a Jenkins Build was started.
    /// </summary>
    public class JenkinsCause
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        /// <summary>
        /// Gets the Short Description describing why the Build was started.
        /// </summary>
        public string ShortDescription => Node?.TryGetValue<string>("shortDescription");

        /// <summary>
        /// Gets the ID of the User that started the Build.
        /// </summary>
        public string UserId => Node?.TryGetValue<string>("userId");

        /// <summary>
        /// Gets the Name of the User that started the Build.
        /// </summary>
        public string UserName => Node?.TryGetValue<string>("userName");


        internal JenkinsCause(XNode node)
        {
            this.Node = node;
        }
    }
}

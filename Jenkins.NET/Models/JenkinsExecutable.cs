using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsExecutable
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XNode Node {get;}

        public string Class => Node?.TryGetValue<string>("@_class");

        public int? Number => Node?.TryGetValue<int?>("number");

        public string Url => Node?.TryGetValue<string>("url");


        internal JenkinsExecutable(XNode node)
        {
            this.Node = node;
        }
    }
}

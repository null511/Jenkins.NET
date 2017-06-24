using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsExecutable
    {
        private readonly XNode node;

        public string Class => node?.TryGetValue<string>("@_class");
        public int? Number => node?.TryGetValue<int?>("number");
        public string Url => node?.TryGetValue<string>("url");


        internal JenkinsExecutable(XNode node)
        {
            this.node = node;
        }
    }
}

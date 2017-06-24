using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsJob
    {
        private readonly XNode node;

        public string Class => node?.TryGetValue<string>("@_class");
        public string Name => node?.TryGetValue<string>("name");
        public string Url => node?.TryGetValue<string>("url");
        public string Color => node?.TryGetValue<string>("color");


        internal JenkinsJob(XNode node)
        {
            this.node = node;
        }
    }
}

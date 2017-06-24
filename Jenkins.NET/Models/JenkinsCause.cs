using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsCause
    {
        private readonly XNode node;

        public string Class => node?.TryGetValue<string>("@_class");
        public string ShortDescription => node?.TryGetValue<string>("shortDescription");
        public string UserId => node?.TryGetValue<string>("userId");
        public string UserName => node?.TryGetValue<string>("userName");


        internal JenkinsCause(XNode node)
        {
            this.node = node;
        }
    }
}

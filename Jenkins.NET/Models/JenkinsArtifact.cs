using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsArtifact
    {
        private readonly XNode node;

        public string DisplayPath => node?.TryGetValue<string>("displayPath");
        public string RelativePath => node?.TryGetValue<string>("relativePath");
        public string FileName => node?.TryGetValue<string>("fileName");


        internal JenkinsArtifact(XNode node)
        {
            this.node = node;
        }
    }
}

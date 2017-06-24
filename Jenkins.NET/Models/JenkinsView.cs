using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsView
    {
        private readonly XNode node;

        public string Name => node?.TryGetValue<string>("name");
        public string Url => node?.TryGetValue<string>("url");


        internal JenkinsView(XNode node)
        {
            this.node = node;
        }
    }
}

using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsParameter
    {
        private readonly XNode node;

        public string Name => node?.TryGetValue<string>("name");
        public string Value => node?.TryGetValue<string>("value");


        internal JenkinsParameter(XNode node)
        {
            this.node = node;
        }
    }
}

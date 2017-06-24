using JenkinsNET.Internal;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsPreviousBuild
    {
        private readonly XNode node;

        public int? Number => node?.TryGetValue<int?>("number");
        public string Url => node?.TryGetValue<string>("url");


        internal JenkinsPreviousBuild(XNode node)
        {
            this.node = node;
        }
    }
}

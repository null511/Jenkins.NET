using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsAction
    {
        private readonly XNode node;

        public IEnumerable<JenkinsParameter> Parameters => node.WrapGroup("parameter", n => new JenkinsParameter(n));

        public string Class => node?.TryGetValue<string>("@_class");


        internal JenkinsAction(XNode node)
        {
            this.node = node;
        }
    }
}

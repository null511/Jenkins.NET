using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.XPath;
using JenkinsNET.Internal;

namespace JenkinsNET.Models
{
    public class JenkinsNode
    {
        /// <summary>
        /// Gets the base XML node.
        /// </summary>
        public XElement Node { get; }

        private string name;
        private bool nameCached = false;
        public string Name
        {
            get
            {
                if (nameCached) return name;
                var node = Node.XPathSelectElement("./displayName");
                name = node?.Value;
                nameCached = true;
                return name;
            }
        }

        private string description;
        private bool descriptionCached = false;
        public string Description
        {
            get
            {
                if (descriptionCached) return description;
                var node = Node.XPathSelectElement("./description");
                description = node?.Value;
                descriptionCached = true;
                return description;
            }
        }

        private bool online;
        private bool onlineCached;
        public bool Online
        {
            get
            {
                if (onlineCached) return online;
                var node = Node.XPathSelectElement("./offline");
                online = node?.Value == "false";
                onlineCached = true;
                return online;
            }
        }

        internal JenkinsNode(XElement node)
        {
            this.Node = node;
        }
    }
}
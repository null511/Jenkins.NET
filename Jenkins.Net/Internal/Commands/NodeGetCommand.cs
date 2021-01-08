using JenkinsNET.Models;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace JenkinsNET.Internal.Commands
{
    internal class NodeGetCommand : JenkinsHttpCommand
    {
        public JenkinsNode[] Result { get; private set; }

        public NodeGetCommand(IJenkinsContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Url = NetPath.Combine(context.BaseUrl, "computer", "api/xml");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = document.XPathSelectElements("/computerSet/computer")
                        .Select(node => new JenkinsNode(node)).ToArray();
                }
            };

#if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "GET";
            };

            OnReadAsync = async (response, token) => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = document.XPathSelectElements("/computerSet/computer")
                        .Select(node => new JenkinsNode(node)).ToArray();
                }
            };
#endif
        }
    }
}

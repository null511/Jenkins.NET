using JenkinsNET.Internal;
using JenkinsNET.Models;
using System;
using System.Xml.Linq;

namespace JenkinsNET.Commands
{
    internal class QueueGetItemCommand : JenkinsHttpCommand
    {
        public JenkinsQueueItem Result {get; private set;}


        public QueueGetItemCommand(IJenkinsContext context, int itemNumber)
        {
            Url = NetPath.Combine(context.BaseUrl, "queue/item", itemNumber.ToString(), "api/xml");
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = new JenkinsQueueItem(document.Root);
                }
            };
        }
    }
}

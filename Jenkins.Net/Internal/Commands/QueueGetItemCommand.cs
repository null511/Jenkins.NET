using JenkinsNET.Models;
using System;
using System.Xml.Linq;

namespace JenkinsNET.Internal.Commands
{
    internal class QueueGetItemCommand : JenkinsHttpCommand
    {
        public JenkinsQueueItem Result {get; private set;}


        public QueueGetItemCommand(JenkinsClient client, int itemNumber) : base(client)
        {
            Path = $"queue/item/{itemNumber}/api/xml";

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

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
            };

            OnReadAsync = async (response, token) => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = new JenkinsQueueItem(document.Root);
                }
            };
        #endif
        }
    }
}

﻿using JenkinsNET.Models;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace JenkinsNET.Internal.Commands
{
    internal class QueueItemListCommand : JenkinsHttpCommand
    {
        public JenkinsQueueItem[] Result {get; private set;}


        public QueueItemListCommand(JenkinsClient client) : base(client)
        {
            Path = "queue/api/xml";

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = document.XPathSelectElements("/queue/item")
                        .Select(node => new JenkinsQueueItem(node)).ToArray();
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

                    Result = document.XPathSelectElements("/queue/item")
                        .Select(node => new JenkinsQueueItem(node)).ToArray();
                }
            };
        #endif
        }
    }
}

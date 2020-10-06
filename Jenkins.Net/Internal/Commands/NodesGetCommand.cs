using System;
using System.Collections.Generic;
using System.Diagnostics;
using JenkinsNET.Models;

namespace JenkinsNET.Internal.Commands
{
    internal class NodesGetCommand : JenkinsHttpCommand
    {
        public IEnumerable<Models.JenkinsNode> Result { get; private set; }

        public NodesGetCommand(IJenkinsContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Url = NetPath.Combine(context.BaseUrl, "computer/", "api/xml");
            
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "GET";
            };
            
            OnRead = response => {
                var document = ReadXml(response);
                Result = document.Root.WrapGroup("computer", n => new JenkinsNode(n));
            };
            
        }
        
    }
}
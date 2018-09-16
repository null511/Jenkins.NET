using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JenkinsGetCommand : JenkinsHttpCommand
    {
        public Jenkins Result {get; private set;}


        public JenkinsGetCommand(IJenkinsContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Url = NetPath.Combine(context.BaseUrl, "api/xml");

            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                var document = ReadXml(response);
                Result = new Jenkins(document.Root);
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "GET";
            };

            OnReadAsync = async (response, token) => {
                var document = await ReadXmlAsync(response);
                Result = new Jenkins(document.Root);
            };
        #endif
        }
    }
}

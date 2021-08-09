using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class ViewCreateCommand : JenkinsHttpCommand
    {
        public ViewCreateCommand(IJenkinsContext context, string viewName, JenkinsProject view)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentException("Value cannot be empty!", nameof(viewName));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            Url = NetPath.Combine(context.BaseUrl, "createView")
                  + NetPath.Query(new { name = viewName });

            UserName = context.UserName;
            Password = context.ApiToken;
            Crumb = context.Crumb;

            OnWrite = request =>
            {
                request.Method = "POST";
                request.ContentType = "application/xml";
                WriteXml(request, view.Node);
            };

#if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
                request.ContentType = "application/xml";
                await WriteXmlAsync(request, view.Node, token);
            };
#endif
        }
    }
}
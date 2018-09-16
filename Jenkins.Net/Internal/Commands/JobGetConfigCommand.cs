using JenkinsNET.Models;
using System;

#if NET_ASYNC
using System.Threading.Tasks;
#endif

namespace JenkinsNET.Internal.Commands
{
    internal class JobGetConfigCommand : JenkinsHttpCommand
    {
        public JenkinsProject Result {get; private set;}


        public JobGetConfigCommand(IJenkinsContext context, string jobName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "config.xml");

            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                var document = ReadXml(response);
                Result = new JenkinsProject(document.Root);
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "GET";
            };

            OnReadAsync = async (response, token) => {
                var document = await ReadXmlAsync(response);
                Result = new JenkinsProject(document.Root);
            };
        #endif
        }
    }
}

using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobUpdateConfigurationCommand : JenkinsHttpCommand
    {
        public JobUpdateConfigurationCommand(IJenkinsContext context, string jobName, JenkinsProject job)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            if (job == null)
                throw new ArgumentNullException(nameof(job));

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "config.xml");

            UserName = context.UserName;
            Password = context.Password;
            ExtraHeaders = context.ExtraHeaders;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "POST";
                request.ContentType = "application/xml";
                WriteXml(request, job.Node);
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
                request.ContentType = "application/xml";
                await WriteXmlAsync(request, job.Node, token);
            };
       #endif
        }
    }
}

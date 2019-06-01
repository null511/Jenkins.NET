using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobCreateCommand : JenkinsHttpCommand
    {
        public JobCreateCommand(JenkinsClient client, string jobName, JenkinsProject job) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            if (job == null)
                throw new ArgumentNullException(nameof(job));

            var query = NetPath.Query(new {name = jobName});
            Path = $"createItem{query}";

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

using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobDeleteCommand : JenkinsHttpCommand
    {
        public JobDeleteCommand(JenkinsClient client, string jobName) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            Path = $"job/{jobName}/doDelete";

            OnWrite = request => {
                request.Method = "POST";
                request.ContentLength = 0;
                request.AllowAutoRedirect = false;
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
                request.ContentLength = 0;
                request.AllowAutoRedirect = false;
            };
        #endif
        }
    }
}

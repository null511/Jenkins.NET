using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobGetCommand<T> : JenkinsHttpCommand where T : class, IJenkinsJob
    {
        public T Result {get; private set;}


        public JobGetCommand(JenkinsClient client, string jobName) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            Path = $"job/{jobName}/api/xml";

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                var document = ReadXml(response);
                Result = Activator.CreateInstance(typeof(T), document.Root) as T;
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "GET";
            };

            OnReadAsync = async (response, token) => {
                var document = await ReadXmlAsync(response);
                Result = Activator.CreateInstance(typeof(T), document.Root) as T;
            };
        #endif
        }
    }
}

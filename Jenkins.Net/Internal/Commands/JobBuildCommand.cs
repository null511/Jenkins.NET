using JenkinsNET.Exceptions;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobBuildCommand : JenkinsHttpCommand
    {
        public JenkinsBuildResult Result {get; internal set;}

        public JobBuildCommand(JenkinsClient client, string jobName) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            Path = $"job/{jobName}/build?delay=0sec";

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    throw new JenkinsJobBuildException($"Expected HTTP status code 201 but found {(int)response.StatusCode}!");

                Result = new JenkinsBuildResult {
                    QueueItemUrl = response.GetResponseHeader("Location")
                };
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
            };

            OnReadAsync = async (response, token) => {
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    throw new JenkinsJobBuildException($"Expected HTTP status code 201 but found {(int)response.StatusCode}!");

                Result = new JenkinsBuildResult {
                    QueueItemUrl = response.GetResponseHeader("Location")
                };
            };
        #endif
        }
    }
}

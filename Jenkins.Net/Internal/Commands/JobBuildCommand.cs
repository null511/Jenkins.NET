using JenkinsNET.Exceptions;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobBuildCommand : JenkinsHttpCommand
    {
        public JenkinsBuildResult Result {get; internal set;}

        public JobBuildCommand(IJenkinsContext context, string jobName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "build?delay=0sec");
            UserName = context.UserName;
            Password = context.Password;
            ExtraHeaders = context.ExtraHeaders;
            Crumb = context.Crumb;

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

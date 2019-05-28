using JenkinsNET.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace JenkinsNET.Internal.Commands
{
    internal class JobBuildWithParametersCommand : JenkinsHttpCommand
    {
        public JenkinsBuildResult Result {get; internal set;}

        public JobBuildWithParametersCommand(JenkinsClient client, string jobName, IDictionary<string, string> jobParameters) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (jobParameters == null)
                throw new ArgumentNullException(nameof(jobParameters));

            var _params = new Dictionary<string, string>(jobParameters) {
                ["delay"] = "0sec",
            };

            var query = new StringWriter();
            WriteJobParameters(query, _params);

            Path = $"job/{jobName}/buildWithParameters?{query}";

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    throw new JenkinsJobBuildException($"Expected HTTP status code 201 but found {(int)response.StatusCode}!");

                Result = new JenkinsBuildResult {
                    QueueItemUrl = response.GetResponseHeader("Location"),
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
                    QueueItemUrl = response.GetResponseHeader("Location"),
                };
            };
        #endif
        }

        private void WriteJobParameters(TextWriter writer, IDictionary<string, string> jobParameters)
        {
            var isFirst = true;
            foreach (var pair in jobParameters) {
                if (isFirst) {
                    isFirst = false;
                }
                else {
                    writer.Write('&');
                }

                var encodedName = HttpUtility.UrlEncode(pair.Key);
                var encodedValue = HttpUtility.UrlEncode(pair.Value);

                writer.Write(encodedName);
                writer.Write('=');
                writer.Write(encodedValue);
            }
        }
    }
}

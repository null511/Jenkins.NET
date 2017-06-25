using JenkinsNET.Exceptions;
using JenkinsNET.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace JenkinsNET.Commands
{
    internal class JobBuildWithParametersCommand : JenkinsHttpCommand
    {
        public JenkinsBuildResult Result {get; internal set;}

        public JobBuildWithParametersCommand(IJenkinsContext context, string jobName, IDictionary<string, string> jobParameters)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (jobParameters == null)
                throw new ArgumentNullException(nameof(jobParameters));

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "buildWithParameters?delay=0sec");
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "POST";

                if (jobParameters?.Any() ?? false) { 
                    using (var stream = request.GetRequestStream())
                    using (var writer = new StreamWriter(stream, Encoding.UTF8)) {
                        WriteJobParameters(writer, jobParameters);
                    }
                }
            };

            OnRead = response => {
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    throw new JenkinsJobBuildException($"Expected HTTP status code 201 but found {(int)response.StatusCode}!");

                Result = new JenkinsBuildResult();
                Result.QueueItemUrl = response.GetResponseHeader("Location");
            };
        }

        private void WriteJobParameters(StreamWriter writer, IDictionary<string, string> jobParameters)
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

using System;
using System.IO;
using System.Text;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildTextCommand : JenkinsHttpCommand
    {
        public string Result {get; private set;}


        public BuildTextCommand(JenkinsClient client, string jobName, string buildNumber) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Path = $"job/{jobName}/{buildNumber}/consoleText";

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    using (var reader = new StreamReader(stream, encoding)) {
                        Result = reader.ReadToEnd();
                    }
                }
            };

        #if NET_ASYNC
            OnReadAsync = async (response, token) => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    Result = await stream.ReadToEndAsync(encoding, token);
                }
            };
        #endif
        }
    }
}

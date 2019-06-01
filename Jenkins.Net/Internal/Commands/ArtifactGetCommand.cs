using System;
using System.IO;

namespace JenkinsNET.Internal.Commands
{
    internal class ArtifactGetCommand : JenkinsHttpCommand
    {
        public MemoryStream Result {get; private set;}


        public ArtifactGetCommand(JenkinsClient client, string jobName, string buildNumber, string filename) : base(client)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("'filename' cannot be empty!");

            var urlFilename = filename.Replace('\\', '/');
            Path = $"job/{jobName}/{buildNumber}/artifact/{urlFilename}";

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    try {
                        Result = new MemoryStream();
                        stream.CopyTo(Result);
                        Result.Seek(0, SeekOrigin.Begin);
                    }
                    catch {
                        Result?.Dispose();
                        throw;
                    }
                }
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
            };

            OnReadAsync = async (response, token) => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    try {
                        Result = new MemoryStream();
                        await stream.CopyToAsync(Result);
                        Result.Seek(0, SeekOrigin.Begin);
                    }
                    catch {
                        Result?.Dispose();
                        throw;
                    }
                }
            };
        #endif
        }
    }
}

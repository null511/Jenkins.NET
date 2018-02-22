using System;
using System.IO;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildOutputCommand : JenkinsHttpCommand
    {
        public string Result {get; private set;}


        public BuildOutputCommand(IJenkinsContext context, string jobName, string buildNumber)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "consoleText");
            UserName = context.UserName;
            Password = context.Password;

            OnReadAsync = async response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    using (var reader = new StreamReader(stream)) {
                        Result = await reader.ReadToEndAsync();
                    }
                }
            };
        }
    }
}

using System;
using System.Text;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildOutputCommand : JenkinsHttpCommand
    {
        public string Result {get; private set;}


        public BuildOutputCommand(IJenkinsContext context, string jobName, string buildNumber)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "consoleText");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnReadAsync = async (response, token) => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    Result = await stream.ReadToEndAsync(encoding, token);
                }
            };
        }
    }
}

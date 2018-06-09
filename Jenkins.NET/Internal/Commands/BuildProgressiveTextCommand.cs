using JenkinsNET.Models;
using System;
using System.IO;
using System.Text;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildProgressiveTextCommand : JenkinsHttpCommand
    {
        public JenkinsProgressiveTextResponse Result {get; private set;}


        public BuildProgressiveTextCommand(IJenkinsContext context, string jobName, string buildNumber, int start)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "logText/progressiveText");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                using (var requestStream = await request.GetRequestStreamAsync())
                using (var writer = new StreamWriter(requestStream)) {
                    await writer.WriteAsync($"start={start}");
                }
            };

            OnReadAsync = async (response, token) => {
                var hSize = response.Headers["X-Text-Size"];
                var hMoreData = response.Headers["X-More-Data"];

                if (!int.TryParse(hSize, out var _size))
                    throw new ApplicationException($"Unable to parse x-text-size header value '{hSize}'!");

                var _moreData = string.Equals(hMoreData, bool.TrueString, StringComparison.OrdinalIgnoreCase);

                Result = new JenkinsProgressiveTextResponse {
                    Size = _size,
                    MoreData = _moreData,
                };

                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    Result.Text = await stream.ReadToEndAsync(encoding, token);
                }
            };
        }
    }
}

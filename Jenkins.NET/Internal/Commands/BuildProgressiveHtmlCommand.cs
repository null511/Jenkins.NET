﻿using JenkinsNET.Models;
using System;
using System.IO;
using System.Text;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildProgressiveHtmlCommand : JenkinsHttpCommand
    {
        public JenkinsProgressiveHtmlResponse Result {get; private set;}


        public BuildProgressiveHtmlCommand(IJenkinsContext context, string jobName, string buildNumber, int start)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "logText/progressiveHtml");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                using (var requestStream = request.GetRequestStream())
                using (var writer = new StreamWriter(requestStream)) {
                    writer.Write($"start={start}");
                }
            };

            OnRead = response => {
                var hSize = response.Headers["X-Text-Size"];
                var hMoreData = response.Headers["X-More-Data"];

                if (!int.TryParse(hSize, out var _size))
                    throw new ApplicationException($"Unable to parse x-text-size header value '{hSize}'!");

                var _moreData = string.Equals(hMoreData, bool.TrueString, StringComparison.OrdinalIgnoreCase);

                Result = new JenkinsProgressiveHtmlResponse {
                    Size = _size,
                    MoreData = _moreData,
                };

                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    using (var reader = new StreamReader(stream, encoding)) {
                        Result.Html = reader.ReadToEnd();
                    }
                }
            };

        #if !NET40
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

                Result = new JenkinsProgressiveHtmlResponse {
                    Size = _size,
                    MoreData = _moreData,
                };

                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var encoding = TryGetEncoding(response.ContentEncoding, Encoding.UTF8);
                    Result.Html = await stream.ReadToEndAsync(encoding, token);
                }
            };
        #endif
        }
    }
}

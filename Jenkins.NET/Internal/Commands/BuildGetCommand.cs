using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class BuildGetCommand<T> : JenkinsHttpCommand where T : class, IJenkinsBuild
    {
        public T Result {get; private set;}


        public BuildGetCommand(IJenkinsContext context, string jobName, string buildNumber)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            if (string.IsNullOrEmpty(buildNumber))
                throw new ArgumentException("'buildNumber' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber, "api/xml");
            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "POST";
            };

            OnReadAsync = async response => {
                var document = await ReadXmlAsync(response);

                var args = new object[] {document.Root};
                Result = Activator.CreateInstance(typeof(T), args) as T;
            };
        }
    }
}

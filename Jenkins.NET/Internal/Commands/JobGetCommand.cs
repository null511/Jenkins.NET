using JenkinsNET.Models;
using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobGetCommand<T> : JenkinsHttpCommand where T : class, IJenkinsJob
    {
        public T Result {get; private set;}


        public JobGetCommand(IJenkinsContext context, string jobName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "api/xml");

            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "GET";
            };

            OnReadAsync = async response => {
                var document = await ReadXmlAsync(response);

                Result = Activator.CreateInstance(typeof(T), new[] {document.Root}) as T;
            };
        }
    }
}

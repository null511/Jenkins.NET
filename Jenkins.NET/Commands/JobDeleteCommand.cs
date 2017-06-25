using JenkinsNET.Internal;
using System;

namespace JenkinsNET.Commands
{
    internal class JobDeleteCommand : JenkinsHttpCommand
    {
        public JobDeleteCommand(IJenkinsContext context, string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("'jobName' cannot be empty!");

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "doDelete");
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "POST";
            };
        }
    }
}

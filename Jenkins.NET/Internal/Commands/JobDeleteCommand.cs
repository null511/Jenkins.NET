using System;

namespace JenkinsNET.Internal.Commands
{
    internal class JobDeleteCommand : JenkinsHttpCommand
    {
        public JobDeleteCommand(IJenkinsContext context, string jobName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

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

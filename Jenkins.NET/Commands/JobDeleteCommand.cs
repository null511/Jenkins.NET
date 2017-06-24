using JenkinsNET.Internal;

namespace JenkinsNET.Commands
{
    internal class JobDeleteCommand : JenkinsHttpCommand
    {
        public JobDeleteCommand(IJenkinsContext context, string jobName)
        {
            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "doDelete");
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "POST";
            };
        }
    }
}

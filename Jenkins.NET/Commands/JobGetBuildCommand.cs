using JenkinsNET.Internal;
using JenkinsNET.Models;
using System;
using System.Xml.Linq;

namespace JenkinsNET.Commands
{
    internal class JobGetBuildCommand : JenkinsHttpCommand
    {
        public JenkinsBuild Result {get; private set;}


        public JobGetBuildCommand(IJenkinsContext context, string jobName, int buildNumber)
        {
            Url = NetPath.Combine(context.BaseUrl, "job", jobName, buildNumber.ToString(), "api/xml");
            UserName = context.UserName;
            Password = context.Password;

            OnWrite = request => {
                request.Method = "POST";
            };

            OnRead = response => {
                using (var stream = response.GetResponseStream()) {
                    if (stream == null) return;

                    var document = XDocument.Load(stream);
                    if (document.Root == null) throw new ApplicationException("An empty response was returned!");

                    Result = new JenkinsBuild(document.Root);
                }
            };
        }
    }
}

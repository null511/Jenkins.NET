using JenkinsNET.Models;
using System;
using System.Xml;

namespace JenkinsNET.Internal.Commands
{
    internal class JobCreateCommand : JenkinsHttpCommand
    {
        public JobCreateCommand(IJenkinsContext context, string jobName, JenkinsJob job)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            if (job == null)
                throw new ArgumentNullException(nameof(job));

            Url = NetPath.Combine(context.BaseUrl, "createItem")
                + NetPath.Query(new {name = jobName});

            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWriteAsync = async request => {
                request.Method = "POST";
                request.ContentType = "application/xml";

                var xmlSettings = new XmlWriterSettings {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    Indent = false,
                };

                using (var stream = await request.GetRequestStreamAsync())
                using (var writer = XmlWriter.Create(stream, xmlSettings)) {
                    job.Node.WriteTo(writer);
                }
            };
        }
    }
}

using JenkinsNET.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace JenkinsNET.Internal.Commands
{
    internal class JobGetCommand : JenkinsHttpCommand
    {
        public JenkinsJob Result {get; private set;}


        public JobGetCommand(IJenkinsContext context, string jobName)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(jobName))
                throw new ArgumentException("Value cannot be empty!", nameof(jobName));

            Url = NetPath.Combine(context.BaseUrl, "job", jobName, "config.xml");

            UserName = context.UserName;
            Password = context.Password;
            Crumb = context.Crumb;

            OnWrite = request => {
                request.Method = "GET";
            };

            OnReadAsync = async response => {
                string xml;
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream)) {
                    xml = await reader.ReadToEndAsync();
                }

                // Remove Decleration
                var pattern = @"<\?xml[^\>]*\?>";
                xml = Regex.Replace(xml, pattern, string.Empty);

                var document = XDocument.Parse(xml);

                Result = new JenkinsJob(document.Root);
            };
        }
    }
}

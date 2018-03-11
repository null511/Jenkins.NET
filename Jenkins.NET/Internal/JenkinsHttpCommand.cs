using JenkinsNET.Models;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace JenkinsNET.Internal
{
    internal class JenkinsHttpCommand
    {
        public string Url {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public JenkinsCrumb Crumb {get; set;}
        public Action<HttpWebRequest> OnWrite {get; set;}
        public Action<HttpWebResponse> OnRead {get; set;}
        public Func<HttpWebRequest, Task> OnWriteAsync {get; set;}
        public Func<HttpWebResponse, Task> OnReadAsync {get; set;}


        public void Run()
        {
            var request = CreateRequest();

            if (OnWrite != null) OnWrite.Invoke(request);
            else if (OnWriteAsync != null) OnWriteAsync.Invoke(request).GetAwaiter().GetResult();

            using (var response = (HttpWebResponse)request.GetResponse()) {
                if (OnRead != null) OnRead?.Invoke(response);
                else if (OnReadAsync != null) OnReadAsync.Invoke(response).GetAwaiter().GetResult();
            }
        }

        public async Task RunAsync()
        {
            var request = CreateRequest();

            if (OnWriteAsync != null) await OnWriteAsync.Invoke(request);
            else if (OnWrite != null) OnWrite.Invoke(request);

            using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                if (OnReadAsync != null) await OnReadAsync.Invoke(response);
                else if (OnRead != null) OnRead?.Invoke(response);
            }
        }

        private HttpWebRequest CreateRequest()
        {
            var _url = Url;
            var hasUser = !string.IsNullOrEmpty(UserName);
            var hasPass = !string.IsNullOrEmpty(Password);

            var request = WebRequest.CreateHttp(_url);
            request.UserAgent = "Jenkins.NET Client";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;

            if (Crumb != null)
                request.Headers.Add(Crumb.CrumbRequestField, Crumb.Crumb);

            if (hasUser && hasPass) {
                request.PreAuthenticate = true;
                request.UseDefaultCredentials = false;

                var data = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
                var basicAuthToken = Convert.ToBase64String(data);
                request.Headers["Authorization"] = $"Basic {basicAuthToken}";
            }

            return request;
        }

        protected async Task<XDocument> ReadXmlAsync(HttpWebResponse response)
        {
            string xml;
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream)) {
                xml = await reader.ReadToEndAsync();
            }

            // Remove Decleration
            var pattern = @"<\?xml[^\>]*\?>";
            xml = Regex.Replace(xml, pattern, string.Empty);

            return XDocument.Parse(xml);
        }

        protected async Task WriteXmlAsync(HttpWebRequest request, XNode node)
        {
            var xmlSettings = new XmlWriterSettings {
                ConformanceLevel = ConformanceLevel.Fragment,
                Indent = false,
            };

            using (var stream = await request.GetRequestStreamAsync())
            using (var writer = XmlWriter.Create(stream, xmlSettings)) {
                node.WriteTo(writer);
            }
        }
    }
}

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsNET.Internal
{
    internal class JenkinsHttpCommand
    {
        public string Url {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public Action<HttpWebRequest> OnWrite {get; set;}
        public Action<HttpWebResponse> OnRead {get; set;}
        public Func<HttpWebRequest, Task> OnWriteAsync {get; set;}
        public Func<HttpWebResponse, Task> OnReadAsync {get; set;}


        public void Run()
        {
            var request = CreateRequest();

            if (OnWrite != null) OnWrite.Invoke(request);
            if (OnWriteAsync != null) OnWriteAsync.Invoke(request).GetAwaiter().GetResult();

            using (var response = (HttpWebResponse)request.GetResponse()) {
                if (OnRead != null) OnRead?.Invoke(response);
                if (OnReadAsync != null) OnReadAsync.Invoke(response).GetAwaiter().GetResult();
            }
        }

        public async Task RunAsync()
        {
            var request = CreateRequest();

            if (OnWrite != null) OnWrite.Invoke(request);
            if (OnWriteAsync != null) await OnWriteAsync.Invoke(request);

            using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                if (OnRead != null) OnRead?.Invoke(response);
                if (OnReadAsync != null) await OnReadAsync.Invoke(response);
            }
        }

        private HttpWebRequest CreateRequest()
        {
            var request = WebRequest.CreateHttp(Url);
            request.UserAgent = "Jenkins.NET Client";
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(UserName) || !string.IsNullOrEmpty(Password)) {
                request.PreAuthenticate = true;
                request.UseDefaultCredentials = false;

                var data = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
                var basicAuthToken = Convert.ToBase64String(data);
                request.Headers["Authorization"] = $"Basic {basicAuthToken}";
            }

            return request;
        }
    }
}

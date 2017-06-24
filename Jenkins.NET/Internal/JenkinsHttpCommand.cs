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


        public void Run()
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);

            OnWrite?.Invoke(request);

            using (var response = (HttpWebResponse)request.GetResponse()) {
                OnRead?.Invoke(response);
            }
        }

        public async Task RunAsync()
        {
            var request = WebRequest.CreateHttp(Url);

            if (!string.IsNullOrEmpty(UserName) || !string.IsNullOrEmpty(Password)) {
                request.PreAuthenticate = true;
                request.UseDefaultCredentials = false;

                var data = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
                var basicAuthToken = Convert.ToBase64String(data);
                request.Headers["Authorization"] = $"Basic {basicAuthToken}";
            }

            if (OnWrite != null) {
                await Task.Run(() => OnWrite(request));
            }

            using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                if (OnRead != null) {
                    await Task.Run(() => OnRead(response));
                }
            }
        }
    }
}

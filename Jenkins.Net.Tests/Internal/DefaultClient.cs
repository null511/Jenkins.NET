namespace JenkinsNET.Tests.Internal
{
    internal static class DefaultClient
    {
        private const string jenkinsUrl = "http://localhost:8080";
        private const string username = "guest";
        private const string apiToken = "bcb954a77ab47750201a9f188b1b25e8";


        public static JenkinsClient Create()
        {
            return new JenkinsClient {
                BaseUrl = jenkinsUrl,
                UserName = username,
                ApiToken = apiToken,
            };
        }
    }
}

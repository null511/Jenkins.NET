namespace JenkinsNET.Internal
{
    internal class JenkinsUrlFactory
    {
        public string BaseUrl {get;}


        public JenkinsUrlFactory(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        public string Job(string jobName)
        {
            return NetPath.Combine(BaseUrl, "job", jobName);
        }

        public string JobStart(string jobName)
        {
            return NetPath.Combine(BaseUrl, "job", jobName, "build?delay=0sec");
        }

        public string JobStatus(string jobName, JenkinsApiFormats format)
        {
            var apiFormat = GetApiFormatString(format);
            return NetPath.Combine(BaseUrl, "job", jobName, "lastBuild/api", apiFormat);
        }

        public static string GetApiFormatString(JenkinsApiFormats format)
        {
            switch (format) {
                default:
                case JenkinsApiFormats.Xml: return "xml";
                case JenkinsApiFormats.Json: return "json";
            }
        }
    }
}

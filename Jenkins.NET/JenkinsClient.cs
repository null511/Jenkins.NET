namespace JenkinsNET
{
    public class JenkinsClient : IJenkinsContext
    {
        public string BaseUrl {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}

        public JenkinsJobs Jobs {get;}
        public JenkinsQueue Queue {get;}


        public JenkinsClient()
        {
            Jobs = new JenkinsJobs(this);
            Queue = new JenkinsQueue(this);
        }
    }
}

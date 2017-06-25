namespace JenkinsNET
{
    /// <summary>
    /// HTTP-Client for interacting with Jenkins API.
    /// </summary>
    public class JenkinsClient : IJenkinsContext
    {
        /// <summary>
        /// The address of the Jenkins instance.
        /// ie: http://localhost:8080
        /// </summary>
        public string BaseUrl {get; set;}

        /// <summary>
        /// [optional] Jenkins Username
        /// </summary>
        public string UserName {get; set;}

        /// <summary>
        /// [optional] Jenkins Password
        /// </summary>
        public string Password {get; set;}

        /// <summary>
        /// Group of methods for interacting with Jenkins Jobs.
        /// </summary>
        public JenkinsJobs Jobs {get;}

        /// <summary>
        /// Group of methods for interacting with the Jenkins Job-Queue.
        /// </summary>
        public JenkinsQueue Queue {get;}


        /// <summary>
        /// Creates a new Jenkins Client.
        /// </summary>
        public JenkinsClient()
        {
            Jobs = new JenkinsJobs(this);
            Queue = new JenkinsQueue(this);
        }
    }
}

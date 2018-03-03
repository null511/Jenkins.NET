using System;

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
        /// [optional] Jenkins Username.
        /// </summary>
        public string UserName {get; set;}

        /// <summary>
        /// [optional] Jenkins ApiToken for the <see cref="UserName"/>.
        /// </summary>
        public string ApiToken {get; set;}

        /// <summary>
        /// [optional] Jenkins Password.
        /// </summary>
        [Obsolete("This property will be removed in future versions; please use 'JenkinsClient.ApiToken' instead.")]
        public string Password {
            get => ApiToken;
            set => ApiToken = value;
        }

        /// <summary>
        /// Group of methods for interacting with Jenkins Jobs.
        /// </summary>
        public JenkinsClientJobs Jobs {get;}

        /// <summary>
        /// Group of methods for interacting with Jenkins Builds.
        /// </summary>
        public JenkinsClientBuilds Builds {get;}

        /// <summary>
        /// Group of methods for interacting with the Jenkins Job-Queue.
        /// </summary>
        public JenkinsClientQueue Queue {get;}

        /// <summary>
        /// Group of methods for interacting with Jenkins Artifacts.
        /// </summary>
        public JenkinsClientArtifacts Artifacts {get;}


        /// <summary>
        /// Creates a new Jenkins Client.
        /// </summary>
        public JenkinsClient()
        {
            Jobs = new JenkinsClientJobs(this);
            Builds = new JenkinsClientBuilds(this);
            Queue = new JenkinsClientQueue(this);
            Artifacts = new JenkinsClientArtifacts(this);
        }
    }
}

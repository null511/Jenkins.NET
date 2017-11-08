namespace JenkinsNET
{
    /// <summary>
    /// Describes the context of a Jenkins.NET client.
    /// </summary>
    public interface IJenkinsContext
    {
        /// <summary>
        /// The address of the Jenkins instance.
        /// ie: http://localhost:8080
        /// </summary>
        string BaseUrl {get;}

        /// <summary>
        /// [optional] Jenkins Username.
        /// </summary>
        string UserName {get;}

        /// <summary>
        /// [optional] Jenkins Password.
        /// </summary>
        string Password {get;}
    }
}

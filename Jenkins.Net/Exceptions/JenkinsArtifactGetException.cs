using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsArtifactGetException : JenkinsNetException
    {
        internal JenkinsArtifactGetException(string message, Exception innerException) : base(message, innerException) {}
    }
}

using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsArtifactGetException : ApplicationException
    {
        internal JenkinsArtifactGetException(string message, Exception innerException) : base(message, innerException) {}
    }
}

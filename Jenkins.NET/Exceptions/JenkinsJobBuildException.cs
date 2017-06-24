using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsJobBuildException : ApplicationException
    {
        internal JenkinsJobBuildException(string message) : base(message) {}
        internal JenkinsJobBuildException(string message, Exception innerException) : base(message, innerException) {}
    }
}

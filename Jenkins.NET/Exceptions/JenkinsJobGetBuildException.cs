using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsJobGetBuildException : JenkinsNetException
    {
        internal JenkinsJobGetBuildException(string message, Exception innerException) : base(message, innerException) {}
    }
}

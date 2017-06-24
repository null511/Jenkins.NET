using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsJobGetBuildException : ApplicationException
    {
        internal JenkinsJobGetBuildException(string message, Exception innerException) : base(message, innerException) {}
    }
}

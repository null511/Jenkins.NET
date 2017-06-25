using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsJobDeleteException : JenkinsNetException
    {
        internal JenkinsJobDeleteException(string message, Exception innerException) : base(message, innerException) {}
    }
}

using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsJobDeleteException : ApplicationException
    {
        internal JenkinsJobDeleteException(string message, Exception innerException) : base(message, innerException) {}
    }
}

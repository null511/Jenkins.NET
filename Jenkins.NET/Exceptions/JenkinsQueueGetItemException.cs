using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsQueueGetItemException : ApplicationException
    {
        internal JenkinsQueueGetItemException(string message, Exception innerException) : base(message, innerException) {}
    }
}

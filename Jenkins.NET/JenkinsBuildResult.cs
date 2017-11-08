using JenkinsNET.Internal;
using System.Text.RegularExpressions;

namespace JenkinsNET
{
    /// <summary>
    /// Describes the result of a request to Build a Jenkins Job.
    /// </summary>
    public sealed class JenkinsBuildResult
    {
        private static readonly Regex expItemNumber = new Regex(@"\/queue\/item\/(\d+)\/*", RegexOptions.Compiled);

        /// <summary>
        /// The URL of the Queue reference item.
        /// </summary>
        public string QueueItemUrl {get; internal set;}


        internal JenkinsBuildResult() {}

        /// <summary>
        /// Gets the unique item number for the Queue reference item.
        /// </summary>
        public int? GetQueueItemNumber()
        {
            var match = expItemNumber.Match(QueueItemUrl);
            if (!match.Groups[1].Success) return null;

            return match.Groups[1].Value.To<int>();
        }
    }
}

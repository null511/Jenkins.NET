using JenkinsNET.Internal;
using System.Text.RegularExpressions;

namespace JenkinsNET
{
    public class JenkinsBuildResult
    {
        private static readonly Regex expItemNumber = new Regex(@"\/queue\/item\/(\d+)\/*", RegexOptions.Compiled);

        public string QueueItemUrl {get; internal set;}


        internal JenkinsBuildResult() {}

        public int? GetQueueItemNumber()
        {
            var match = expItemNumber.Match(QueueItemUrl);
            if (!match.Groups[1].Success) return null;

            return match.Groups[1].Value.To<int>();
        }
    }
}

namespace JenkinsNET.Models
{
    /// <summary>
    /// Contains a segment of output HTML from a running Jenkins Job.
    /// </summary>
    public class JenkinsProgressiveHtmlResponse
    {
        /// <summary>
        /// The segment of HTML read.
        /// </summary>
        public string Html {get; internal set;}

        /// <summary>
        /// The position of the current reader after retrieving <see cref="Html"/>.
        /// </summary>
        public int Size {get; internal set;}

        /// <summary>
        /// Gets whether more data is available to read.
        /// </summary>
        public bool MoreData {get; internal set;}
    }
}

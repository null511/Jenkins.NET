namespace JenkinsNET.Models
{
    /// <summary>
    /// Contains a segment of output text from a running Jenkins Job.
    /// </summary>
    public class JenkinsProgressiveTextResponse
    {
        /// <summary>
        /// The segment of text read.
        /// </summary>
        public string Text {get; internal set;}

        /// <summary>
        /// The position of the current reader after retrieving <see cref="Text"/>.
        /// </summary>
        public int Size {get; internal set;}

        /// <summary>
        /// Gets whether more data is available to read.
        /// </summary>
        public bool MoreData {get; internal set;}
    }
}

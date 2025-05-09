using System;

#if !NET40
using System.Threading.Tasks;
#endif

namespace JenkinsNET.Utilities
{
    /// <summary>
    /// Represents a method that will handle an
    /// event when the job console output has changed.
    /// </summary>
    public delegate void ProgressiveTextChangedEventHandler(string newText);

    /// <summary>
    /// Utility class for reading progressive text output
    /// from a running Jenkins Job.
    /// </summary>
    public class ProgressiveTextReader
    {
        private readonly IJenkinsClient client;
        private readonly string jobName;
        private readonly string buildNumber;
        private int readPos;

        /// <summary>
        /// Gets whether the reading of text has completed.
        /// </summary>
        public bool IsComplete {get; private set;}

        /// <summary>
        /// Occurs when the value of <see cref="Text"/> is changed.
        /// </summary>
        public event ProgressiveTextChangedEventHandler TextChanged;

        /// <summary>
        /// Gets the text that has been retrieved.
        /// </summary>
        public string Text {get; private set;}


        /// <summary>
        /// Creates a new instance of <see cref="ProgressiveTextReader"/>
        /// attached to the specified Jenkins Job.
        /// </summary>
        /// <param name="client">The Jenkins client to use for network operations.</param>
        /// <param name="jobName">The name of the Jenkins Job.</param>
        /// <param name="buildNumber">The build-number of the running Jenkins Job.</param>
        public ProgressiveTextReader(IJenkinsClient client, string jobName, string buildNumber)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.jobName = jobName ?? throw new ArgumentNullException(nameof(jobName));
            this.buildNumber = buildNumber ?? throw new ArgumentNullException(nameof(buildNumber));
        }

        /// <summary>
        /// Retrieves and appends any additional text
        /// returned by the running Jenkins Job.
        /// </summary>
        public void Update()
        {
            if (IsComplete) return;

            var result = client.Builds.GetProgressiveText(jobName, buildNumber, readPos);

            if (result.Size > 0) {
                Text += result.Text;
                TextChanged?.Invoke(result.Text);
                readPos = result.Size;
            }

            if (!result.MoreData)
                IsComplete = true;
        }

    #if !NET40
        /// <summary>
        /// Retrieves and appends any additional text returned
        /// by the running Jenkins Job asynchronously.
        /// </summary>
        public async Task UpdateAsync()
        {
            if (IsComplete) return;

            var result = await client.Builds.GetProgressiveTextAsync(jobName, buildNumber, readPos);

            if (result.Size > 0) {
                Text += result.Text;
                TextChanged?.Invoke(result.Text);
                readPos = result.Size;
            }

            if (!result.MoreData)
                IsComplete = true;
        }
    #endif
    }
}

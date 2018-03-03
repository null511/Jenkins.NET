using JenkinsNET.Exceptions;
using JenkinsNET.Internal;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;
using System;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Builds.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientBuilds
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientBuilds(IJenkinsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets information describing a Jenkins Job Build.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        public JenkinsBuild Get(string jobName, string buildNumber)
        {
            try {
                var cmd = new BuildGetCommand(context, jobName, buildNumber);
                cmd.RunAsync().GetAwaiter().GetResult();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets information describing a Jenkins Job Build asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        public async Task<JenkinsBuild> GetAsync(string jobName, string buildNumber)
        {
            try {
                var cmd = new BuildGetCommand(context, jobName, buildNumber);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets information describing the last successful Build of a Jenkins Job.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        [Obsolete("This method will be removed in future versions; please use `Get(BuildNumber.LastSuccessful)`.")]
        public JenkinsBuild GetLastSuccessful(string jobName)
        {
            return Get(jobName, BuildNumber.LastSuccessful);
        }

        /// <summary>
        /// Gets information describing the last successful Build of a Jenkins Job asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        [Obsolete("This method will be removed in future versions; please use `GetAsync(BuildNumber.LastSuccessful)`.")]
        public async Task<JenkinsBuild> GetLastSuccessfulAsync(string jobName)
        {
            return await GetAsync(jobName, BuildNumber.LastSuccessful);
        }

        /// <summary>
        /// Gets the console output from a Jenkins Job Build.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        public string GetConsoleOutput(string jobName, string buildNumber)
        {
            try {
                var cmd = new BuildOutputCommand(context, jobName, buildNumber);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve console output from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets the console output from a Jenkins Job Build asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        public async Task<string> GetConsoleOutputAsync(string jobName, string buildNumber)
        {
            try {
                var cmd = new BuildOutputCommand(context, jobName, buildNumber);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve console output from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets the progressive text output from a Jenkins Job Build.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        /// <param name="start">The character position to begin reading from.</param>
        public JenkinsProgressiveTextResponse GetProgressiveText(string jobName, string buildNumber, int start)
        {
            try {
                var cmd = new BuildProgressiveTextCommand(context, jobName, buildNumber, start);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve progressive text from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets the progressive text output from a Jenkins Job Build asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        /// <param name="start">The character position to begin reading from.</param>
        public async Task<JenkinsProgressiveTextResponse> GetProgressiveTextAsync(string jobName, string buildNumber, int start)
        {
            try {
                var cmd = new BuildProgressiveTextCommand(context, jobName, buildNumber, start);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve progressive text from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets the console output from a Jenkins Job Build.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        /// <param name="start">The character position to begin reading from.</param>
        public JenkinsProgressiveHtmlResponse GetProgressiveHtml(string jobName, string buildNumber, int start)
        {
            try {
                var cmd = new BuildProgressiveHtmlCommand(context, jobName, buildNumber, start);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve progressive HTML from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Gets the console output from a Jenkins Job Build asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        /// <param name="start">The character position to begin reading from.</param>
        public async Task<JenkinsProgressiveHtmlResponse> GetProgressiveHtmlAsync(string jobName, string buildNumber, int start)
        {
            try {
                var cmd = new BuildProgressiveHtmlCommand(context, jobName, buildNumber, start);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to retrieve progressive HTML from build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }
    }
}

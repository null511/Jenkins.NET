using JenkinsNET.Exceptions;
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
        public JenkinsBuild Get(string jobName, string buildNumber = "lastSuccessfulBuild")
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
        public async Task<JenkinsBuild> GetAsync(string jobName, string buildNumber = "lastSuccessfulBuild")
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
        public JenkinsBuild GetLastSuccessful(string jobName)
        {
            return Get(jobName, "lastSuccessfulBuild");
        }

        /// <summary>
        /// Gets information describing the last successful Build of a Jenkins Job asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        public async Task<JenkinsBuild> GetLastSuccessfulAsync(string jobName)
        {
            return await GetAsync("lastSuccessfulBuild");
        }

        /// <summary>
        /// Gets the console output from a Jenkins Job Build.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The number of the build.</param>
        public string GetConsoleOutput(string jobName, string buildNumber = "lastSuccessfulBuild")
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
        public async Task<string> GetConsoleOutputAsync(string jobName, string buildNumber = "lastSuccessfulBuild")
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
    }
}

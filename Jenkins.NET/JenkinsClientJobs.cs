using JenkinsNET.Commands;
using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Jobs API.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public class JenkinsClientJobs
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientJobs(IJenkinsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Enqueues a Job to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        public JenkinsBuildResult Build(string jobName)
        {
            try {
                var cmd = new JobBuildCommand(context, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Enqueues a Job to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        public async Task<JenkinsBuildResult> BuildAsync(string jobName)
        {
            try {
                var cmd = new JobBuildCommand(context, jobName);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Enqueues a Job with parameters to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="jobParameters">The collection of parameters for building the job.</param>
        public JenkinsBuildResult BuildWithParameters(string jobName, IDictionary<string, string> jobParameters)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(context, jobName, jobParameters);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        /// <summary>
        /// Enqueues a Job with parameters to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="jobParameters">The collection of parameters for building the job.</param>
        public async Task<JenkinsBuildResult> BuildWithParametersAsync(string jobName, IDictionary<string, string> jobParameters)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(context, jobName, jobParameters);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        public void Delete(string jobName)
        {
            try {
                new JobDeleteCommand(context, jobName).Run();
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task DeleteAsync(string jobName)
        {
            try {
                await new JobDeleteCommand(context, jobName).RunAsync();
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }
    }
}

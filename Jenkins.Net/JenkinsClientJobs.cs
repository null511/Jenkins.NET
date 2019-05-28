using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Jobs API.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientJobs
    {
        private readonly JenkinsClient client;


        internal JenkinsClientJobs(JenkinsClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Enqueues a Job to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public JenkinsBuildResult Build(string jobName)
        {
            try {
                var cmd = new JobBuildCommand(client, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Enqueues a Job to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public async Task<JenkinsBuildResult> BuildAsync(string jobName, CancellationToken token = default)
        {
            try {
                var cmd = new JobBuildCommand(client, jobName);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Enqueues a Job with parameters to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="jobParameters">The collection of parameters for building the job.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public JenkinsBuildResult BuildWithParameters(string jobName, IDictionary<string, string> jobParameters)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(client, jobName, jobParameters);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Enqueues a Job with parameters to be built.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="jobParameters">The collection of parameters for building the job.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public async Task<JenkinsBuildResult> BuildWithParametersAsync(string jobName, IDictionary<string, string> jobParameters, CancellationToken token = default)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(client, jobName, jobParameters);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Gets a Job description from Jenkins.
        /// </summary>
        /// <param name="jobName">The Name of the Job to retrieve.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public T Get<T>(string jobName) where T : class, IJenkinsJob
        {
            try {
                var cmd = new JobGetCommand<T>(client, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to get Jenkins Job '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Gets a Job description from Jenkins asynchronously.
        /// </summary>
        /// <param name="jobName">The Name of the Job to retrieve.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task<T> GetAsync<T>(string jobName, CancellationToken token = default) where T : class, IJenkinsJob
        {
            try {
                var cmd = new JobGetCommand<T>(client, jobName);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to get Jenkins Job '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Gets a Job configuration from Jenkins.
        /// </summary>
        /// <param name="jobName">The Name of the Job to retrieve.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public JenkinsProject GetConfiguration(string jobName)
        {
            try {
                var cmd = new JobGetConfigCommand(client, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to get Jenkins Job Configuration '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Gets a Job configuration from Jenkins asynchronously.
        /// </summary>
        /// <param name="jobName">The Name of the Job to retrieve.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task<JenkinsProject> GetConfigurationAsync(string jobName, CancellationToken token = default)
        {
            try {
                var cmd = new JobGetConfigCommand(client, jobName);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to get Jenkins Job Configuration '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Creates a new Job in Jenkins.
        /// </summary>
        /// <param name="jobName">The name of the Job to create.</param>
        /// <param name="job">The description of the Job to create.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public void Create(string jobName, JenkinsProject job)
        {
            try {
                new JobCreateCommand(client, jobName, job).Run();
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to create Jenkins Job '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Creates a new Job in Jenkins asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job to create.</param>
        /// <param name="job">The description of the Job to create.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task CreateAsync(string jobName, JenkinsProject job, CancellationToken token = default)
        {
            try {
                await new JobCreateCommand(client, jobName, job).RunAsync(token);
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to create Jenkins Job '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Updates the configuration on an existing Job in Jenkins.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="job">The updated description of the Job.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public void UpdateConfiguration(string jobName, JenkinsProject job)
        {
            try {
                new JobUpdateConfigurationCommand(client, jobName, job).Run();
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to update Jenkins Job configuration '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Updates the configuration on an existing Job in Jenkins asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="job">The updated description of the Job.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task UpdateConfigurationAsync(string jobName, JenkinsProject job, CancellationToken token = default)
        {
            try {
                await new JobUpdateConfigurationCommand(client, jobName, job).RunAsync(token);
            }
            catch (Exception error) {
                throw new JenkinsNetException($"Failed to update Jenkins Job configuration '{jobName}'!", error);
            }
        }
    #endif

        /// <summary>
        /// Deletes a Job from Jenkins.
        /// </summary>
        /// <param name="jobName">The name of the Job to delete.</param>
        /// <exception cref="JenkinsJobDeleteException"></exception>
        public void Delete(string jobName)
        {
            try {
                new JobDeleteCommand(client, jobName).Run();
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Deletes a Job from Jenkins asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job to delete.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsJobDeleteException"></exception>
        public async Task DeleteAsync(string jobName, CancellationToken token = default)
        {
            try {
                await new JobDeleteCommand(client, jobName).RunAsync(token);
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }
    #endif
    }
}

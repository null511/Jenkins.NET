using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// Represents the method that will handle an
    /// event when the job status has changed.
    /// </summary>
    public delegate void StatusChangedEventHandler();

    /// <summary>
    /// Begins building a Jenkins Job, and polls the status
    /// until the job has completed.
    /// </summary>
    public class JenkinsJobRunner
    {
        /// <summary>
        /// Fired when the status of the JobRunner changes.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;

        private readonly JenkinsClient client;

        /// <summary>
        /// The status of the Job.
        /// </summary>
        public JenkinsJobStatus Status {get; private set;}

        /// <summary>
        /// Time in milliseconds to wait between requests.
        /// Default value is 500.
        /// </summary>
        public int PollInterval {get; set;} = 500;

        /// <summary>
        /// Maximum time in seconds to wait for job to be queued.
        /// Default value is 30.
        /// </summary>
        public int QueueTimeout {get; set;} = 30;

        /// <summary>
        /// Maximum time in seconds to wait for job to complete.
        /// Default value is 60.
        /// </summary>
        public int BuildTimeout {get; set;} = 60;


        /// <summary>
        /// Creates a new JobRunner using the provided Jenkins-Client.
        /// </summary>
        public JenkinsJobRunner(JenkinsClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Run the Job.
        /// </summary>
        /// <param name="jobName">The name of the Job to run.</param>
        public JenkinsBuild Run(string jobName)
        {
            SetStatus(JenkinsJobStatus.Pending);
            var queueStartTime = DateTime.Now;

            var buildResult = client.Jobs.Build(jobName);

            if (buildResult == null)
                throw new JenkinsJobBuildException("An empty build response was returned!");

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new JenkinsNetException("Queue-Item number not found!");

            SetStatus(JenkinsJobStatus.Queued);

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = client.Queue.GetItem(queueItemNumber.Value);
                buildNumber = queueItem?.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to start!");

                    Thread.Sleep(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Building);
            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = client.Builds.Get(jobName, buildNumber.Value.ToString());

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to complete!");

                    Thread.Sleep(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Complete);
            return buildItem;
        }

        /// <summary>
        /// Run the Job asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job to run.</param>
        public async Task<JenkinsBuild> RunAsync(string jobName)
        {
            SetStatus(JenkinsJobStatus.Pending);
            var queueStartTime = DateTime.Now;

            var buildResult = await client.Jobs.BuildAsync(jobName);

            if (buildResult == null)
                throw new JenkinsJobBuildException("An empty build response was returned!");

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new JenkinsNetException("Queue-Item number not found!");

            SetStatus(JenkinsJobStatus.Queued);

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = await client.Queue.GetItemAsync(queueItemNumber.Value);
                buildNumber = queueItem?.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to start!");

                    await Task.Delay(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Building);
            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = await client.Builds.GetAsync(jobName, buildNumber.Value.ToString());

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to complete!");

                    await Task.Delay(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Complete);
            return buildItem;
        }

        /// <summary>
        /// Run the Job with parameters.
        /// </summary>
        /// <param name="jobName">The name of the Job to run.</param>
        /// <param name="jobParameters">The parameters used to start the Job.</param>
        public JenkinsBuild RunWithParameters(string jobName, IDictionary<string, string> jobParameters)
        {
            SetStatus(JenkinsJobStatus.Pending);
            var queueStartTime = DateTime.Now;

            var buildResult = client.Jobs.BuildWithParameters(jobName, jobParameters);

            if (buildResult == null)
                throw new JenkinsJobBuildException("An empty build response was returned!");

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new JenkinsNetException("Queue-Item number not found!");

            SetStatus(JenkinsJobStatus.Queued);

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = client.Queue.GetItem(queueItemNumber.Value);
                buildNumber = queueItem?.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to start!");

                    Thread.Sleep(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Building);
            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = client.Builds.Get(jobName, buildNumber.Value.ToString());

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to complete!");

                    Thread.Sleep(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Complete);
            return buildItem;
        }

        /// <summary>
        /// Run the Job asynchronously with parameters.
        /// </summary>
        /// <param name="jobName">The name of the Job to run.</param>
        /// <param name="jobParameters">The parameters used to start the Job.</param>
        public async Task<JenkinsBuild> RunWithParametersAsync(string jobName, IDictionary<string, string> jobParameters)
        {
            SetStatus(JenkinsJobStatus.Pending);
            var queueStartTime = DateTime.Now;

            var buildResult = await client.Jobs.BuildWithParametersAsync(jobName, jobParameters);

            if (buildResult == null)
                throw new JenkinsJobBuildException("An empty build response was returned!");

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new JenkinsNetException("Queue-Item number not found!");

            SetStatus(JenkinsJobStatus.Queued);

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = await client.Queue.GetItemAsync(queueItemNumber.Value);
                buildNumber = queueItem?.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to start!");

                    await Task.Delay(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Building);
            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = await client.Builds.GetAsync(jobName, buildNumber.Value.ToString());

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new JenkinsNetException("Timeout occurred while waiting for build to complete!");

                    await Task.Delay(PollInterval);
                }
            }

            SetStatus(JenkinsJobStatus.Complete);
            return buildItem;
        }

        private void SetStatus(JenkinsJobStatus newStatus)
        {
            Status = newStatus;

            try {
                StatusChanged?.Invoke();
            }
            catch {}
        }
    }
}

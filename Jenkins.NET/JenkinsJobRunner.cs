using JenkinsNET.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET
{
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

        public JenkinsJobStatus Status {get; private set;}

        /// <summary>
        /// Time in milliseconds to wait between requests.
        /// </summary>
        public int PollInterval {get; set;} = 500;

        /// <summary>
        /// Maximum time in seconds to wait for job to be queued.
        /// </summary>
        public int QueueTimeout {get; set;} = 30;

        /// <summary>
        /// Maximum time in seconds to wait for job to complete.
        /// </summary>
        public int BuildTimeout {get; set;} = 60;


        public JenkinsJobRunner(JenkinsClient client)
        {
            this.client = client;
        }

        public JenkinsBuild Run(string jobName)
        {
            var queueStartTime = DateTime.Now;

            Status = JenkinsJobStatus.Pending;
            StatusChanged?.Invoke();

            var buildResult = client.Jobs.BuildWithParameters(jobName, new Dictionary<string, string> {
                ["Integration_Test"] = "false",
            });

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new ApplicationException("Queue-Item number not found!");

            Status = JenkinsJobStatus.Queued;
            StatusChanged?.Invoke();

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = client.Queue.GetItem(queueItemNumber.Value);
                buildNumber = queueItem.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new ApplicationException("Timeout occurred while waiting for build to start!");

                    Thread.Sleep(PollInterval);
                }
            }

            Status = JenkinsJobStatus.Building;
            StatusChanged?.Invoke();

            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = client.Jobs.GetBuild(jobName, buildNumber.Value);

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new ApplicationException("Timeout occurred while waiting for build to complete!");

                    Thread.Sleep(PollInterval);
                }
            }

            Status = JenkinsJobStatus.Complete;
            StatusChanged?.Invoke();

            return buildItem;
        }

        public async Task<JenkinsBuild> RunAsync(string jobName)
        {
            var queueStartTime = DateTime.Now;

            Status = JenkinsJobStatus.Pending;
            StatusChanged?.Invoke();

            var buildResult = await client.Jobs.BuildWithParametersAsync(jobName, new Dictionary<string, string> {
                ["Integration_Test"] = "false",
            });

            var queueItemNumber = buildResult.GetQueueItemNumber();
            if (!queueItemNumber.HasValue) throw new ApplicationException("Queue-Item number not found!");

            Status = JenkinsJobStatus.Queued;
            StatusChanged?.Invoke();

            int? buildNumber = null;
            while (!buildNumber.HasValue) {
                var queueItem = await client.Queue.GetItemAsync(queueItemNumber.Value);
                buildNumber = queueItem.Executable?.Number;

                if (!buildNumber.HasValue) {
                    if (DateTime.Now.Subtract(queueStartTime).TotalSeconds > QueueTimeout)
                        throw new ApplicationException("Timeout occurred while waiting for build to start!");

                    await Task.Delay(PollInterval);
                }
            }

            Status = JenkinsJobStatus.Building;
            StatusChanged?.Invoke();

            var buildStartTime = DateTime.Now;

            JenkinsBuild buildItem = null;
            while (string.IsNullOrEmpty(buildItem?.Result)) {
                buildItem = await client.Jobs.GetBuildAsync(jobName, buildNumber.Value);

                if (string.IsNullOrEmpty(buildItem?.Result)) {
                    if (DateTime.Now.Subtract(buildStartTime).TotalSeconds > BuildTimeout)
                        throw new ApplicationException("Timeout occurred while waiting for build to complete!");

                    await Task.Delay(PollInterval);
                }
            }

            Status = JenkinsJobStatus.Complete;
            StatusChanged?.Invoke();

            return buildItem;
        }
    }
}

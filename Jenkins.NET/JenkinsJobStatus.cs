namespace JenkinsNET
{
    /// <summary>
    /// Describes the status of a Jenkins Job.
    /// </summary>
    public enum JenkinsJobStatus
    {
        /// <summary>
        /// No status is defined.
        /// </summary>
        None,

        /// <summary>
        /// The job request is pending.
        /// </summary>
        Pending,

        /// <summary>
        /// The job has been queued to run.
        /// </summary>
        Queued,

        /// <summary>
        /// The job is running.
        /// </summary>
        Building,

        /// <summary>
        /// The job is complete.
        /// </summary>
        Complete,
    }
}

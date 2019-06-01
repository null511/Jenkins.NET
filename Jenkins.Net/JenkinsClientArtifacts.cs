using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using System;
using System.IO;

#if NET_ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Artifacts.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientArtifacts
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientArtifacts(IJenkinsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves an artifact from a completed Job.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The build number of the Job.</param>
        /// <param name="filename">The relative path and file name of the artifact.</param>
        /// <returns>A memory-stream containing the contents of the artifact.</returns>
        /// <exception cref="JenkinsArtifactGetException"></exception>
        public MemoryStream Get(string jobName, string buildNumber, string filename)
        {
            try {
                var cmd = new ArtifactGetCommand(context, jobName, buildNumber, filename);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsArtifactGetException($"Failed to retrieve artifact '{filename}'!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Retrieves an artifact from a completed Job asynchronously.
        /// </summary>
        /// <param name="jobName">The name of the Job.</param>
        /// <param name="buildNumber">The build number of the Job.</param>
        /// <param name="filename">The relative path and file name of the artifact.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <returns>A memory-stream containing the contents of the artifact.</returns>
        /// <exception cref="JenkinsArtifactGetException"></exception>
        public async Task<MemoryStream> GetAsync(string jobName, string buildNumber, string filename, CancellationToken token = default)
        {
            try {
                var cmd = new ArtifactGetCommand(context, jobName, buildNumber, filename);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsArtifactGetException($"Failed to retrieve artifact '{filename}'!", error);
            }
        }
    #endif
    }
}

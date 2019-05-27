using JenkinsNET.Models;

#if NET_ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace JenkinsNET
{
    /// <summary>
    /// Methods for interacting with Jenkins API.
    /// </summary>
    public interface IJenkinsClient
    {
        /// <summary>
        /// Group of methods for interacting with Jenkins Jobs.
        /// </summary>
        JenkinsClientJobs Jobs {get;}

        /// <summary>
        /// Group of methods for interacting with Jenkins Builds.
        /// </summary>
        JenkinsClientBuilds Builds {get;}

        /// <summary>
        /// Group of methods for interacting with the Jenkins Job-Queue.
        /// </summary>
        JenkinsClientQueue Queue {get;}

        /// <summary>
        /// Group of methods for interacting with Jenkins Artifacts.
        /// </summary>
        JenkinsClientArtifacts Artifacts {get;}

        /// <summary>
        /// Updates the security Crumb attached to this client.
        /// </summary>
        /// <exception cref="Exceptions.JenkinsNetException"></exception>
        void UpdateSecurityCrumb();

        /// <summary>
        /// Gets the root description of the Jenkins node.
        /// </summary>
        /// <exception cref="Exceptions.JenkinsNetException"></exception>
        Jenkins Get();

    #if NET_ASYNC
        /// <summary>
        /// Updates the security Crumb attached to this client asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="Exceptions.JenkinsNetException"></exception>
        Task UpdateSecurityCrumbAsync(CancellationToken token = default);

        /// <summary>
        /// Gets the root description of the Jenkins node asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="Exceptions.JenkinsNetException"></exception>
        Task<Jenkins> GetAsync(CancellationToken token = default);
    #endif
    }
}
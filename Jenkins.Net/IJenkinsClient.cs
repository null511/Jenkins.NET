using System.Threading;
using System.Threading.Tasks;
using JenkinsNET.Models;

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
        /// <exception cref="JenkinsNetException"></exception>
        void UpdateSecurityCrumb();


        /// <summary>
        /// Updates the security Crumb attached to this client asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        Task UpdateSecurityCrumbAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the root description of the Jenkins node.
        /// </summary>
        /// <exception cref="JenkinsNetException"></exception>
        Jenkins Get();


        /// <summary>
        /// Gets the root description of the Jenkins node asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        Task<Jenkins> GetAsync(CancellationToken token = default(CancellationToken));
    }
}
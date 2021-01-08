using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with the Jenkins Job-Queue.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientNodes
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientNodes(IJenkinsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves all Nodes.
        /// </summary>
        /// <exception cref="JenkinsNetException"></exception>
        public JenkinsNode[] GetNodes()
        {
            try
            {
                var cmd = new NodeGetCommand(context);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error)
            {
                throw new JenkinsNetException("Failed to retrieve nodes list!", error);
            }
        }

#if NET_ASYNC
        /// <summary>
        /// Retrieves all Nodes asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task<JenkinsNode[]> GetNodesAsync(CancellationToken token = default)
        {
            try {
                var cmd = new NodeGetCommand(context);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException("Failed to retrieve nodes list!", error);
            }
        }
#endif

    }
}

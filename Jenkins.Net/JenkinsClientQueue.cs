using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;
using System;

#if NET_ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with the Jenkins Job-Queue.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientQueue
    {
        private readonly JenkinsClient client;


        internal JenkinsClientQueue(JenkinsClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Retrieves all items from the Job-Queue.
        /// </summary>
        /// <exception cref="JenkinsNetException"></exception>
        public JenkinsQueueItem[] GetAllItems()
        {
            try {
                var cmd = new QueueItemListCommand(client);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException("Failed to retrieve queue item list!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Retrieves all items from the Job-Queue asynchronously.
        /// </summary>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task<JenkinsQueueItem[]> GetAllItemsAsync(CancellationToken token = default)
        {
            try {
                var cmd = new QueueItemListCommand(client);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException("Failed to retrieve queue item list!", error);
            }
        }
    #endif

        /// <summary>
        /// Retrieves an item from the Job-Queue.
        /// </summary>
        /// <param name="itemNumber">The ID of the queue-item.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public JenkinsQueueItem GetItem(int itemNumber)
        {
            try {
                var cmd = new QueueGetItemCommand(client, itemNumber);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to retrieve queue item #{itemNumber}!", error);
            }
        }

    #if NET_ASYNC
        /// <summary>
        /// Retrieves an item from the Job-Queue.
        /// </summary>
        /// <param name="itemNumber">The ID of the queue-item.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsJobBuildException"></exception>
        public async Task<JenkinsQueueItem> GetItemAsync(int itemNumber, CancellationToken token = default)
        {
            try {
                var cmd = new QueueGetItemCommand(client, itemNumber);
                await cmd.RunAsync(token);
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to retrieve queue item #{itemNumber}!", error);
            }
        }
    #endif
    }
}

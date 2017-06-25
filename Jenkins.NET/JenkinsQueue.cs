using JenkinsNET.Commands;
using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with the Jenkins Job-Queue.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public class JenkinsQueue
    {
        private readonly IJenkinsContext context;


        internal JenkinsQueue(IJenkinsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves an item from the Job-Queue.
        /// </summary>
        /// <param name="itemNumber">The ID of the queue-item.</param>
        public JenkinsQueueItem GetItem(int itemNumber)
        {
            try {
                var cmd = new QueueGetItemCommand(context, itemNumber);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to retrieve queue item #{itemNumber}!", error);
            }
        }

        /// <summary>
        /// Retrieves an item from the Job-Queue.
        /// </summary>
        /// <param name="itemNumber">The ID of the queue-item.</param>
        public async Task<JenkinsQueueItem> GetItemAsync(int itemNumber)
        {
            try {
                var cmd = new QueueGetItemCommand(context, itemNumber);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to retrieve queue item #{itemNumber}!", error);
            }
        }
    }
}

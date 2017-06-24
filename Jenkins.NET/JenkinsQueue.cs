using JenkinsNET.Commands;
using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Threading.Tasks;

namespace JenkinsNET
{
    public class JenkinsQueue
    {
        private readonly IJenkinsContext context;


        internal JenkinsQueue(IJenkinsContext context)
        {
            this.context = context;
        }

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

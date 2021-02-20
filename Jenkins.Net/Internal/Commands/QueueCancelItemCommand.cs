namespace JenkinsNET.Internal.Commands
{
    internal class QueueCancelItemCommand : JenkinsHttpCommand
    {
        public QueueCancelItemCommand(JenkinsClient client, int itemNumber) : base(client)
        {
            Path = $"queue/cancelItem?id={itemNumber}";

            OnWrite = request => {
                request.Method = "POST";
            };
            
        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "POST";
            };
        #endif
        }
    }
}

using JenkinsNET.Models;

namespace JenkinsNET.Internal.Commands
{
    internal class JenkinsGetCommand : JenkinsHttpCommand
    {
        public Jenkins Result {get; private set;}


        public JenkinsGetCommand(JenkinsClient client) : base(client)
        {
            Path = "api/xml";

            OnWrite = request => {
                request.Method = "GET";
            };

            OnRead = response => {
                var document = ReadXml(response);
                Result = new Jenkins(document.Root);
            };

        #if NET_ASYNC
            OnWriteAsync = async (request, token) => {
                request.Method = "GET";
            };

            OnReadAsync = async (response, token) => {
                var document = await ReadXmlAsync(response);
                Result = new Jenkins(document.Root);
            };
        #endif
        }
    }
}

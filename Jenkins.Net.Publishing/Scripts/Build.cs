using Jenkins.NET.Publishing.Tools;
using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing.Scripts
{
    public class Build : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildTools.BuildSolution(Context, token);
            await TestTools.UnitTest(Context, token);
        }
    }
}

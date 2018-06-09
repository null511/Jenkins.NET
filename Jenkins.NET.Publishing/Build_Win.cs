using Jenkins.NET.Publishing.Internal;
using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Build_Win : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution();

            // TODO: Test
        }

        private async Task BuildSolution()
        {
            var msBuild = new MsBuild(Context) {
                Exe = ".\\bin\\msbuild.cmd",
                Filename = "Jenkins.NET.sln",
                Configuration = "Release",
                Platform = "Any CPU",
                Parallel = true,
            };

            await msBuild.BuildAsync();
        }
    }
}

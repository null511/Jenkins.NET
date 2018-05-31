using System.Threading;
using System.Threading.Tasks;
using Jenkins.NET.Publishing.Internal;
using Photon.Framework.Agent;
using Photon.Framework.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Build_Linux : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution();
        }

        private async Task BuildSolution()
        {
            var msBuild = new MsBuild(Context) {
                Exe = "msbuild",
                Filename = "Jenkins.NET.sln",
                Configuration = "Release",
                Platform = "Any CPU",
            };

            await msBuild.BuildAsync();
        }
    }
}

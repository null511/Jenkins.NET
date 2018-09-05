using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using Photon.MSBuildPlugin;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Build_Linux : IBuildTask
    {
        private MSBuildCommand msbuild;

        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            msbuild = new MSBuildCommand(Context) {
                Exe = "msbuild",
                WorkingDirectory = Context.ContentDirectory,
            };

            await BuildSolution(token);
        }

        private async Task BuildSolution(CancellationToken token)
        {
            await msbuild.RunAsync(new MSBuildArguments {
                ProjectFile = "Jenkins.NET.sln",
                Properties = {
                    ["Configuration"] = "Release",
                    ["Platform"] = "Any CPU",
                },
                Verbosity = MSBuildVerbosityLevels.Minimal,
                MaxCpuCount = 0,
            }, token);
        }
    }
}

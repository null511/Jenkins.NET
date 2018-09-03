using Photon.Framework.Agent;
using Photon.Framework.Process;
using Photon.Framework.Tasks;
using Photon.MSBuild;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Build_Windows : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution(token);
            await UnitTest(token);
        }

        private async Task BuildSolution(CancellationToken token)
        {
            var msbuild = new MSBuildCommand(Context) {
                Exe = Context.AgentVariables["global"]["msbuild_exe"],
                WorkingDirectory = Context.ContentDirectory,
            };

            var buildArgs = new MSBuildArguments {
                ProjectFile = "Jenkins.NET.sln",
                Properties = {
                    ["Configuration"] = "Release",
                    ["Platform"] = "Any CPU",
                },
                Verbosity = MSBuildVerbosityLevel.Minimal,
                MaxCpuCount = 0,
            };

            await msbuild.RunAsync(buildArgs, token);
        }

        private async Task UnitTest(CancellationToken token)
        {
            var info = new ProcessRunInfo {
                Filename = Context.AgentVariables["global"]["nunit_exe"],
                Arguments = "\"Jenkins.NET.Tests\\bin\\Release\\Jenkins.NET.Tests.dll\" --where=\"cat == 'unit'\"",
                WorkingDirectory = Context.ContentDirectory,
            };

            var runner = new ProcessRunner(Context);
            await runner.RunAsync(info, token);
        }
    }
}

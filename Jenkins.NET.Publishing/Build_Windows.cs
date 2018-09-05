using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using Photon.MSBuildPlugin;
using Photon.NUnitPlugin;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Build_Windows : IBuildTask
    {
        private MSBuildCommand msbuild;
        private NUnit3Command nunit;

        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            msbuild = new MSBuildCommand(Context) {
                Exe = Context.AgentVariables["global"]["msbuild_exe"],
                WorkingDirectory = Context.ContentDirectory,
            };

            nunit = new NUnit3Command(Context) {
                Exe = Context.AgentVariables["global"]["nunit_exe"],
                WorkingDirectory = Context.ContentDirectory,
            };

            await BuildSolution(token);
            await UnitTest(token);
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

        private async Task UnitTest(CancellationToken token)
        {
            await nunit.RunAsync(new NUnit3Arguments {
                InputFiles = {
                    "Jenkins.NET.Tests\\bin\\Release\\Jenkins.NET.Tests.dll",
                },
                Where = "cat == 'unit'",
            }, token);
        }
    }
}

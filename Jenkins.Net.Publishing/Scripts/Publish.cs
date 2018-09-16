using Jenkins.NET.Publishing.Tools;
using Photon.Framework.Agent;
using Photon.Framework.Process;
using Photon.Framework.Tasks;
using Photon.NuGet.CorePlugin;
using Photon.NuGetPlugin;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing.Scripts
{
    public class Publish : IBuildTask
    {
        private NuGetCore nugetCore;

        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            nugetCore = new NuGetCore(Context) {
                ApiKey = Context.ServerVariables["global"]["nuget/apiKey"],
            };
            nugetCore.Initialize();

            await BuildTools.BuildSolution(Context, token);
            await TestTools.UnitTest(Context, token);

            await PublishPackage(token);
        }

        private async Task PublishPackage(CancellationToken token)
        {
            var packageDir = Path.Combine(Context.WorkDirectory, "Packages");

            await Pack(packageDir, token);

            var packageFilename = Directory
                .GetFiles(packageDir, "jenkinsnet.*.nupkg")
                .FirstOrDefault();

            if (string.IsNullOrEmpty(packageFilename))
                throw new ApplicationException("No package found matching package ID 'jenkinsnet'!");

            await nugetCore.PushAsync(packageFilename, token);
        }

        private async Task Pack(string packageDir, CancellationToken token)
        {
            var args = new[] {
                "pack",
                "\"Jenkins.Net\\Jenkins.Net.csproj\"",
                "--configuration Release",
                "--no-build",
                $"--output \"{packageDir}\"",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = Context.ContentDirectory,
            };

            var runner = new ProcessRunner(Context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Build Failed! [{result.ExitCode}]");
        }
    }
}

using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using Photon.Framework.Tools;
using Photon.MSBuildPlugin;
using Photon.NuGet.CorePlugin;
using Photon.NuGetPlugin;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Publish_Windows : IBuildTask
    {
        private MSBuildCommand msbuild;
        private NuGetCore nugetCore;
        private NuGetCommand nugetCmd;

        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            msbuild = new MSBuildCommand(Context) {
                Exe = Context.AgentVariables["global"]["msbuild_exe"],
                WorkingDirectory = Context.ContentDirectory,
            };

            nugetCore = new NuGetCore(Context) {
                ApiKey = Context.ServerVariables["global"]["nuget/apiKey"],
            };
            nugetCore.Initialize();

            nugetCmd = new NuGetCommand(Context) {
                Exe = Path.Combine(Context.ContentDirectory, "bin", "NuGet.exe"), //Context.AgentVariables["global"]["nuget_exe"];
                WorkingDirectory = Context.ContentDirectory,
            };

            await BuildSolution(token);

            await PublishPackage(token);
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

        private async Task PublishPackage(CancellationToken token)
        {
            const string packageId = "jenkinsnet";
            var assemblyFilename = Path.Combine(Context.ContentDirectory, "Jenkins.NET", "bin", "Release", "Jenkins.NET.dll");
            var packageDefinitionFilename = Path.Combine(Context.ContentDirectory, "Jenkins.NET", "Jenkins.NET.csproj");
            var nugetPackageDir = Path.Combine(Context.WorkDirectory, "Packages");
            var assemblyVersion = AssemblyTools.GetVersion(assemblyFilename);


            var versionList = await nugetCore.GetAllPackageVersions(packageId, token);
            var packageVersion = versionList.Any() ? versionList.Max() : null;

            if (!VersionTools.HasUpdates(packageVersion, assemblyVersion)) {
                Context.Output.WriteLine($"Package '{packageId}' is up-to-date. Version {packageVersion}", ConsoleColor.DarkCyan);
                return;
            }

            await nugetCmd.RunAsync(new NuGetPackArguments {
                Filename = packageDefinitionFilename,
                Version = assemblyVersion,
                OutputDirectory = nugetPackageDir,
                Properties = {
                    ["Configuration"] = "Release",
                    ["Platform"] = "AnyCPU",
                    ["Version"] = assemblyVersion,
                },
            }, token);

            var packageFilename = Directory
                .GetFiles(nugetPackageDir, $"{packageId}.*.nupkg")
                .FirstOrDefault();

            if (string.IsNullOrEmpty(packageFilename))
                throw new ApplicationException($"No package found matching package ID '{packageId}'!");

            await nugetCore.PushAsync(packageFilename, token);
        }
    }
}

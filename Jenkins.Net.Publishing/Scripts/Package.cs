using Jenkins.NET.Publishing.Tools;
using Photon.Framework.Agent;
using Photon.Framework.Packages;
using Photon.Framework.Process;
using Photon.Framework.Tasks;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Photon.Framework.Extensions;

namespace Jenkins.NET.Publishing.Scripts
{
    public class Package : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            var packageDir = Path.Combine(Context.ContentDirectory, "PublishPackages");

            await BuildTools.BuildSolution(Context, token);
            await TestTools.UnitTest(Context, token);

            await CreateNugetPackage(packageDir, token);

            await CreateProjectPackage(token);
        }

        private async Task CreateProjectPackage(CancellationToken token)
        {
            var def = new ProjectPackageDefinition {
                Id = "jenkinsnet",
                Name = "Jenkins.NET",
                Description = "Publishes a pre-compiled and pre-tested NuGet Package for Jenkins.NET.",
                Project = "jenkins.net",
                Assembly = "Jenkins.Net.Publishing.dll",
                Script = "Publish",
                Files = {
                    new PackageFileDefinition {
                        Path = "Jenkins.Net.Publishing\\bin\\Debug\\**",
                        Destination = "",
                    },
                    new PackageFileDefinition {
                        Path = "PublishPackages\\**",
                        Destination = "PublishPackages",
                    }
                }
            };

            var version = Context.BuildNumber.ToString();
            var output = Path.Combine(Context.ContentDirectory, "PublishPackages", "Jenkins.Net.zip");

            try {
                Context.WriteTagLine("Creating project package...", ConsoleColor.White);

                await ProjectPackageTools.CreatePackage(
                    definition: def,
                    rootPath: Context.ContentDirectory,
                    version: version,
                    outputFilename: output);

                Context.WriteTagLine("Created project package successfully.", ConsoleColor.White);
            }
            catch (Exception error) {
                Context.WriteErrorBlock("Failed to create project package!", error.UnfoldMessages());
                throw;
            }

            try {
                Context.WriteTagLine("Publishing project package...", ConsoleColor.White);

                await Context.Packages.PushProjectPackageAsync(output, token);

                Context.WriteTagLine("Published project package successfully.", ConsoleColor.White);
            }
            catch (Exception error) {
                Context.WriteErrorBlock("Failed to publish project package!", error.UnfoldMessages());
                throw;
            }
        }

        private async Task CreateNugetPackage(string packageDir, CancellationToken token)
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

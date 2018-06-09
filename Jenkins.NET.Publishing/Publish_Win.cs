using Jenkins.NET.Publishing.Internal;
using Photon.Framework.Agent;
using Photon.NuGetPlugin;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    internal class Publish_Win
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution(token);

            await PublishPackage(token);
        }

        private async Task BuildSolution(CancellationToken token)
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

        private async Task PublishPackage(CancellationToken token)
        {
            var nugetTool = new NuGetPackagePublisher(Context) {
                Mode = NugetModes.Hybrid,
                Client = new NuGetCore {
                    EnableV2 = false,
                    EnableV3 = true,
                    Output = Context.Output,
                    SourceUrl = "?",
                    ApiKey = "?",
                },
                CL = new NuGetCommandLine {
                    Output = Context.Output,
                },
                PackageId = "Jenkins.NET",
                PackageDefinition = Path.Combine(Context.ContentDirectory, "Jenkins.NET", "Jenkins.NET.csproj"),
                PackageDirectory = Path.Combine(Context.WorkDirectory, "Packages"),
                Configuration = "Release",
                Platform = "Any CPU",
            };

            await nugetTool.PublishAsync(token);
        }
    }
}

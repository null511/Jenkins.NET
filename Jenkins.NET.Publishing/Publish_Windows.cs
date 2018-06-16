﻿using Jenkins.NET.Publishing.Internal;
using Photon.Framework.Agent;
using Photon.Framework.Tasks;
using Photon.Framework.Tools;
using Photon.NuGetPlugin;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    public class Publish_Windows : IBuildTask
    {
        public IAgentBuildContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            await BuildSolution(token);

            await PublishPackage(token);
        }

        private async Task BuildSolution(CancellationToken token)
        {
            var msbuild_exe = Context.AgentVariables["global"]["msbuild_exe"];

            var msBuild = new MsBuild(Context) {
                //Exe = ".\\bin\\msbuild.cmd",
                Exe = $"\"{msbuild_exe}\"",
                Filename = "Jenkins.NET.sln",
                Configuration = "Release",
                Platform = "Any CPU",
                Parallel = true,
            };

            await msBuild.BuildAsync();
        }

        private async Task PublishPackage(CancellationToken token)
        {
            var assemblyFilename = Path.Combine(Context.ContentDirectory, "Jenkins.NET", "bin", "Release", "Jenkins.NET.dll");
            var version = AssemblyTools.GetVersion(assemblyFilename);

            var apiKey = Context.ServerVariables["global"]["nuget.apiKey"];

            var nugetTool = new NuGetPackagePublisher(Context) {
                Mode = NugetModes.Hybrid,
                Client = new NuGetCore {
                    EnableV3 = true,
                    Output = Context.Output,
                    ApiKey = apiKey,
                },
                CL = new NuGetCommandLine {
                    ExeFilename = Path.Combine(Context.ContentDirectory, "bin", "NuGet.exe"),
                    Output = Context.Output,
                },
                PackageId = "jenkinsnet",
                PackageDefinition = Path.Combine(Context.ContentDirectory, "Jenkins.NET", "Jenkins.NET.csproj"),
                PackageDirectory = Path.Combine(Context.WorkDirectory, "Packages"),
                //Configuration = "Release",
                //Platform = "AnyCPU",
                Version = version,
                PackProperties = {
                    ["configuration"] = "Release",
                    ["platform"] = "AnyCPU",
                },
            };

            nugetTool.Client.Initialize();

            await nugetTool.PublishAsync(token);
        }
    }
}

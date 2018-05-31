﻿using Jenkins.NET.Publishing.Internal;
using Photon.Framework.Agent;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing
{
    internal class Build_Win
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

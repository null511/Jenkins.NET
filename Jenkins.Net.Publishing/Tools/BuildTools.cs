using Photon.Framework.Agent;
using Photon.Framework.Process;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing.Tools
{
    internal static class BuildTools
    {
        public static async Task BuildSolution(IAgentContext context, CancellationToken token)
        {
            var args = new[] {
                "build",
                "\"Jenkins.Net.sln\"",
                "--configuration Release",
                "--no-incremental",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = context.ContentDirectory,
            };

            var runner = new ProcessRunner(context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Build Failed! [{result.ExitCode}]");
        }
    }
}

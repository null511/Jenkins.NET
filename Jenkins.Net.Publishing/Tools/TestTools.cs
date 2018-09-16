using Photon.Framework.Agent;
using Photon.Framework.Process;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing.Tools
{
    internal static class TestTools
    {
        public static async Task UnitTest(IAgentContext context, CancellationToken token)
        {
            var args = new[] {
                "test",
                "\"Jenkins.Net.Tests\\Jenkins.Net.Tests.csproj\"",
                "--configuration Release",
                "--no-build",
                "--filter Category=unit",
            };

            var info = new ProcessRunInfo {
                Filename = "dotnet",
                Arguments = string.Join(" ", args),
                WorkingDirectory = context.ContentDirectory,
            };

            var runner = new ProcessRunner(context);
            var result = await runner.RunAsync(info, token);

            if (result.ExitCode != 0) throw new ApplicationException($"Unit-Test Failed! [{result.ExitCode}]");
        }
    }
}

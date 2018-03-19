using JenkinsNET.Utilities;
using System;
using System.IO;

namespace JenkinsNET.Tests.Internal
{
    internal static class DefaultRunner
    {
        public static JenkinsJobRunner Create(TextWriter writer, JenkinsClient client = null)
        {
            client = client ?? DefaultClient.Create();

            var jobRunner = new JenkinsJobRunner(client) {
                MonitorConsoleOutput = true,
            };

            jobRunner.StatusChanged += () => {
                writer.WriteLine($"[{DateTime.Now}] Status: '{jobRunner.Status}'");
            };

            jobRunner.ConsoleOutputChanged += newText => {
                writer.Write(newText);
            };

            return jobRunner;
        }
    }
}

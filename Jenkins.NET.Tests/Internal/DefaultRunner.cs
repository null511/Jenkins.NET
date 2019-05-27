using JenkinsNET.Utilities;
using System;

#if NETCORE
using Xunit.Abstractions;
#else
using System.IO;
#endif

namespace JenkinsNET.Tests.Internal
{
    internal static class DefaultRunner
    {
    #if NETCORE
        public static JenkinsJobRunner Create(ITestOutputHelper writer, JenkinsClient client = null, bool quiet = false)
    #else
        public static JenkinsJobRunner Create(TextWriter writer, JenkinsClient client = null, bool quiet = false)
    #endif
        {
            client = client ?? DefaultClient.Create();

            var jobRunner = new JenkinsJobRunner(client) {
                MonitorConsoleOutput = !quiet,
            };

            if (!quiet) {
                jobRunner.StatusChanged += () => {
                    writer.WriteLine($"[{DateTime.Now}] Status: '{jobRunner.Status}'");
                };

            #if NETCORE
                // Write method not available!
                jobRunner.ConsoleOutputChanged += writer.WriteLine;
            #else
                jobRunner.ConsoleOutputChanged += writer.Write;
            #endif
            }

            return jobRunner;
        }
    }
}

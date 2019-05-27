using JenkinsNET.Models;
using JenkinsNET.Tests.Internal;
using System;
using Xunit;
using Assert = Xunit.Assert;
#if NET_ASYNC
using System.Threading.Tasks;
#endif

#if NETCORE
using Xunit.Abstractions;
#else
using System.IO;
#endif

namespace JenkinsNET.Tests.IntegrationTests
{
    public class ClientTests
    {
        private const string jobName = "Test Job";


    #if NETCORE
        private readonly ITestOutputHelper output;

        public ClientTests(ITestOutputHelper output)
        {
            this.output = output;
        }
    #else
        private readonly TextWriter output;

        public ClientTests()
        {
            this.output = Console.Out;
        }
    #endif

        [Fact]
        [Trait("Category", "Integration")]
        public void ListAllJobs()
        {
            var client = DefaultClient.Create();

            var jenkins = client.Get();

            foreach (var job in jenkins.Jobs) {
                output.WriteLine($"{job.Name} [{job.Class}]");

                var jobBase = client.Jobs.Get<JenkinsJobBase>(job.Name);
                output.WriteLine($"  {jobBase.FullDisplayName}");
                output.WriteLine($"  {jobBase.Url}");
            }
        }

    #if NET_ASYNC
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ListAllJobsAsync()
        {
            var client = DefaultClient.Create();

            var jenkins = await client.GetAsync();

            foreach (var job in jenkins.Jobs) {
                output.WriteLine($"{job.Name} [{job.Class}]");

                var jobBase = await client.Jobs.GetAsync<JenkinsJobBase>(job.Name);
                output.WriteLine($"  {jobBase.FullDisplayName}");
                output.WriteLine($"  {jobBase.Url}");
            }
        }
    #endif

        [Fact]
        [Trait("Category", "Integration")]
        public void Run()
        {
            var jobRunner = DefaultRunner.Create(output);

            var startTime = DateTime.Now;
            var build = jobRunner.Run(jobName);
            var duration = DateTime.Now.Subtract(startTime);

            output.WriteLine(string.Empty);
            output.WriteLine($"Result: {build.Result}");
            output.WriteLine($"Duration: {duration}");
        }

    #if NET_ASYNC
        [Fact]
        [Trait("Category", "Integration")]
        public async Task RunAsync()
        {
            var jobRunner = DefaultRunner.Create(output);

            var startTime = DateTime.Now;
            var build = await jobRunner.RunAsync(jobName);
            var duration = DateTime.Now.Subtract(startTime);

            output.WriteLine(string.Empty);
            output.WriteLine($"Result: {build.Result}");
            output.WriteLine($"Duration: {duration}");
        }
    #endif

        [Fact]
        [Trait("Category", "Integration")]
        public void GetConsoleText()
        {
            var jobRunner = DefaultRunner.Create(output, quiet: true);

            output.WriteLine("Running job...");
            var build = jobRunner.Run(jobName);
            output.WriteLine("Job complete.");

            Assert.Equal("SUCCESS", build.Result);
            Assert.True(jobRunner.BuildNumber.HasValue);

            var text = jobRunner.Client.Builds.GetConsoleText(jobName, jobRunner.BuildNumber.Value.ToString());
            output.WriteLine(text);

            Assert.Contains("[Hello World!]", text);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetConsoleHtml()
        {
            var jobRunner = DefaultRunner.Create(output, quiet: true);

            output.WriteLine("Running job...");
            var build = jobRunner.Run(jobName);
            output.WriteLine("Job complete.");

            Assert.Equal("SUCCESS", build.Result);
            Assert.True(jobRunner.BuildNumber.HasValue);

            var text = jobRunner.Client.Builds.GetConsoleHtml(jobName, jobRunner.BuildNumber.Value.ToString());
            output.WriteLine(text);

            Assert.Contains("<html>", text);
            Assert.Contains("[Hello World!]", text);
        }
    }
}

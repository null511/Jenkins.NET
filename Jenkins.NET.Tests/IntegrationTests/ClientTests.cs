using JenkinsNET.Models;
using JenkinsNET.Tests.Internal;
using System;
using Xunit;

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
    }
}

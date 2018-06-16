using JenkinsNET.Models;
using JenkinsNET.Tests.Internal;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JenkinsNET.Tests.IntegrationTests
{
    [IntegrationTestFixture]
    public class ClientTests
    {
        private const string jobName = "Test Job";


        [Test]
        public async Task ListAllJobsAsync()
        {
            var client = DefaultClient.Create();

            var jenkins = await client.GetAsync();

            foreach (var job in jenkins.Jobs) {
                TestContext.Out.WriteLine($"{job.Name} [{job.Class}]");

                var jobBase = await client.Jobs.GetAsync<JenkinsJobBase>(job.Name);
                TestContext.Out.WriteLine($"  {jobBase.FullDisplayName}");
                TestContext.Out.WriteLine($"  {jobBase.Url}");
            }
        }

        [Test]
        public void Run()
        {
            var jobRunner = DefaultRunner.Create(TestContext.Out);

            var startTime = DateTime.Now;
            var build = jobRunner.Run(jobName);
            var duration = DateTime.Now.Subtract(startTime);

            TestContext.Out.WriteLine();
            TestContext.Out.WriteLine($"Result: {build.Result}");
            TestContext.Out.WriteLine($"Duration: {duration}");
        }

        [Test]
        public async Task RunAsync()
        {
            var jobRunner = DefaultRunner.Create(TestContext.Out);

            var startTime = DateTime.Now;
            var build = await jobRunner.RunAsync(jobName);
            var duration = DateTime.Now.Subtract(startTime);

            TestContext.Out.WriteLine();
            TestContext.Out.WriteLine($"Result: {build.Result}");
            TestContext.Out.WriteLine($"Duration: {duration}");
        }
    }
}

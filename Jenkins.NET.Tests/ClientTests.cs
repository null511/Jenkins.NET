using JenkinsNET.IntegrationTests.Internal;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JenkinsNET.IntegrationTests
{
    [TestFixture]
    public class ClientTests
    {
        private const string jobName = "Test Job";


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

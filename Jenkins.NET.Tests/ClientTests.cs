using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JenkinsNET.IntegrationTests
{
    [TestFixture]
    public class ClientTests
    {
        private const string jenkinsUrl = "http://localhost:8080";
        private const string jobName = "Test Job";
        private const string username = "guest";
        private const string password = "bcb954a77ab47750201a9f188b1b25e8";


        [Test]
        public async Task Run()
        {
            var client = new JenkinsClient {
                BaseUrl = jenkinsUrl,
                UserName = username,
                Password = password,
            };

            var jobRunner = new JenkinsJobRunner(client);
            jobRunner.StatusChanged += () => {
                TestContext.Out.WriteLine($"[{DateTime.Now}] Status: '{jobRunner.Status}'");
            };
            jobRunner.ConsoleOutputChanged += newText => {
                TestContext.Out.Write(newText);
            };

            var startTime = DateTime.Now;
            var build = await jobRunner.RunAsync(jobName);
            var duration = DateTime.Now.Subtract(startTime);

            TestContext.Out.WriteLine();
            TestContext.Out.WriteLine($"Result: {build.Result}");
            TestContext.Out.WriteLine($"Duration: {duration}");
        }
    }
}

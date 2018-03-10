using JenkinsNET.IntegrationTests.Internal;
using JenkinsNET.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace JenkinsNET.IntegrationTests
{
    [TestFixture]
    public class CrumbTests
    {
        [Test]
        public async Task DeleteTest_WithoutCrumb()
        {
            var client = DefaultClient.Create();

            var createJob = await client.Jobs.GetAsync("Test Job");

            var deleteJob = new JenkinsJob(createJob.Node);

            await client.Jobs.CreateAsync("Delete Job", deleteJob);

            await client.Jobs.DeleteAsync("Delete Job");
        }

        [Test]
        public async Task DeleteTest_WithCrumb()
        {
            var client = DefaultClient.Create();

            var crumb = await client.GetSecurityCrumbAsync();

            var createJob = await client.Jobs.GetAsync("Test Job");

            var deleteJob = new JenkinsJob(createJob.Node);

            await client.Jobs.CreateAsync("Delete Job", deleteJob);

            await client.Jobs.DeleteAsync("Delete Job");
        }
    }
}

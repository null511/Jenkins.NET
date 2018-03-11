using JenkinsNET.IntegrationTests.Internal;
using JenkinsNET.Models;
using NUnit.Framework;
using System.Linq;
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

            await SetupDeleteJob(client);

            await client.Jobs.DeleteAsync("Delete Job");
        }

        [Test]
        public async Task DeleteTest_WithCrumb()
        {
            var client = DefaultClient.Create();

            await client.UpdateSecurityCrumbAsync();

            await SetupDeleteJob(client);

            await client.Jobs.DeleteAsync("Delete Job");
        }

        private async Task SetupDeleteJob(JenkinsClient client)
        {
            var jenkins = await client.GetAsync();

            if (jenkins.Jobs.Any(x => string.Equals(x.Name, "Delete Job", System.StringComparison.OrdinalIgnoreCase)))
                return;

            var createJob = await client.Jobs.GetConfigurationAsync("Test Job");

            var deleteJob = new JenkinsProject(createJob.Node);

            await client.Jobs.CreateAsync("Delete Job", deleteJob);
        }
    }
}

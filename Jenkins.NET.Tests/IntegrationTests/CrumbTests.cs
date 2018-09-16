using JenkinsNET.Models;
using JenkinsNET.Tests.Internal;
using System.Linq;
using Xunit;

#if NET_ASYNC
using System.Threading.Tasks;
#endif

namespace JenkinsNET.Tests.IntegrationTests
{
    public class CrumbTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void DeleteTest_WithoutCrumb()
        {
            var client = DefaultClient.Create();

            SetupDeleteJob(client);

            client.Jobs.Delete("Delete Job");
        }

    #if NET_ASYNC
        [Fact]
        [Trait("Category", "Integration")]
        public async Task DeleteTest_WithoutCrumb_Async()
        {
            var client = DefaultClient.Create();

            await SetupDeleteJobAsync(client);

            await client.Jobs.DeleteAsync("Delete Job");
        }
    #endif

        [Fact]
        [Trait("Category", "Integration")]
        public void DeleteTest_WithCrumb()
        {
            var client = DefaultClient.Create();

            client.UpdateSecurityCrumb();

            SetupDeleteJob(client);

            client.Jobs.Delete("Delete Job");
        }

    #if NET_ASYNC
        [Fact]
        [Trait("Category", "Integration")]
        public async Task DeleteTest_WithCrumb_Async()
        {
            var client = DefaultClient.Create();

            await client.UpdateSecurityCrumbAsync();

            await SetupDeleteJobAsync(client);

            await client.Jobs.DeleteAsync("Delete Job");
        }
    #endif

        private static void SetupDeleteJob(JenkinsClient client)
        {
            var jenkins = client.Get();

            if (jenkins.Jobs.Any(x => string.Equals(x.Name, "Delete Job", System.StringComparison.OrdinalIgnoreCase)))
                return;

            var createJob = client.Jobs.GetConfiguration("Test Job");

            var deleteJob = new JenkinsProject(createJob.Node);

            client.Jobs.Create("Delete Job", deleteJob);
        }

    #if NET_ASYNC
        private static async Task SetupDeleteJobAsync(JenkinsClient client)
        {
            var jenkins = await client.GetAsync();

            if (jenkins.Jobs.Any(x => string.Equals(x.Name, "Delete Job", System.StringComparison.OrdinalIgnoreCase)))
                return;

            var createJob = await client.Jobs.GetConfigurationAsync("Test Job");

            var deleteJob = new JenkinsProject(createJob.Node);

            await client.Jobs.CreateAsync("Delete Job", deleteJob);
        }
    #endif
    }
}

using JenkinsNET.IntegrationTests.Internal;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace JenkinsNET.IntegrationTests
{
    [TestFixture]
    public class CrumbTests
    {
        private const string jobName = "Test Job";


        [Test]
        public async Task DeleteTest_WithoutCrumb()
        {
            var client = DefaultClient.Create();

            await client.Jobs.DeleteAsync("Delete Job");
        }

        [Test]
        public async Task DeleteTest_WithCrumb()
        {
            var client = DefaultClient.Create();

            var crumb = await client.GetSecurityCrumbAsync();

            await client.Jobs.DeleteAsync("Delete Job");
        }
    }
}

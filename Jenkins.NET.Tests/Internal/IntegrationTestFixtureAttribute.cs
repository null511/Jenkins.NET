using NUnit.Framework;

namespace JenkinsNET.Tests.Internal
{
    internal class IntegrationTestFixtureAttribute : CategoryAttribute
    {
        public IntegrationTestFixtureAttribute() : base("integration") {}
    }
}

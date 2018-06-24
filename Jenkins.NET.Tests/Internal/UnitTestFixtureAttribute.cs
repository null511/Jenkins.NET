using NUnit.Framework;

namespace JenkinsNET.Tests.Internal
{
    internal class UnitTestFixtureAttribute : CategoryAttribute
    {
        public UnitTestFixtureAttribute() : base("unit") {}
    }
}

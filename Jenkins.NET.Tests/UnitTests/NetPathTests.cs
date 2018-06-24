using JenkinsNET.Internal;
using JenkinsNET.Tests.Internal;
using NUnit.Framework;

namespace JenkinsNET.Tests.UnitTests
{
    [UnitTestFixture]
    public class NetPathTests
    {
        [Test]
        public void JoinsNoSlash()
        {
            Assert.That(NetPath.Combine("root", "path"), Is.EqualTo("root/path"));
        }

        [Test]
        public void JoinsLeftSlash()
        {
            Assert.That(NetPath.Combine("root/", "path"), Is.EqualTo("root/path"));
        }

        [Test]
        public void JoinsRightSlash()
        {
            Assert.That(NetPath.Combine("root", "/path"), Is.EqualTo("root/path"));
        }

        [Test]
        public void JoinsBothSlash()
        {
            Assert.That(NetPath.Combine("root/", "/path"), Is.EqualTo("root/path"));
        }

        [Test]
        public void JoinsMultiple()
        {
            Assert.That(NetPath.Combine("root", "path1", "path2", "path3"), Is.EqualTo("root/path1/path2/path3"));
        }
    }
}

using JenkinsNET.Internal;
using Xunit;

namespace JenkinsNET.Tests.UnitTests
{
    public class NetPathTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsNoSlash()
        {
            Assert.Equal(NetPath.Combine("root", "path"), "root/path");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsLeftSlash()
        {
            Assert.Equal(NetPath.Combine("root/", "path"), "root/path");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsRightSlash()
        {
            Assert.Equal(NetPath.Combine("root", "/path"), "root/path");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsBothSlash()
        {
            Assert.Equal(NetPath.Combine("root/", "/path"), "root/path");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsMultiple()
        {
            Assert.Equal(NetPath.Combine("root", "path1", "path2", "path3"), "root/path1/path2/path3");
        }
    }
}

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
            Assert.Equal("root/path", NetPath.Combine("root", "path"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsLeftSlash()
        {
            Assert.Equal("root/path", NetPath.Combine("root/", "path"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsRightSlash()
        {
            Assert.Equal("root/path", NetPath.Combine("root", "/path"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsBothSlash()
        {
            Assert.Equal("root/path", NetPath.Combine("root/", "/path"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void JoinsMultiple()
        {
            Assert.Equal("root/path1/path2/path3", NetPath.Combine("root", "path1", "path2", "path3"));
        }
    }
}

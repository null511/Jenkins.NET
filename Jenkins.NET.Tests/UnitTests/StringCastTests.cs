using JenkinsNET.Internal;
using Xunit;

namespace JenkinsNET.Tests.UnitTests
{
    public class StringCastTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Null_To_String()
        {
            Assert.Null(((string)null).To<string>());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Null_To_Int()
        {
            Assert.Equal(((string)null).To<int>(), 0);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Null_To_NullableInt()
        {
            Assert.Null(((string)null).To<int?>());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void To_String()
        {
            Assert.Equal("2".To<int>(), 2);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void To_Int()
        {
            Assert.Equal("2".To<int>(), 2);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void To_Double()
        {
            Assert.Equal("123.456".To<double>(), 123.456d);
        }
    }
}

using JenkinsNET.Internal;
using NUnit.Framework;

namespace JenkinsNET.UnitTests
{
    [TestFixture]
    public class StringCastTests
    {
        [Test]
        public void Null_To_String()
        {
            Assert.That(((string)null).To<string>(), Is.EqualTo(null));
        }

        [Test]
        public void Null_To_Int()
        {
            Assert.That(((string)null).To<int>(), Is.EqualTo(0));
        }

        [Test]
        public void Null_To_NullableInt()
        {
            Assert.That(((string)null).To<int?>(), Is.EqualTo(null));
        }

        [Test]
        public void To_String()
        {
            Assert.That("2".To<int>(), Is.EqualTo(2));
        }

        [Test]
        public void To_Int()
        {
            Assert.That("2".To<int>(), Is.EqualTo(2));
        }

        [Test]
        public void To_Double()
        {
            Assert.That("123.456".To<double>(), Is.EqualTo(123.456d));
        }
    }
}

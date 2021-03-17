using NUnit.Framework;

namespace NUnitTestGenerator.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var sut = new NUnitTestGenerator(
                new [] {"Creating a test should generate full template"})
                {
                    Fixture = "TestFixture"
                };

            var r = sut.GenerateTests();

            Assert.That(r, Is.Not.Null.And.Not.Empty);
            System.Console.WriteLine(r);

        }
    }
}
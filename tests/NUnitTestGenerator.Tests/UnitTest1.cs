using NUnit.Framework;

using System.Collections.Generic;
using System.IO;

namespace NUnitTestGenerator.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        public static string GetExpected(string rel) =>
            File.ReadAllText(Directory.GetCurrentDirectory() + $@"\Resources\{rel}.res");
        
        public static IEnumerable<TestCaseData> TestCases => new []
        {
            new TestCaseData(
                 new [] 
                 { 
                     "generating a single test should create a valid result" 
                 }, 
                 GetExpected("GenerateSingleNoParams")),
            
            new TestCaseData(
                 new [] 
                 { 
                    "generating a single test should create a valid result",
                    "generating multiple tests should create a valid result" 
                 }, 
                 GetExpected("GenerateMultipleNoParams"))
        };

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Generating_Tests_With_No_Params_Should_Return_Expected_Results(IEnumerable<string> tests, string expected)
        {
            var sut = new NUnitTestGenerator("TestFixture", tests);

            var r = sut.GenerateTests();

            Assert.That(r, Is.Not.Null.And.Not.Empty);
            Assert.That(r, Is.EqualTo(expected));
        }
    }
}
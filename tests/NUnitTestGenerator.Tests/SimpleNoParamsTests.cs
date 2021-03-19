using NUnit.Framework;

using System.Collections.Generic;
using System.IO;

namespace NUnitTestGenerator.Tests
{

    public static class TestHelpers
    {
        public static string GetExpected(string rel) =>
            File.ReadAllText(Directory.GetCurrentDirectory() + $@"\Resources\{rel}.res");

    }

    [TestFixture]
    public class GenerationWithNoParameters
    {
        public static IEnumerable<TestCaseData> TestCasesNoParams => new []
        {
            new TestCaseData(
                 new [] 
                 { 
                     "generating a single test should create a valid result" 
                 }, 
                 TestHelpers.GetExpected("GenerateSingleNoParams")),
            
            new TestCaseData(
                 new [] 
                 { 
                    "generating a single test should create a valid result",
                    "generating multiple tests should create a valid result" 
                 },
                 TestHelpers.GetExpected("GenerateMultipleNoParams"))
        };

        [Test]
        [TestCaseSource(nameof(TestCasesNoParams))]
        public void Generating_Tests_With_No_Params_Should_Return_Expected_Results(IEnumerable<string> tests, string expected)
        {
            var sut = new NUnitTestGenerator("TestFixture", tests);

            var r = sut.GenerateTests();

            Assert.That(r, Is.Not.Null.And.Not.Empty);
            Assert.That(r, Is.EqualTo(expected));
        }

    }

    [TestFixture]
    public class GenerationWithParametersTests
    {

        public static IEnumerable<TestCaseData> TestCasesWithParams => new[]
{
            new TestCaseData(
                 new []
                 {
                     "generating a {single:string} test should create a valid result"
                 },
                 TestHelpers.GetExpected("GenerateSingleWithSingleParam"))
            .SetName("Generate single test with single parameter"),

            new TestCaseData(
                 new []
                 {
                     "generating a {single:string} test should create a {valid:bool} result"
                 },
                 TestHelpers.GetExpected("GenerateSingleWithMultipleParams"))
            .SetName("Generate single test with multiple parameters"),

            new TestCaseData(
                 new []
                 {
                    "generating a {single:string} test should create a valid result",
                    "generating {multiple:IEnumerable<string>} tests should create a valid result"
                 },
                 TestHelpers.GetExpected("GenerateMultipleWithSingleParam"))
            .SetName("Generate multiple tests with single parameter each"),

            new TestCaseData(
                 new []
                 {
                    "generating a {single:string} test should create a {valid:bool} result",
                    "generating {multiple:IEnumerable<string>} tests should create a {valid:bool} result"
                 },
                 TestHelpers.GetExpected("GenerateMultipleNoParams"))
            .SetName("Generate multiple tests with multiple parameters each"),
        };


        [Test]
        [TestCaseSource(nameof(TestCasesWithParams))]
        public void Generating_Tests_With_Parameters_Should_Create_Tests_With_Parameters(IEnumerable<string> tests, string expected)
        {
            var sut = new NUnitTestGenerator("TestFixture", tests);

            var r = sut.GenerateTests();

            Assert.That(r, Is.Not.Null.And.Not.Empty);
            Assert.That(r, Is.EqualTo(expected));
        }

    }
}
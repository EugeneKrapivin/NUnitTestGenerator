using NUnit.Framework;

using System.Collections.Generic;
using System.IO;

namespace NUnitTestGenerator.Tests
{

    public static class TestHelpers
    {
        public static string GetExpected(string rel) =>
            File.ReadAllText(Directory.GetCurrentDirectory() + $@"/Resources/{rel}.res");

    }

    [TestFixture]
    public class BasicTestGenerationTests
    {
        public static IEnumerable<TestCaseData> TestCasesNoParams => new []
        {
            new TestCaseData(
                 new [] 
                 { 
                     "generating a single test should create a valid result" 
                 }, 
                 TestHelpers.GetExpected("GenerateSingleNoParams"))
            .SetName("generating a single test with no parameters should create a valid result"),


            new TestCaseData(
                 new [] 
                 { 
                    "generating a single test should create a valid result",
                    "generating multiple tests should create a valid result" 
                 },
                 TestHelpers.GetExpected("GenerateMultipleNoParams"))
            .SetName("generating a multiple tests with no parameters should create a valid results")
        };
      
        public static IEnumerable<TestCaseData> TestCasesWithParams => new[]
{
            new TestCaseData(
                 new []
                 {
                     "generating a {single:string} test should create a valid result"
                 },
                 TestHelpers.GetExpected("GenerateSingleWithSingleParam"))
            .SetName("generate single test with single parameter"),

            new TestCaseData(
                 new []
                 {
                     "generating a {single:string} test should create a {valid:bool} result"
                 },
                 TestHelpers.GetExpected("GenerateSingleWithMultipleParams"))
            .SetName("generate single test with multiple parameters"),

            new TestCaseData(
                 new []
                 {
                    "generating a {single:string} test should create a valid result",
                    "generating {multiple:IEnumerable<string>} tests should create a valid result"
                 },
                 TestHelpers.GetExpected("GenerateMultipleWithSingleParam"))
            .SetName("generate multiple tests with single parameter each"),

            new TestCaseData(
                 new []
                 {
                    "generating a {single:string} test should create a {valid:bool} result",
                    "generating {multiple:IEnumerable<string>} tests should create a {valid:bool} result"
                 },
                 TestHelpers.GetExpected("GenerateMultipleWithMultipleParams"))
            .SetName("generate multiple tests with multiple parameters each"),
        };

        [TestCaseSource(nameof(TestCasesNoParams))]
        [TestCaseSource(nameof(TestCasesWithParams))]
        public void Generating_Basic_Tests_Should_Return_Expected_Value(IEnumerable<string> tests, string expected)
        {
            var sut = new NUnitTestGenerator("TestFixture", tests);

            var r = sut.GenerateTests();

            Assert.That(r, Is.Not.Null.And.Not.Empty);
            Assert.That(r, Is.EqualTo(expected));
        }

    }
}
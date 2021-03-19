using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NUnitTestGenerator
{
    public class NUnitTestGenerator
    {
        public FixtureDescriptor Fixture { get; }
        private List<TestDescriptor> _tests = new();

        public IEnumerable<TestDescriptor> Tests => _tests;
        private readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public NUnitTestGenerator(string fixture, IEnumerable<string> tests)
        {
            var testDescriptors = tests.Select(x => new TestDescriptor { Title = x }).ToList();
            _tests.AddRange(testDescriptors);
            
            Fixture = new FixtureDescriptor
            {
                FixtureName = fixture
            };
        }

        public string GenerateTests()
        {
            var sb = new StringBuilder();
            sb.Append(
@$"using NUnit.Framework;

namespace {Fixture.FixtureName}.Tests
{{
    [TestFixture(Description = ""{Fixture.FixtureName}"")]
	public class {Fixture.FixtureName}Tests
	{{
");

            foreach (var test in _tests)
            {
                sb.AppendLine();
                sb.AppendJoin("\r\n", GenerateTest(test).Select(x => $"\t\t{x}"));
                sb.AppendLine();
            }
            sb.Append(
@$"	}}
}}");
            return sb.ToString();
        }

        private static readonly Regex ParametersRegex = new Regex(@"(?:(?:\w+)|{(?<param>\w+:\w+)})", RegexOptions.Compiled);

        private IEnumerable<string> GenerateTest(TestDescriptor test)
        {
            var title = textInfo
                .ToTitleCase(test.Title)
                .Replace(' ', '_')
                .Replace('-', '_');

            var parameters = ParametersRegex.Matches(test.Title);

            return
$@"[Test(Description = ""{test.Title}"")]
public void {title}()
{{
	// Arrange
	
	// Act
	
	// Assert
	Assert.Fail();
}}".Split("\r\n");
        }
    }
}

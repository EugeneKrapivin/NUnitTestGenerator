using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using static NUnitTestGenerator.TestDescriptor;

namespace NUnitTestGenerator
{
    public class TestGenerator
    {
        public FixtureDescriptor Fixture { get; }
        private readonly List<TestDescriptor> _tests = new();

        public IEnumerable<TestDescriptor> Tests => _tests;

        public string Match { get; private set; }

        private readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public TestGenerator(string fixture, IEnumerable<string> tests)
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
$@"using NUnit.Framework;

namespace {Fixture.FixtureName}.Tests
{{
  [TestFixture(Description = ""{Fixture.FixtureName}"")]
  public class {Fixture.FixtureName}Tests
  {{
");

            foreach (var test in _tests)
            {
                sb.AppendLine();
                sb.AppendJoin("\r\n", GenerateTest(test)
                    .Select(x => !string.IsNullOrWhiteSpace(x) ? $"    {x}" : x));
                sb.AppendLine();
            }
            
            sb.Append(
$@"  }}
}}");
            return sb.ToString();
        }

        private static readonly Regex ParametersStructureRegex = new Regex(@"(?<param>\w+):(?<type>\w+(?:<\w+>)?)", RegexOptions.Compiled);
        private static readonly Regex ParametersRegex = new Regex(@$"{{(?<full>{ParametersStructureRegex})}}", RegexOptions.Compiled);

        private IEnumerable<string> GenerateTest(TestDescriptor test)
        {
            var title = test.Title;
            
            var parsedParameters = ParseParameters(test);
            
            foreach(var p in parsedParameters)
            {
                title = title.Replace($"{{{p.Match}}}", p.Name);
            }
            
            var cleanTitle = title;

            var parametersList = string.Join(", ", parsedParameters.Select(x => $"{x.Type} {x.Name}"));

            title = textInfo
               .ToTitleCase(title)
               .Replace(' ', '_')
               .Replace('-', '_');

            return
$@"[Test(Description = ""{test.Description ?? cleanTitle}"")]
public void {title}({parametersList})
{{
  // Arrange

  // Act

  // Assert
  Assert.Fail();
}}".Split("\r\n");

            static TestParameter ParseParameter(string parameterMatch)
            {
                var m = ParametersStructureRegex.Match(parameterMatch);

                var name = m.Groups["param"].Value;
                var type = m.Groups["type"].Value;

                return new()
                {
                    Name = name,
                    Type = type,
                    Match = parameterMatch
                };
            }

            static IEnumerable<TestParameter> ParseParameters(TestDescriptor d)
            {
                var parameterMatches = ParametersRegex.Matches(d.Title);

                var groups = parameterMatches.SelectMany(x => x.Groups["full"].Captures)
                    .ToArray();

                return !groups.Any()
                    ? Enumerable.Empty<TestParameter>()
                    : groups
                        .Select(capture => ParseParameter(capture.Value));
            }
        }
    }
}

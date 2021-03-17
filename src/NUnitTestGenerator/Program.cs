using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NUnitTestGenerator
{
    public class Test
    {
        public string Title { get; set; }
    }

    public interface IGenerateTestFixtures
    {
        public string Fixture { get; }

        string GenerateTests();
    }

    public class NUnitTestGenerator : IGenerateTestFixtures
    {
        public string Fixture { get; init; }
        private List<Test> _tests { get; init; } = new();
        private readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public NUnitTestGenerator(IEnumerable<string> tests)
        {
            _tests.AddRange(tests.Select(x => new Test { Title = x }));
        }

        public string GenerateTests()
        {
            var sb = new StringBuilder();
            sb.Append(@$"
using NUnit.Framework;

namespace {Fixture}.Tests
{{
	public class {Fixture}Tests
	{{
");

            foreach (var test in _tests)
            {
                sb.AppendLine();
                sb.AppendJoin("\r\n", GenerateTest(test).Select(x => $"\t\t{x}"));
                sb.AppendLine();
            }
            sb.Append(@"	}
}");
            return sb.ToString();
        }

    private IEnumerable<string> GenerateTest(Test test)
        {
            var title = textInfo
                .ToTitleCase(test.Title)
                .Replace(' ', '_')
                .Replace('-', '_');

            return
    $@"[Test]
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

[![.NET](https://github.com/EugeneKrapivin/NUnitTestGenerator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EugeneKrapivin/NUnitTestGenerator/actions/workflows/dotnet.yml)

# NUnit Test Generator

A tool to generate NUnit tests fixtures from test titles, essentially generating the boiler plate code

usage:

```csharp
var gen = new TestGenerator(
    "FixtureTests",
    new []
    {
        "generating a {single:string} test should create a valid result",
        "generating {multiple:IEnumerable<string>} tests should create a valid result"
    });

File.WriteAllText("result.cs", gen.GenerateTests());
```

will generate a vaild tests boiler plate file:

```csharp
using NUnit.Framework;

namespace FixtureTests.Tests
{
  [TestFixture(Description = "FixtureTests")]
  public class FixtureTestsTests
  {

    [Test(Description = "generating a single test should create a valid result")]
    public void Generating_A_Single_Test_Should_Create_A_Valid_Result(string single)
    {
      // Arrange

      // Act

      // Assert
      Assert.Fail();
    }

    [Test(Description = "generating multiple tests should create a valid result")]
    public void Generating_Multiple_Tests_Should_Create_A_Valid_Result(IEnumerable<string> multiple)
    {
      // Arrange

      // Act

      // Assert
      Assert.Fail();
    }
  }
}
```

**Note**
* Parameters are supported by wrapping the parameter in curly braces `{PARAM_NAME:PARAM_TYPE}`
  * parameters in test title with be replaced by the parameter name
  * parameters will be added as parameters to the test method
  * types are not validated and used as-is
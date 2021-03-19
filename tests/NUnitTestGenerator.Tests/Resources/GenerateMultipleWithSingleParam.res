using NUnit.Framework;

namespace TestFixture.Tests
{
	[TestFixture(Description = "TestFixture")]
	public class TestFixtureTests
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
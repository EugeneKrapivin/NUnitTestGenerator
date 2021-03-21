using System;
using System.IO;

using NUnitTestGenerator;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();

            var gen = new TestGenerator("FixtureTests",
                new[]
                 {
                    "generating a {single:string} test should create a valid result",
                    "generating {multiple:IEnumerable<string>} tests should create a valid result"
                 });

            File.WriteAllText("result.cs.generated", gen.GenerateTests());
        }
    }
}

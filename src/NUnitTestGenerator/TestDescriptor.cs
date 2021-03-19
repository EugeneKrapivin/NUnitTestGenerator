namespace NUnitTestGenerator
{
    public class TestDescriptor
    {
        public string Title { get; init; }
        public string Description { get; set; }

        public class TestParameter
        {
            public string Name { get; init; }
            public string Type { get; init; }

            public string Match { get; init; }
        }
    }
}

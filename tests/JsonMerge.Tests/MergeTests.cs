using System.IO;
using System.Linq;
using Xunit;

namespace JsonMerge.Tests
{
    public class MergeTests
    {
        [Fact]
        public void Merge_TwoFiles_ReturnsExpectedJson()
        {
            var file1 = Path.Combine("..", "..", "sampleData", "1.json");
            var file2 = Path.Combine("..", "..", "sampleData", "2.json");
            var json1 = File.ReadAllText(file1);
            var json2 = File.ReadAllText(file2);

            var result = JsonMerge.CommandLine.JsonMerger.Merge(new[] { json1, json2 });

            Assert.Contains("\"Name\": \"John\"", result);
            Assert.Contains("\"LastName\": \"Snow\"", result);
            Assert.Contains("\"HairColor\": \"Brown\"", result);
            Assert.Contains("\"Knows\": \"Nothing\"", result);
        }
    }
}

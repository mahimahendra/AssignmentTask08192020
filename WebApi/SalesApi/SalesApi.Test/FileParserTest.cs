using SalesApi.Application;
using SalesApi.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace SalesApi.Test
{
    public class FileParserTest
    {

        private readonly IFileParser fileParser = new FileParser();

        [Fact]
        public async Task ReadsFile()
        {
            using var stream = File.OpenRead("Files/SalesData.txt");
            var result = await fileParser.Read(stream);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task Throws_InvalidDate()
        {
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                using var stream = File.OpenRead("Files/SalesData2.txt");
                var result = await fileParser.Read(stream);
            });


        }

    }
}

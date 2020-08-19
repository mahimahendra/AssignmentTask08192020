using CsvHelper;
using SalesApi.Application;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Infrastructure
{
    public class FileParser : IFileParser
    {
        public const char CommentCharacter = '#';
        public async Task<List<CustomerSalesRecord>> Read(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.AllowComments = true;
            csv.Configuration.Comment = CommentCharacter;
            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.RegisterClassMap<CustomerSalesMap>();

            var list = new List<CustomerSalesRecord>();
            await foreach (var customer in csv.GetRecordsAsync<CustomerSalesRecord>())
            {
                list.Add(customer);
            };

            return list;
        }
    }
}

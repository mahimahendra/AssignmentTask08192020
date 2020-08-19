using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SalesApi.Application;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Infrastructure
{
    public class CustomerSalesMap : ClassMap<CustomerSalesRecord>
    {
        public CustomerSalesMap()
        {
            Map(m => m.CustomerId).Index(0);
            Map(m => m.CustomerType).Index(1);
            Map(m => m.CustomerName).Index(2);
            Map(m => m.TotalSalesAmount).Index(3);
            Map(m => m.Timestamp).Index(4).TypeConverter(new DateConverter());
        }
    }

    public class DateConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var date = DateTimeOffset.ParseExact(text, "[yyyy]-(MM)-[dd]", CultureInfo.InvariantCulture.DateTimeFormat);
            return date;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var date = (DateTimeOffset)value;
            return date.ToString("[yyyy]-(MM)-[dd]");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Application
{
    public class CustomerSalesRecord
    {
        public int CustomerId { get; set; }
        public int CustomerType { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public DateTimeOffset Timestamp { get; set; }

    }

    public enum CustomerType
    {
        Private = 1,
        Company = 2
    }
}

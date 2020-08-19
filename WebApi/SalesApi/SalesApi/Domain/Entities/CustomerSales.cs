using SalesApi.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Domain.Entities
{
    public class CustomerSales
    {

        public int CustomerSalesId { get; set; }

        public int CustomerId { get; set; }
        public int CustomerType { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }
}

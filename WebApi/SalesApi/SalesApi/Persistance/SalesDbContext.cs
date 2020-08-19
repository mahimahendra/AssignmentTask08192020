using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Persistance
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {

        }

        public DbSet<CustomerSales> CustomerSales { get; set; }

    }
}

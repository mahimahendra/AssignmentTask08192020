using SalesApi.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApi.Application
{
    public interface IFileParser
    {
        Task<List<CustomerSalesRecord>> Read(Stream stream);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Application;
using SalesApi.Domain;
using SalesApi.Domain.Entities;
using SalesApi.Persistance;

namespace SalesApi.Controllers
{

    public class FilesController : BaseController
    {
        private readonly IFileParser _fileParser;
        private readonly SalesDbContext _dbContext;

        public FilesController(IFileParser fileParser, SalesDbContext dbContext)
        {
            _fileParser = fileParser;
            this._dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] RequestModel requestModel)
        {
            if (requestModel.formFile == null)
            {
                ModelState.AddModelError("formFile", "Please upload the file.");
                return BadRequest(ModelState);
            }
            var result = await _fileParser.Read(requestModel.formFile.OpenReadStream());

            var entities = result
                .Where(y => y.TotalSalesAmount > requestModel.minimumSalesAmount && y.Timestamp < DateTimeOffset.UtcNow)
                .Select(x => new CustomerSales()
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    CustomerType = x.CustomerType,
                    TotalSalesAmount = x.TotalSalesAmount,
                    Timestamp = x.Timestamp,
                    CreatedOn = DateTimeOffset.UtcNow,
                    UpdatedOn = DateTimeOffset.UtcNow

                }).ToList();


            await _dbContext.AddRangeAsync(entities);
            _dbContext.SaveChanges();



            return Ok();
        }

        [HttpGet]
        public async Task<List<CustomerSales>> Get()
        {
            return await _dbContext.CustomerSales.OrderByDescending(x => x.CustomerType)
                .ThenByDescending(x => x.CustomerName)
                .ToListAsync();

        }
    }

    public class RequestModel
    {
        public IFormFile formFile { get; set; }
        public decimal minimumSalesAmount { get; set; }
    }
}

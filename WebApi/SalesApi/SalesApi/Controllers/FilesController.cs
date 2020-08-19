using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Post(IFormFile formFile)
        {
            if (formFile == null)
            {
                ModelState.AddModelError("formFile", "Please upload the file.");
                return BadRequest(ModelState);
            }
            var result = await _fileParser.Read(formFile.OpenReadStream());

            var entities = result.Select(x => new CustomerSales()
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
    }
}

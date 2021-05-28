using CsvHelper;
using CSVParser.Data;
using CSVParser.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvController : Controller
    {
        private readonly string _connectionString;
        public CsvController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }   
        [HttpGet]
        [Route("generateCsv/{pplAmount}")]
        public IActionResult GenerateCsv(int pplAmount)
        {
            var repo = new PeopleRepository(_connectionString);
            var ppl = repo.GenerateListPeople(pplAmount);
            var csvString = GetStringCsv(ppl);
            var bytes = Encoding.UTF8.GetBytes(csvString);
            return File(bytes, "text/csv", "people.csv");
        }
        private string GetStringCsv(List<Person> ppl)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(ppl);
            return builder.ToString();
        }
        [HttpPost]
        [Route("UploadCsv")]
        public void UploadCsv(CsvUploadViewModel vm)
        {
            int commaIndex = vm.Base64File.IndexOf(',');
            string base64 = vm.Base64File.Substring(commaIndex + 1);
            var bytes = Convert.FromBase64String(base64);
            var ppl = GetFromCsv(bytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(ppl);
        }

        private List<Person> GetFromCsv(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Person>().ToList();

        }

    }
}

using CSVParser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CSVParser.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly string _connectionString;
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("GetPpl")]
        public List<Person> GetPpl()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }
        [HttpPost]
        [Route("DeletePpl")]
        public void DeletePpl()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePeople();
        }
    }
}

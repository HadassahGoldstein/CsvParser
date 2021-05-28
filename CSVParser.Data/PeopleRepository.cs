using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Faker;
namespace CSVParser.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;
        
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPeople(List<Person> ppl)
        {
            using var context = new PeopleDbContext(_connectionString);           
            context.People.AddRange(ppl);
            context.SaveChanges();
        }
        public List<Person> GenerateListPeople(int amount)
        {
            var ppl = new List<Person>();
            for (int i = 1; i <= amount; i++)
            {
                ppl.Add(new Person
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Address = $"{Address.StreetAddress()} {Address.StreetName()} {Address.City()}, {Address.UsState()}",
                    Age = RandomNumber.Next(5, 120),
                    Email = Internet.Email()
                });
            }
            return ppl;
           
        }
        public void DeletePeople()
        {
            using var context = new PeopleDbContext(_connectionString);
            context.RemoveRange(context.People);
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            using var context = new PeopleDbContext(_connectionString);
           return context.People.ToList();
        }
    }
}

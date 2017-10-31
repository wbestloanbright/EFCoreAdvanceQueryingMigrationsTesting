using Contacts.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutoMapper;
using Contacts.BusinessObjects;
using System.Threading.Tasks;
using PersonObject = Contacts.BusinessObjects.Person;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Contacts.UnitTest
{

    [TestClass]
    public class PersonControllerTests : TestCore
    {
        private List<int> _toDelete = new List<int>();

        [TestCleanup]
        public void CleanupTest()
        {
            Init();
            _toDelete.ForEach(one =>
            {
                _context.Persons.Remove(_context.Persons.Find(one));
            });
            _context.SaveChanges();
            _toDelete.Clear();
        }

        [TestMethod]
        public async Task Should_Insert_Person()
        {
            var mappingProfile = new AutoMapperProfile();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperProfile).Assembly);
            });

            var contoller = new PersonsController(_context, Mapper.Instance);

            var person = new PersonObject
            {
                BirthDate = DateTime.Today,
                FirstName = "John",
                LastName = Guid.NewGuid().ToString(),
                IsActive = true,
                Height = 6
            };

            var result = (CreatedAtRouteResult)await contoller.Post(person);
            var output = (PersonObject)result.Value;
            Init();
            _toDelete.Add(output.PersonId);

            var data = await _context.Persons.FindAsync(output.PersonId);
            Assert.IsNotNull(data, "Should have created a person");
            Assert.AreEqual(person.LastName, output.LastName, "Should have corerect name");

        }


        [TestMethod]
        public async Task Should_Insert_Person_In_Memory()
        {
            var mappingProfile = new AutoMapperProfile();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperProfile).Assembly);
            });

            var contoller = new PersonsController(CreateInMemoryContext(), Mapper.Instance);

            var person = new PersonObject
            {
                BirthDate = DateTime.Today,
                FirstName = "John",
                LastName = Guid.NewGuid().ToString(),
                IsActive = true,
                Height = 6
            };

            var result = (CreatedAtRouteResult)await contoller.Post(person);
            var output = (PersonObject)result.Value;

            var data = await CreateInMemoryContext().Persons.FindAsync(output.PersonId);
            Assert.IsNotNull(data, "Should have created a person");
            Assert.AreEqual(person.LastName, output.LastName, "Should have corerect name");

        }

    }
}

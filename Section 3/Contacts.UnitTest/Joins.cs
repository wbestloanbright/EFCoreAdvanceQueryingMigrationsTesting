using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class Joins : TestCore
    {
        [TestMethod]
        public void InnerJoinsMethodSyntax()
        {

            var people = _context.Persons
            .Join(
                _context.PersonTypes,
                person => person.PersonTypeId,
                personType => personType.PersonTypeId,
                (person, type) => new
                {
                    Person = person,
                    PersonType = type
                })
            .Select(p => new
            {
                p.Person.LastName,
                p.Person.FirstName,
                p.PersonType.PersonTypeName
            });


            foreach (var item in people)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }


        }

        [TestMethod]
        public void InnerJoinsQuerySyntax()
        {
            var people = from person in _context.Persons
                         join personType in _context.PersonTypes
                            on person.PersonTypeId equals personType.PersonTypeId
                         select new
                         {
                             person.LastName,
                             person.FirstName,
                             personType.PersonTypeName
                         };
            foreach (var item in people)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }
    }
}

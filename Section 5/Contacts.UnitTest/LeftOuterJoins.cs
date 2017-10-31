using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class LeftOuterJoins : TestCore
    {
        [TestMethod]
        public void LeftOuterJoinsMethodSyntax()
        {

            var people = _context.Persons
                .GroupJoin(
                    _context.PersonTypes,
                    person => person.PersonTypeId,
                    personType => personType.PersonTypeId,
                    (person, type) => new
                    {
                        Person = person,
                        PersonType = type
                    })
                .SelectMany(groupedData =>
                    groupedData.PersonType.DefaultIfEmpty(),
                    (group, personType) => new
                    {
                        group.Person.LastName,
                        group.Person.FirstName,
                        TypeName = personType.PersonTypeName ?? "Unknown"
                    });


            foreach (var item in people)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }


        }

        [TestMethod]
        public void LeftOuterJoinsQuerySyntax()
        {

            var people = from person in _context.Persons
                         join personType in _context.PersonTypes
                            on person.PersonTypeId equals personType.PersonTypeId into finalGroup
                         from groupedData in finalGroup.DefaultIfEmpty()
                         select new
                         {
                             person.LastName,
                             person.FirstName,
                             TypeName = groupedData.PersonTypeName ?? "Unknown"
                         };
            foreach (var item in people)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }
    }
}

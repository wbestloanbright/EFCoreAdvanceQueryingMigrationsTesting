using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class WindowingFunctions : TestCore
    {
        [TestMethod]
        public void PagingMethodSyntax()
        {
            var pageSize = 2;
            var pageNumber = 1;

            var query = 
                _context.Persons.OrderBy(p => p.LastName).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(p => p);
            Trace.WriteLine($"Processing {pageNumber}");
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

            pageNumber = 2;
            Trace.WriteLine($"Processing {pageNumber}");
            query = _context.Persons.OrderBy(p => p.LastName).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(p => p);

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void PagingQuerySyntax()
        {
            var pageSize = 2;
            var pageNumber = 1;
            Trace.WriteLine($"Processing {pageNumber}");
            var query = from person in _context.Persons
                        orderby person.LastName
                        select new PersonInfo
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            PersonId = person.PersonId
                        };

            foreach (var item in query.Skip((pageNumber - 1) * pageSize).Take(pageSize))
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void PagingIncludeTotal()
        {
            var pageSize = 2;
            var pageNumber = 1;
            var query = from person in _context.Persons
                        orderby person.LastName
                        select new PersonInfo
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            PersonId = person.PersonId
                        };
            var total = query.Count();
            Trace.WriteLine($"Total {total}");
            foreach (var item in query.Skip((pageNumber - 1) * pageSize).Take(pageSize))
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }

        [TestMethod]
        public void PagingIncludeTotalInData()
        {
            var pageSize = 2;
            var pageNumber = 1;
            var query = from person in _context.Persons
                        orderby person.LastName
                        select new
                        {
                            Person = new PersonInfo
                            {
                                FirstName = person.FirstName,
                                LastName = person.LastName,
                                PersonId = person.PersonId
                            },
                            Total = _context.Persons.Count()
                        };
            foreach (var item in query.Skip((pageNumber - 1) * pageSize).Take(pageSize))
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }

        }
    }
}

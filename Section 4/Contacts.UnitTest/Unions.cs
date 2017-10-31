using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace Contacts.UnitTest
{
    [TestClass]
    public class Unions : TestCore
    {
        [TestMethod]
        public void UnionsMehtodSyntax()
        {
            var query = _context.Persons
                .Select(p => new
                {
                    Name = p.LastName + " " + p.FirstName,
                    RowType = "Person"
                })
                .Union(_context.Companies.Select(c => new
                {
                    Name = c.CompanyName,
                    RowType = "Company"
                }))
                .OrderBy(result => result.RowType)
                .ThenBy(result => result.Name);


            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }
    }
}

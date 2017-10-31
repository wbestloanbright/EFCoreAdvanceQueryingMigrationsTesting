using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Contacts.UnitTest
{
    [TestClass]
    public class RepositoryTests : TestCore
    {
        [TestMethod]
        public async Task RunRepository()
        {
            var repo = new Respoitory(GetContactsContext());
            int personId;
            var person = new Person
            {
                BirthDate = DateTime.Today,
                FirstName = "Repo",
                LastName = "Person",
                IsActive = true,
                Height = 6
            };
            var result = await repo.Insert(person);
            personId = result.PersonId;
            using (var ctx = GetContactsContext())
            {
                var p = ctx.Persons.FirstOrDefault(a => a.PersonId == result.PersonId);
                Trace.WriteLine(JsonConvert.SerializeObject(p));
            }

            person.LastName = "Updated Person";
            repo = new Respoitory(GetContactsContext());
            result = await repo.Update(person);
            using (var ctx = GetContactsContext())
            {
                var p = ctx.Persons.FirstOrDefault(a => a.PersonId == result.PersonId);
                Trace.WriteLine(JsonConvert.SerializeObject(p));
            }

            repo = new Respoitory(GetContactsContext());
            var deleted = await repo.Delete<Person>(personId, p => p.PersonId);
            using (var ctx = GetContactsContext())
            {
                var p = ctx.Persons.FirstOrDefault(a => a.PersonId == result.PersonId);
                Trace.WriteLine(JsonConvert.SerializeObject(p));
            }

            using (repo = new Respoitory(GetContactsContext()))
            {
                foreach(var data in repo.Get<Person>(p => p.PersonId > 0))
                {
                    Trace.WriteLine(JsonConvert.SerializeObject(data));
                }
            }
            
        }

        public ContactsContext GetContactsContext()
        {
            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer("Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ContactsContext(builder.Options);
        }
    }
}

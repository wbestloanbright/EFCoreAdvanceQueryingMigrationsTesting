using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;

namespace Contacts.UnitTest
{
    [TestClass]
    public class TransactionTests : TestCore
    {
        [TestMethod]
        public void MultipleSaves()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer("Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            using (var context = new ContactsContext(builder.Options))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var person = new Person
                        {
                            BirthDate = DateTime.Today,
                            FirstName = "John",
                            LastName = "Thomas",
                            IsActive = true,
                            Height = 6
                        };

                        context.Persons.Add(person);
                        context.SaveChanges();

                        person = new Person
                        {
                            BirthDate = DateTime.Today,
                            FirstName = "Jane",
                            LastName = "Thomason",
                            IsActive = true,
                            Height = 5
                        };

                        context.Persons.Add(person);
                        context.SaveChanges();

                        var thomases = context.Persons.Where(p => p.LastName == "Thomas")
                            .OrderBy(b => b.FirstName)
                            .ToList();
                        Trace.WriteLine(JsonConvert.SerializeObject(thomases, settings));

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Transaction is not committed. Handle errors
                        Trace.WriteLine(JsonConvert.SerializeObject(ex, settings));
                    }
                }
            }
        }

        [TestMethod]
        public void MultipleContexts()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseSqlServer("Server=.;Database=ContactsDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            using (var context = new ContactsContext(builder.Options))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var person = new Person
                        {
                            BirthDate = DateTime.Today,
                            FirstName = "John",
                            LastName = "Jameson",
                            IsActive = true,
                            Height = 6
                        };

                        context.Persons.Add(person);
                        context.SaveChanges();

                        using (var secondContext = new Contacts2Context(context.Database.GetDbConnection()))
                        {
                            secondContext.Database.UseTransaction(transaction.GetDbTransaction());
                            person = new Person
                            {
                                BirthDate = DateTime.Today,
                                FirstName = "Jane",
                                LastName = "Johnson",
                                IsActive = true,
                                Height = 5
                            };

                            secondContext.Persons.Add(person);
                            secondContext.SaveChanges();
                        }
                        

                        var johnsons = context.Persons.Where(p => p.LastName == "Thomas")
                            .OrderBy(b => b.FirstName)
                            .ToList();
                        Trace.WriteLine(JsonConvert.SerializeObject(johnsons, settings));
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Transaction is not committed. Handle errors
                        Trace.WriteLine(JsonConvert.SerializeObject(ex, settings));
                    }
                }
            }
        }
    }
}

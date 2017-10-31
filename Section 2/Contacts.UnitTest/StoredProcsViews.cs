using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Contacts.UnitTest
{
    [TestClass]
    public class StoredProcsViews : TestCore
    {
        [TestMethod]
        public void View()
        {
            var query = _context.PersonView.FromSql("Select * from Contacts.PersonView");
            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        [TestMethod]
        public void StoredProcedure()
        {
           
            var query = _context.PersonView.FromSql("exec Contacts.PersonProcedure {0}", "a");

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }

        [TestMethod]
        public void RawCommands()
        {
            var query = new List<PersonViewInfo>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                command.Connection.Open();
                command.CommandText = "Select * from Contacts.PersonView";
                using (var reader = command.ExecuteReader())
                {
                    var firstname = reader.GetOrdinal("FirstName");
                    var lastName = reader.GetOrdinal("LastName");
                    var id = reader.GetOrdinal("PersonId");
                    var typeName = reader.GetOrdinal("PersonTypeName");
                    while (reader.Read())
                    {
                        query.Add(new PersonViewInfo
                        {
                            FirstName = reader.GetString(firstname),
                            LastName = reader.GetString(lastName),
                            PersonId = reader.GetInt32(id),
                            PersonTypeName = reader.GetString(typeName),
                        });
                    }
                    
                }
            }
            

            foreach (var item in query)
            {
                Trace.WriteLine(JsonConvert.SerializeObject(item));
            }
        }
    }
}

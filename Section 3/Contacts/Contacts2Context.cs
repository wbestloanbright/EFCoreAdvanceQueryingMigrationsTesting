using Microsoft.EntityFrameworkCore;

namespace Contacts
{
    public class Contacts2Context : DbContext
    {
        public Contacts2Context(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
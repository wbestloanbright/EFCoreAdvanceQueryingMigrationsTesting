using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contacts
{
    public class Contacts2Context : DbContext
    {
        private readonly DbConnection connection;

        public Contacts2Context(DbContextOptions<Contacts2Context> options) : base(options)
        {

        }

        public Contacts2Context(DbConnection connection)
        {
            this.connection = connection;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration()
            modelBuilder.Entity<Person>().ToTable("People", "Contacts").HasKey(a => a.PersonId);
            modelBuilder.Entity<CompanyPerson>()
                .HasKey(e => new { e.PersonId, e.CompanyId });
            modelBuilder.Entity<PersonResume>()
               .HasKey(e => e.PersonId);
        }
    }
}
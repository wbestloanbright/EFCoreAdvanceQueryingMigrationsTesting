using Contacts.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts
{

    public class Person
    {

        public int PersonId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(40, ErrorMessage = "First name cannot be longer than 40")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50")]
        [NamesValidation("Jones")]
        public string LastName { get; set; }
        public decimal Height { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<PersonPhone> PersonPnones { get; set; } = new HashSet<PersonPhone>();

        public int? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; }

        public virtual ICollection<CompanyPerson> CompanyPersons { get; set; } = new HashSet<CompanyPerson>();

        public virtual PersonResume PersonResume { get; set; }
    }

}

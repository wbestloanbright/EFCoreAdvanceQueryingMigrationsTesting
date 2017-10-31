using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contacts.BusinessObjects
{

    public class Person
    {

        public int PersonId { get; set; }
        public int? PersonTypeId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(40, ErrorMessage = "First name cannot be longer than 40")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50")]
        public string LastName { get; set; }

        public decimal Height { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? BirthDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<PersonPhone> PersonPnones { get; set; } = new HashSet<PersonPhone>();


    }

}

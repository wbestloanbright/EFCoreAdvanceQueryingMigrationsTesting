using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts
{
    public class PersonPhone
    {
        public int PersonPhoneId { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(30, ErrorMessage = "Phone cannot be longer than 30")]
        public string PhoneNumber { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Contacts.BusinessObjects
{
    public class PersonPhone
    {
        public int PersonPhoneId { get; set; }
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Size of {0} must be between {2} and {1}")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Put a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Note { get; set; }

        //public Contact()
        //{

        //}

        //public Contact(int id, string name, string email, string phone, string note)
        //{
        //    Id = id;
        //    Name = name;
        //    Email = email;
        //    Phone = phone;
        //    Note = note;
        //}
    }
}

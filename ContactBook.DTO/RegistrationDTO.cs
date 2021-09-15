using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class RegistrationDTO
    {
        public string PhotoUrl { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

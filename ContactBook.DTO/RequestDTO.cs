using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class RequestDTO
    {
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
        
    }
}

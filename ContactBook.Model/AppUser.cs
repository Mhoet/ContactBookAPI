using Microsoft.AspNetCore.Identity;
using System;

namespace ContactBook.Model
{
    public class AppUser : IdentityUser
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  PhotoUrl { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.Now;
    }
}

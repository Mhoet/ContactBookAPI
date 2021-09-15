using ContactBook.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactBook.Data
{
    public class ContactBookDBContext : IdentityDbContext<AppUser>
    {
        public ContactBookDBContext(DbContextOptions<ContactBookDBContext> options) : base(options)
        {

        }
    }        
}

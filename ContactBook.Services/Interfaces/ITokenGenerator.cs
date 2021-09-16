using ContactBook.DTO;
using ContactBook.Model;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public interface ITokenGenerator
    {
        Task<string> GenerateAuthenticationToken(AppUser user);
    }
}

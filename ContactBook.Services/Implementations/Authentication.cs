using ContactBook.DTO;
using ContactBook.DTO.Mappings;
using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<AppUser> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseDTO> Login(RequestDTO requester)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(requester.Email);
            if (appUser != null)
            {
                if (await _userManager.CheckPasswordAsync(appUser, requester.Password))
                {
                    var response = AppUserMapping.GetResponse(appUser);
                    response.Token = await _tokenGenerator.GenerateAuthenticationToken(appUser);
                    return response;
                }
                throw new AccessViolationException("Invalid Credentials");
            }
            throw new AccessViolationException("Invalid Credentials");
        }
    }
}

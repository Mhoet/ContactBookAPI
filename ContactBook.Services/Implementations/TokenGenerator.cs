using ContactBook.DTO;
using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public class TokenGenerator : ITokenGenerator
    {

        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;
        private object authClaims;

        public TokenGenerator(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        } 
        public async Task<ResponseDTO> GenerateAuthenticationToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return new ResponseDTO
            {
                Token = tokenAsString,
                TokenValidUntil = token.ValidTo
            };
        }
    }    
}

using ContactBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.DTO.Mappings
{
    public class AppUserMapping
    {
        public static ResponseDTO GetResponse(AppUser user)
        {
            return new ResponseDTO
            {
                Id = user.Id,
                PhotoUrl = user.PhotoUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                DateCreated = user.DateCreated,
                LastModified = user.LastModified
            };
        }

        public static AppUser GetAppUser(RegistrationDTO newUser)
        {
            return new AppUser
            {

                PhotoUrl = newUser.PhotoUrl,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                UserName = newUser.UserName
            };
        }
    }
}

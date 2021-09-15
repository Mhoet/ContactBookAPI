using ContactBook.Common;
using ContactBook.DTO;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public class AppUserServices : IAppUserServices
    {
        public Task<ResponseDTO> AddUser(RegistrationDTO registration)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAppUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public Paging<ResponseDTO> GetUsers(PagingDTO paging)
        {
            throw new NotImplementedException();
        }

        public Paging<ResponseDTO> SearchUsers(string searchQuery, PagingDTO paging)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAppUser(string userId, UpdateDTO userUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePhoto(string userId, PhotoDTO photo)
        {
            throw new NotImplementedException();
        }
    }
}

using ContactBook.Common;
using ContactBook.DTO;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public interface IAppUserServices
    {
        Task<ResponseDTO> GetUserById(string userId); 
        Task<ResponseDTO> GetUserByEmail(string email);
        Task<ResponseDTO> AddUser(RegistrationDTO registration);
        Paging<ResponseDTO> GetUsers(PagingDTO paging);
        Paging<ResponseDTO> SearchUsers(string searchQuery, PagingDTO paging);
        Task<bool> DeleteAppUser(string userId);
        Task<bool> UpdateAppUser(string userId, UpdateDTO userUpdateDTO);
        Task<bool> UpdatePhoto(string userId, PhotoDTO photo);
    }
}

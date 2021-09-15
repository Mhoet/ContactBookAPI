using ContactBook.DTO;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services
{
    public interface IAuthentication
    {
        Task<ResponseDTO> Login(RequestDTO request);
    }
}

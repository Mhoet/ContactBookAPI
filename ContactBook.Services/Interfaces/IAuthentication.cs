using ContactBook.DTO;
using System;
using System.Threading.Tasks;

namespace ContactBook.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<ResponseDTO> Login(RequestDTO request);
    }
}

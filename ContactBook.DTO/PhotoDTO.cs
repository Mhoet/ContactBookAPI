using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class PhotoDTO
    {
        public IFormFile PhotoUrl { get; set; }
       
    }
}

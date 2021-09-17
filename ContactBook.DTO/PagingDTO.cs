using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class PagingDTO
    {
        public int PageNumber { get; set; }
        public int NumberOfUsers { get; set; }     

    }
}

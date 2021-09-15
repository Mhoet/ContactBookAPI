using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class PagingDTO
    {
        const int numberOfUsersPerPage = 12;
        public int PageNumber { get; set; } = 1;

        private int usersPerPage = 6;
        public int UsersPerPage
        {
            get => UsersPerPage;
            set => UsersPerPage = (value > numberOfUsersPerPage) ? numberOfUsersPerPage : value;
        }

       
    }
}

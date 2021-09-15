using System;

namespace ContactBook.DTO
{
    public class ResponseDTO
    {
        public string Id { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTimeOffset? TokenValidUntil { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModified { get; set; }

    }
}

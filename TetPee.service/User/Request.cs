using System;
using Microsoft.AspNetCore.Http;

namespace TetPee.service.User;

public class Request
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public IFormFile? Avatar { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Password { get; set; }
    }
    public class UpdateUserRequest: CreateUserRequest
    {
        public Guid Id { get; set; }
    }

}
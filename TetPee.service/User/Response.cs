using System;

namespace TetPee.service.User;

public class Response
{
    public class GetUsersResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? Address { get; set; }    
        
        public string Role { get; set; } = "User";
        // public string? DateOfBirth { get; set; } = null;
    }
    
    public class GetAllUserResponse
    {
        public  Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; } = null;
        public String Role { get; set; } = "User";
        // public string? DateOfBirth { get; set; } = null;
    }
    
}
namespace TetPee.service.Identity;

public class Request
{
    public class  CreateUserRequest
    {
        public required string  Email { get; set; }
        public required  string HashedPassword { get; set; }
        public required  string FirstName { get; set; }
        public required  string LastName { get; set; }
    }
}
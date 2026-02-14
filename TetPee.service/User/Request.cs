namespace TetPee.service.User;

public class Request
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Password { get; set; }
    }
    public class UpdateUserRequest: CreateUserRequest
    {
        public Guid Id { get; set; }
    }
    
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }
    }
    
    public class UpdateCategoryRequest: CreateCategoryRequest
    {
        public Guid Id { get; set; }
    }
}
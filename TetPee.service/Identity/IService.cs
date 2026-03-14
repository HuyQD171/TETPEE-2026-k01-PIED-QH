namespace TetPee.service.Identity;

public interface IService
{
    public Task<Response.IdentityResponse> Login(string Email, string Password);

}
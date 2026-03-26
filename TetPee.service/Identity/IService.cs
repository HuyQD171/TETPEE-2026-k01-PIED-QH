using System.Threading.Tasks;

namespace TetPee.service.Identity;

public interface IService
{
    public Task<Response.IdentityResponse> Login(string email, string password);

}
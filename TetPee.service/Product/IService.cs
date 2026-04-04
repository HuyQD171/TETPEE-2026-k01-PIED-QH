using System.Threading.Tasks;

namespace TetPee.service.Product;

public interface IService
{
    public Task<string> CreateProduct(Request.CreateProductRequest request);
}
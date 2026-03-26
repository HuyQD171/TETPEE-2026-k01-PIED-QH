using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TetPee.service.Categogy;

public interface IService
{
    public Task<List<Response.GetCategoriesResponse>> GetCategories();

    public Task<List<Response.GetCategoriesResponse>> GetAllChildrenCategory(Guid id);

}
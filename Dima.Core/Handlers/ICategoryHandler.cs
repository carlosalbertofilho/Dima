using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<Category>> GetAllAsync(GetAllCategoriesRequest request);
}
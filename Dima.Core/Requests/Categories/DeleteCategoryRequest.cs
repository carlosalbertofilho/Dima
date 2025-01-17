using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest(long id) : GetCategoryByIdRequest(id)
{
    public DeleteCategoryRequest(long id, string userId) : this(id)
    {
        UserId = userId;
    }
}
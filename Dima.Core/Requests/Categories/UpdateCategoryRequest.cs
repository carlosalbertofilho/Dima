namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest(long id) : CreateCategoryRequest
{
    public long Id { get; set; } = id;
}
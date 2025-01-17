using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest(long id) : CreateCategoryRequest
{
    [Required(ErrorMessage = "Id is required!")]
    [Range(1, long.MaxValue, ErrorMessage = "Id is not valid, negative or zero!")]
    public long Id { get; set; } = id;
}
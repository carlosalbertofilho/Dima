using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : CreateCategoryRequest
{
    [Required(ErrorMessage = "Id is required!")]
    [Range(1, long.MaxValue, ErrorMessage = "Id is not valid, negative or zero!")]
    public long Id { get; set; } = 0;
    
}
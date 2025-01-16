using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class GetCategoryByIdRequest(long id) : Request
{
    [Required]
    [Range(1, long.MaxValue)]
    [Display(Name = "Category Id")]
    public long Id { get; set; } = id;
}
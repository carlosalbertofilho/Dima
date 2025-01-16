using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(80, ErrorMessage = "Title must be less than 80 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Display(Name = "Description")]
    [MaxLength(250, ErrorMessage = "Description must be less than 250 characters")]
    public string? Description { get; set; } = null;
}
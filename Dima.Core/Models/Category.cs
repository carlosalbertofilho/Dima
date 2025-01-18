using Dima.Core.Requests.Categories;
// ReSharper disable MemberCanBePrivate.Global

namespace Dima.Core.Models;

public class Category
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Description { get; set; } = null;

    private Category()
    {
        
    }

    public static Category Create
        (long id, string title, string userId, string? description = null)
        => new()
        {
            Id = id,
            Title = title,
            UserId = userId,
            Description = description,
        };

    public static Category Create(CreateCategoryRequest request) 
        => Create(0, request.Title, request.UserId, request.Description);

    public void Update
        (string title, string userId,  string? description = null)
    {
        Title = title;
        UserId = userId;
        Description = description;
    }

    public void Update(UpdateCategoryRequest request)
        => Update(request.Title, request.UserId, request.Description);
    
}
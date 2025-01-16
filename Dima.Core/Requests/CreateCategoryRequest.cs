namespace Dima.Core.Requests;

public class CreateCategoryRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = null;
}
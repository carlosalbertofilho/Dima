using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest(long id) : GetCategoryByIdRequest(id)
{  }
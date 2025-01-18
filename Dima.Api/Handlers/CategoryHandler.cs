using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = Category.Create(request);
        
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return new CreatedResponse<Category?>(category,  "Category created");
        }
        catch
        {
            return new InternalServerErrorResponse<Category?>("Category not created");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) return new NotFoundResponse<Category?>("Category not found");
        
            category.Update(request);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category);
        }
        catch 
        {
            return new InternalServerErrorResponse<Category?>("Category not updated");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) return new NotFoundResponse<Category?>("Category not found");
        
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        
            return new NoContentResponse<Category?>("Category deleted", category);
        }
        catch
        {
            return new InternalServerErrorResponse<Category?>("Category not deleted");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
                .Categories.AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id && x.UserId == request.UserId);
            return category is null
                ? new NotFoundResponse<Category?>("Category not found")
                : new Response<Category?>(category, Message: "Category found");
        }
        catch
        {
            return new InternalServerErrorResponse<Category?>("Error on get category by id");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query =  context.Categories
                .AsNoTracking().Where(x => x.UserId == request.UserId)
                .OrderBy(c => c.Title);
            
            var categoriesCount = await query.CountAsync();
            var categories = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            
            return new PagedResponse<System.Collections.Generic.List<Category>>
                (categoriesCount, request.PageSize, request.PageNumber, categories);
        }
        catch (Exception e)
        {
            return new PagedResponse<System.Collections.Generic.List<Category>>
                (0,Data: [], Code: 500, Message: e.Message);
        }
    }
}
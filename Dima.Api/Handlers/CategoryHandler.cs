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
            return new Response<Category?>(category, 201, "Category created");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await QueryCategoriesByUserId(request.UserId)
                .FirstOrDefaultAsync(x => x!.Id == request.Id);
            if (category is null) 
                return new Response<Category?>(null, 404, "Category not found");
        
            category.Update(request);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category, Message: "Category updated");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await QueryCategoriesByUserId(request.UserId)
                    .FirstOrDefaultAsync(x => x!.Id == request.Id);
            if (category is null) return new Response<Category?>
                    (null, 404, "Category not found");
        
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category, Message: "Category deleted");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await QueryCategoriesByUserId(request.UserId)
                .FirstOrDefaultAsync(c => c!.Id == request.Id);
            return category is null
                ? new Response<Category?>(null, 404, "Category not found")
                : new Response<Category?>(category, Message: "Category found");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            
            var categoriesCount = await QueryCategoriesByUserId(request.UserId).CountAsync();
            var categories = await QueryCategoriesByUserId(request.UserId)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            
            return new PagedResponse<List<Category>>
                (categoriesCount, request.PageSize, request.PageNumber, categories!);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Category>>
                (0,Data: [], Code: 500, Message: e.Message);
        }
    }
    
    private IQueryable<Category?> QueryCategoriesByUserId(string userId)
        => context.Categories
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Title);
}
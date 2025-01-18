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
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) 
                return new Response<Category?>(null, 404, "Category not found");
        
            category.Update(request);
            await context.SaveChangesAsync();
        
            return new Response<Category?>(category);
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
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) 
                return new Response<Category?>(null, 404, "Category not found");
        
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
            var category = await context
                .Categories.AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id && x.UserId == request.UserId);
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
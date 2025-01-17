using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = Category.Create(request);
        
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) 
                return new Response<Category>(null, 404, "Category not found");
        
            category.Update(request);
            await context.SaveChangesAsync();
        
            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category is null) 
                return new Response<Category>(null, 404, "Category not found");
        
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        
            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            return category is null
                ? new Response<Category>(null,
                    404,
                    "Category not found")
                : new Response<Category>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PagedResponse<Category>> GetAllAsync(GetAllCategoriesRequest request)
    {
        throw new NotImplementedException();
    }
}
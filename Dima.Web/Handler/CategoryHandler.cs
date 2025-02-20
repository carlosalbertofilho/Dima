using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Web.Handler;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    private const string BaseUrl = "v1/categories";
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _client.PostAsJsonAsync(BaseUrl, request);
        return await result.Content.ReadFromJsonAsync<Response<Category?>>()
            ?? new Response<Category?>(null, 400, "Erro ao criar categoria");
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var result = await _client.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request);
            return result.IsSuccessStatusCode 
                   ? new Response<Category?>(null, 204, "Categoria atualizada com sucesso")
                   : new Response<Category?>(null, 400, "Erro ao atualizar categoria");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _client.DeleteAsync($"{BaseUrl}/{request.Id}");
        return result.IsSuccessStatusCode 
            ? new Response<Category?>(null, 204, "Categoria deletada com sucesso")
            : new Response<Category?>(null, 400, "Erro ao deletar categoria");
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request) 
        => await _client.GetFromJsonAsync<Response<Category?>>($"{BaseUrl}/{request.Id}") 
           ?? new Response<Category?>(null, 400, "Erro ao buscar categoria");

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Category>>>(BaseUrl)
            ?? new PagedResponse<List<Category>>(0, Data: [], Code: 400, Message: "Erro ao buscar categorias");
}
using System.Net.Http.Json;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Web.Handler;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    private const string BaseUrl = "v1/transactions";
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _client.PostAsJsonAsync(BaseUrl, request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
               ?? new Response<Transaction?>(null, 400, "Erro ao criar transação");
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _client.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
               ?? new Response<Transaction?>(null, 400, "Erro ao atualizar transação");
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _client.DeleteAsync($"{BaseUrl}/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
               ?? new Response<Transaction?>(null, 400, "Erro ao deletar transação");
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        => await _client.GetFromJsonAsync<Response<Transaction?>>($"{BaseUrl}/{request.Id}")
           ?? new Response<Transaction?>(null, 400, "Erro ao buscar transação");

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        const string format = "yyyy-MM-dd";
        var startDate = request.StartDate is not null
            ? request.StartDate.Value.ToString(format)
            : DateTime.Now.GetFirstDayOfMonth().ToString(format);
        var endDate = request.EndDate is not null
            ? request.EndDate.Value.ToString(format)
            : DateTime.Now.GetLastDayOfMonth().ToString(format);

        var url = $"{BaseUrl}?startDate={startDate}&endDate={endDate}&pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        return await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url)
               ??
               new PagedResponse<List<Transaction>?>(0,
                   Data: [],
                   Code: 400,
                   Message: "Não foi possível obter as transações");
    }
}
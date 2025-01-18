using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
   
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        if ( request is { Type: ETransactionType.Withdraw, Amount: >= 0}) request.Amount *= -1;
        try
        {
            var category = await GetCategoryByIdAsync(request.CategoryId);
            var transaction = Transaction.Create(request, category);
            
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 201, "Transaction created");
        }
        catch (ArgumentException ex)
        {
            return new Response<Transaction?>(null, 404, ex.Message);
        }
        catch (Exception ex)
        {
            return new Response<Transaction?>(null, 500, "Transaction not created");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        if ( request is { Type: ETransactionType.Withdraw, Amount: >= 0}) request.Amount *= -1;
        try
        {
            var category = await GetCategoryByIdAsync(request.CategoryId);
            var transaction = await QueryTransactionsByUserId(request.UserId)
                .FirstOrDefaultAsync(t => t.Id == request.Id)
                              ?? throw new ArgumentException("Transactions not found");;
            
            transaction!.Update(request, category);
            
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 201, "Transaction created");
        }
        catch (ArgumentException ex)
        {
            return new Response<Transaction?>(null, 404, ex.Message);
        }
        catch (Exception ex)
        {
            return new Response<Transaction?>(null, 500, "Transaction not created");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await QueryTransactionsByUserId(request.UserId)
                                  .FirstOrDefaultAsync(t => t.Id == request.Id)
                              ?? throw new ArgumentException("Transactions not found");;
            
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 204, "Transaction deleted");
        }
        catch (ArgumentException ex)
        {
            return new Response<Transaction?>(null, 404, ex.Message);
        }
        catch (Exception ex)
        {
            return new Response<Transaction?>(null, 500, "Transaction not created");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await QueryTransactionsByUserId(request.UserId)
                                  .FirstOrDefaultAsync(t => t.Id == request.Id)
                              ?? throw new ArgumentException("Transactions not found");;
            
            return new Response<Transaction?>(transaction, 204, "Transaction found!");
        }
        catch (ArgumentException ex)
        {
            return new Response<Transaction?>(null, 404, ex.Message);
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Transaction not found");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            request.EndDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
        catch 
        {
            return new PagedResponse<List<Transaction>?>(0, Data: null, Code: 500, Message: "Invalid date");
        }

        try
        {
            var query = QueryTransactionsByUserId(request.UserId)
                .Where(t => t.PaidOrReceivedAt >= request.StartDate && t.PaidOrReceivedAt <= request.EndDate);
            
            var transactionsCount = await query.CountAsync();
            var transactions = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            
            return new PagedResponse<List<Transaction>?>(transactionsCount, request.PageSize, request.PageNumber, transactions);


        }
        catch
        {
            return new PagedResponse<List<Transaction>?>(0, Data: null, Code: 500, Message: "Transaction not found");
        }
    }

    private IQueryable<Transaction?> QueryTransactionsByUserId(string userId)
        => context.Transactions
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.PaidOrReceivedAt);
    private async Task<Category> GetCategoryByIdAsync(long id)
        => await context.Categories.FirstOrDefaultAsync(x => x.Id == id) ??
           throw new ArgumentException("Category not found");
}
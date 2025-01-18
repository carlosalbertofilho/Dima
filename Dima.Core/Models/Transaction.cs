using Dima.Core.Enums;
using Dima.Core.Requests.Transactions;
// ReSharper disable MemberCanBePrivate.Global

namespace Dima.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    
    public decimal Amount { get; set; }
    
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }
    
    
    private Transaction()
    {
    }

    public static Transaction Create
        ( string title
        , string userId
        , ETransactionType type
        , decimal amount
        , DateTime? paidOrReceivedAt
        , long? categoryId
        , Category? category)
        => new()
        {
            Id = 0,
            Title = title,
            UserId = userId,
            Type = type,
            Amount = amount,
            CategoryId = categoryId,
            Category = category,
            PaidOrReceivedAt = paidOrReceivedAt,
            CreatedAt = DateTime.Now,
        };

    public static Transaction Create
        (CreateTransactionRequest request, Category category)
    => Create( request.Title
             , request.UserId
             , request.Type
             , request.Amount
             , request.PaidOrReceiveAt
             , request.CategoryId
             , category);

    public void Update
    (string title
        , string userId
        , ETransactionType type
        , decimal amount
        , DateTime? paidOrReceivedAt
        , long? categoryId
        , Category? category)
    {
        Title = title;
        UserId = userId;
        Type = type;
        Amount = amount;
        CategoryId = categoryId;
        Category = category;
        PaidOrReceivedAt = paidOrReceivedAt;
    }

    public void Update(UpdateTransactionRequest request, Category category)
        => Update( request.Title
                 , request.UserId
                 , request.Type
                 , request.Amount
                 , request.PaidOrReceiveAt
                 , request.CategoryId
                 , category);
}
using System.ComponentModel.DataAnnotations;
using Dima.Core.Models;

namespace Dima.Core.Requests.Transactions;

public class UpdateTransactionRequest : CreateTransactionRequest
{
    [Required(ErrorMessage = "Id is required!")]
    [Range(1, long.MaxValue, ErrorMessage = "Id is not valid, negative or zero!")]
    public long Id { get; set; }

    public static implicit operator UpdateTransactionRequest(Transaction transaction)
        => new UpdateTransactionRequest()
        { Id = transaction.Id
        , Title = transaction.Title
        , Amount = transaction.Amount
        , Type = transaction.Type
        , PaidOrReceiveAt = transaction.PaidOrReceivedAt
        , CategoryId = (long)transaction.CategoryId!
        };
}
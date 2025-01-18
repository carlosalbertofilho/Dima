using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions;

public class GetTransactionByIdRequest : Request
{
    [Required(ErrorMessage = "Id is required!")]
    [Range(1, long.MaxValue, ErrorMessage = "Id is not valid, negative or zero!")]
    public long Id { get; set; }
}
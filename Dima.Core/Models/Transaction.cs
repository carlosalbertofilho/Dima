using Dima.Core.Enums;

namespace Dima.Core.Models;

public class Transaction
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    
    public decimal Amount { get; set; }
    
    public long? CategoryId { get; set; } = null;
    public Category? Category { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; } = null;
}
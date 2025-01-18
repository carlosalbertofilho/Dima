using Dima.Core.Common.Extensions;

namespace Dima.Core.Requests.Transactions;

public  class GetTransactionsByPeriodRequest : PagedRequest
{
    public DateTime? StartDate { get; set; } = DateTime.Now.GetFirstDayOfMonth();
    public DateTime? EndDate { get; set; } = DateTime.Now.GetLastDayOfMonth();
}
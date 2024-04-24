using static System.Decimal;

namespace MotokaEasy.Core.Infrastructure.Pagination;
public static class PaginationBusiness
{
    const decimal RoundingValue = (decimal) 0.1;
    
    public static int ReturnCountRemaining(float pagesRemaining, int pageNumber, int totalRecords, int pageSize)
    {
        var countPagesRemaining = pagesRemaining - pageNumber;
            
        if (countPagesRemaining == 0 && totalRecords != pageSize && (int)pagesRemaining != pageNumber)
            countPagesRemaining = 1;
            
        return (int) countPagesRemaining;
    }

    public static int CalculateSkip(PaginationParameters paginationParameters)
    {
        return (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;
    }
    public static int CalculateCountPage(int recordCount, int pageSize)
    {
        var result = (decimal) recordCount / pageSize;
        var fraction = result - Truncate(result);

        if (fraction >= RoundingValue)
            result += 1;

        return (int) result;
    }
}
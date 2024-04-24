namespace MotokaEasy.Core.Infrastructure.Pagination;

public class PaginationReturn<T>
{
    public PaginationReturn() { }

    public PaginationReturn(int pageNumber, int pageSize, int totalRecords, T dataPagination, float pagesRemaining)
    {
        var countPagesRemaining = PaginationBusiness.ReturnCountRemaining(pagesRemaining, pageNumber, totalRecords, pageSize); 
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        DataPagination = dataPagination;
        PagesRemaining = countPagesRemaining <= 0 ? 0 : countPagesRemaining;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int PagesRemaining { get; set; } 
    public T DataPagination { get; set; }
}
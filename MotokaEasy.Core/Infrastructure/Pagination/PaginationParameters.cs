using System.ComponentModel.DataAnnotations;

namespace MotokaEasy.Core.Infrastructure.Pagination;

public class PaginationParameters
{
    
    public PaginationParameters() { }

    public PaginationParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 100 ? 100 : pageSize;
    }
        
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
}
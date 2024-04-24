using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Infrastructure.Pagination;

namespace MotokaEasy.Core.Commands;

public class GetAllPaginationCommand: Notifiable, IValidator
{
    public GetAllPaginationCommand(PaginationParameters paginationParameters)
    {
        PaginationParameters = paginationParameters;
    }

    public PaginationParameters PaginationParameters { get; set;}
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(PaginationParameters.PageNumber <= 0, "PageNumber", "Menor ou igual a 0, Verifique!")
                .IsFalse(PaginationParameters.PageSize <= 0, "PageSize", "Menor ou igual a 0, Verifique!")
                .IsFalse(PaginationParameters.PageSize > 1000, "PageSize", "Maior que 1000, Verifique!")
        );
    }
}
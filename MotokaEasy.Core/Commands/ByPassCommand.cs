using Flunt.Notifications;
using MotokaEasy.Core.Domain.Contracts;

namespace MotokaEasy.Core.Commands;

public class ByPassCommand: Notifiable, IValidator
{
    public void Validate() { }
}
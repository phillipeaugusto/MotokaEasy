namespace MotokaEasy.Core.Shared.Contracts;

public interface IResultGeneric
{
    bool Success { get; set; }
    string Message { get; set; }
    object Data { get; set; }
}
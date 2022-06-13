namespace Domain.Model.Interfaces.Infrastructure;

public interface IFunctionService
{
    Task InvokeAsync(object functionObject);
}
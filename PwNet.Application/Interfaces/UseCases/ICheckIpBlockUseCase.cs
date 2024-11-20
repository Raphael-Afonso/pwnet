namespace PwNet.Application.Interfaces.UseCases
{
    public interface ICheckIpBlockUseCase
    {
        Task<bool> ExecuteAsync(string? ipAddress, CancellationToken cancellationToken);
    }
}
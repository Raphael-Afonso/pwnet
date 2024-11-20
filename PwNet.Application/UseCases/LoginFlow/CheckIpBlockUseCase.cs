using Microsoft.Extensions.Logging;
using PwNet.Application.Interfaces.Infra.Persistence;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Entities;

namespace PwNet.Application.UseCases.PlayerFeatures
{
    public class CheckIpBlockUseCase(IGenericRepository<IpBlocked> ipBlockedRepository, ILogger<CheckIpBlockUseCase> logger) : ICheckIpBlockUseCase
    {
        public async Task<bool> ExecuteAsync(string? ipAddress, CancellationToken cancellationToken)
        {
            try
            {
                if (ipAddress is null) return true;

                var result = await ipBlockedRepository.GetFirstByAction(ip => ip.IpAddress == ipAddress, cancellationToken);

                return result?.IsBlocked ?? false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[CheckIpBlockUseCase] Fail while checking ip block");
                throw;
            }
        }
    }
}

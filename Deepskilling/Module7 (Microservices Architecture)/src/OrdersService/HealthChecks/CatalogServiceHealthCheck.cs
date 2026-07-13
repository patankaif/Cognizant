using Microsoft.Extensions.Diagnostics.HealthChecks;
using OrdersService.Services;

namespace OrdersService.HealthChecks;

public class CatalogServiceHealthCheck : IHealthCheck
{
    private readonly ICatalogServiceClient _catalogServiceClient;

    public CatalogServiceHealthCheck(ICatalogServiceClient catalogServiceClient)
    {
        _catalogServiceClient = catalogServiceClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var isHealthy = await _catalogServiceClient.IsHealthyAsync(cancellationToken);

        return isHealthy
            ? HealthCheckResult.Healthy("CatalogService is reachable.")
            : HealthCheckResult.Unhealthy("CatalogService did not respond successfully.");
    }
}

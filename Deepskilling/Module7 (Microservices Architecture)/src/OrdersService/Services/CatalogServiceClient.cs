using OrdersService.Models;

namespace OrdersService.Services;

public interface ICatalogServiceClient
{
    Task<BookLookupResult?> GetBookAsync(int bookId, CancellationToken cancellationToken = default);
    Task<bool> ReserveStockAsync(int bookId, int quantity, CancellationToken cancellationToken = default);
    Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default);
}

public class CatalogServiceClient : ICatalogServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatalogServiceClient> _logger;

    public CatalogServiceClient(HttpClient httpClient, ILogger<CatalogServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<BookLookupResult?> GetBookAsync(int bookId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"api/books/{bookId}", cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<BookLookupResult>(cancellationToken: cancellationToken);
    }

    public async Task<bool> ReserveStockAsync(int bookId, int quantity, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync(
            $"api/books/{bookId}/reserve-stock?quantity={quantity}",
            content: null,
            cancellationToken);

        if (response.IsSuccessStatusCode)
            return true;

        _logger.LogWarning(
            "CatalogService rejected stock reservation for Book {BookId}, quantity {Quantity}: {StatusCode}",
            bookId, quantity, response.StatusCode);

        return false;
    }

    public async Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("health", cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "CatalogService health check failed");
            return false;
        }
    }
}

namespace Module4.Core.Interfaces;

public interface IInventoryRepository
{
    int GetStock(string sku);
    void ReduceStock(string sku, int quantity);
}

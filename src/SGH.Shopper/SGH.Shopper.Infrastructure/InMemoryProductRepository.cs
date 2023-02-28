using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> _products;

    public InMemoryProductRepository(IEnumerable<Product> products)
    {
        _products = products.ToDictionary(p => p.Id);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values);
    }

    public Task<IEnumerable<Product>> GetByContent(string content)
    {
        var results = _products.Values
            .Where(p => p.Name.Contains(content, StringComparison.OrdinalIgnoreCase)
            || p.Description.Contains(content, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(results);
    }

    public Task<Product> GetByIdAsync(int id)
    {
        return Task.FromResult(_products[id]);
    }
}

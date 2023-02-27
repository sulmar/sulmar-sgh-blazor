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

    public Task<Product> GetByIdAsync(int id)
    {
        return Task.FromResult(_products[id]);
    }
}

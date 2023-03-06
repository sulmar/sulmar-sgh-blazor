using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> _products;

    public InMemoryProductRepository(IEnumerable<Product> products)
    {
        _products = products.ToDictionary(p => p.Id);
    }

    public Task AddAsync(Product product)
    {
        var id = _products.Values.Max(p => p.Id);
        
        product.Id = ++id;

        _products.Add(product.Id, product);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values);
    }

    public Task<PagingResponse<Product>> GetAllAsync(PagingParameters parameters)
    {
        var totalCount = _products.Count;

        var items = _products.Values
            .Skip(parameters.PageNumber * parameters.PageSize)
            .Take(parameters.PageSize).ToList();

        var response = new PagingResponse<Product>
        {
            Items = items,
            TotalCount = totalCount
        };

        return Task.FromResult(response);
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

    public Task UpdateAsync(Product product)
    {
        _products.Remove(product.Id);
        _products.Add(product.Id, product);

        return Task.CompletedTask;  
    }
}

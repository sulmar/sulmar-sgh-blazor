using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure;

public class DbProductRepository : IProductRepository
{
    private readonly ShopperContext context;

    public DbProductRepository(ShopperContext context)
    {
        this.context = context;
    }

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PagingResponse<Product>> GetAllAsync(PagingParameters parameters)
    {
        throw new NotImplementedException();
    }

    public Task<PagingResponse<Product>> GetAllAsync(RequestParameters parameters)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByContent(string content)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}

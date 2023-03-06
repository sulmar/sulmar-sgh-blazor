using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure;

public class InMemoryEntityRepository<T> : IEntityRepository<T> 
    where T : BaseEntity
{
    protected readonly IDictionary<int, T> _entities;

    public InMemoryEntityRepository(IEnumerable<T> entities)
    {
        _entities = entities.ToDictionary(p => p.Id);
    }

    public virtual Task AddAsync(T entity)
    {
        var id = _entities.Values.Max(p => p.Id);

        entity.Id = ++id;

        _entities.Add(entity.Id, entity);

        return Task.CompletedTask;
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_entities.Values);
    }

    public virtual Task<PagingResponse<T>> GetAllAsync(PagingParameters parameters)
    {
        var totalCount = _entities.Count;

        var items = _entities.Values
            .Skip(parameters.PageNumber * parameters.PageSize)
            .Take(parameters.PageSize).ToList();

        var response = new PagingResponse<T>
        {
            Items = items,
            TotalCount = totalCount
        };

        return Task.FromResult(response);
    }

    public Task<PagingResponse<T>> GetAllAsync(RequestParameters parameters)
    {
        var items = _entities.Values.Skip(parameters.StartIndex).Take(parameters.Count);

        var response = new PagingResponse<T> { Items = items, TotalCount = _entities.Count };

        return Task.FromResult(response);
    }

    public virtual Task<IEnumerable<T>> GetByContent(string content)
    {
        var results = _entities.Values
            .Where(p => p.HasContent(content));

        return Task.FromResult(results);
    }

    public virtual Task<T> GetByIdAsync(int id)
    {
        return Task.FromResult(_entities[id]);
    }

    public virtual Task UpdateAsync(T entity)
    {
        _entities.Remove(entity.Id);
        _entities.Add(entity.Id, entity);

        return Task.CompletedTask;
    }
}

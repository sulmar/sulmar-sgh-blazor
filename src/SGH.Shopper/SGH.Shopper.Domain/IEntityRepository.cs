namespace SGH.Shopper.Domain;

public interface IEntityRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByContent(string content);
    Task<PagingResponse<T>> GetAllAsync(PagingParameters parameters);
    Task<PagingResponse<T>> GetAllAsync(RequestParameters parameters);
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task AddAsync(T entity);
}

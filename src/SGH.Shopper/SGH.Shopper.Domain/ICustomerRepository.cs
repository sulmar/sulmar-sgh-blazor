namespace SGH.Shopper.Domain;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    IEnumerable<Customer> GetByEmail(string content);
}

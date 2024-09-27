namespace RepairCompanyApi.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
    Task<long> AddAsync(T entity);
    Task<long> UpdateAsync(T entity);
    Task<long> DeleteAsync(long id);
}

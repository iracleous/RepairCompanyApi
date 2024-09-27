namespace RepairCompanyApi.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(int pageCount, int pageSize);
    Task<T?> GetByIdAsync(long id);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(long id);
}

namespace RepairCompanyApi.Repository;

using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class DapperRepository<T> : IRepository<T> where T : class
{
    private readonly IDbConnection _dbConnection;

    public DapperRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<T>> GetAllAsync(int pageCount, int pageSize)
    {
        var sql = $"SELECT * FROM {typeof(T).Name}s order by Id "; 
        return await _dbConnection.QueryAsync<T>(sql);
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        var sql = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<bool> AddAsync(T entity)
    {
        var sql = $"INSERT INTO {typeof(T).Name}s VALUES (@Entity); SELECT CAST(SCOPE_IDENTITY() as long)";
        var result = await _dbConnection.ExecuteAsync(sql, new { Entity = entity });
        return result > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var sql = $"UPDATE {typeof(T).Name}s SET @Entity WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var sql = $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}

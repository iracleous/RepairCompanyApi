using Dapper;
using RepairCompanyApi.Models;
using System.Data;

namespace RepairCompanyApi.Repository.Implenentations;

public class PropertyOwnerRepositoryDapper : IPropertyOwnerRepository
{
    private readonly IDbConnection _dbConnection;

    public PropertyOwnerRepositoryDapper(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<PropertyOwner>> GetAllAsync(int pageCount, int pageSize)
    {
        var sql = $"SELECT * FROM PropertyOwners order by id OFFSET @Offset ROWS FETCH NEXT @Page ROWS ONLY";
        return await _dbConnection.QueryAsync<PropertyOwner>(sql, new { Offset = (pageCount - 1) * pageSize, Page = pageSize });
    }

    public async Task<PropertyOwner?> GetByIdAsync(long id)
    {
        var sql = $"SELECT * FROM PropertyOwners WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<PropertyOwner>(sql, new { Id = id });
    }

    public async Task<bool> AddAsync(PropertyOwner entity)
    {
        var sql = $"INSERT INTO PropertyOwners VALUES (@Entity); SELECT CAST(SCOPE_IDENTITY() as long)";
        var result = await _dbConnection.ExecuteAsync(sql, new { Entity = entity });
        return result > 0;
    }

    public async Task<bool> UpdateAsync(PropertyOwner entity)
    {
        var sql = $"UPDATE PropertyOwners SET @Entity WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var sql = $"DELETE FROM PropertyOwners WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    public async Task<PropertyOwner?> GetByNameAsync(string name)
    {
        var sql = $"SELECT * FROM PropertyOwners WHERE name = @Name";
        return await _dbConnection.QueryFirstOrDefaultAsync<PropertyOwner>(sql, new { Name = name });

    }
}

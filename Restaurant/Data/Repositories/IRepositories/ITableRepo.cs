using Restaurant.Models;

namespace Restaurant.Data.Repositories.IRepositories
{
    public interface ITableRepo
    {
        // Retrieves all tables asynchronously.
        Task<IEnumerable<Table>> GetAllTablesAsync();

        // Retrieves a table by its ID asynchronously.
        Task<Table> GetTableByIdsAsync(int tableId);

        Task AddTablesAsync(Table table);
        Task UpdateTablesAsync(Table table);
        Task<bool> DeleteTablesAsync(int id);

     
    }
}

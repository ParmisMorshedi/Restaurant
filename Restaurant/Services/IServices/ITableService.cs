using Restaurant.Models.DTOs;

namespace Restaurant.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int tableId);
        Task AddTablesAsync(TableDTO tableDTO);
        Task UpdateTablesAsync(TableDTO tableDTO);
        Task<bool> DeleteTablesAsync(int tableId);
      
        Task<IEnumerable<TableDTO>> GetAvailableTablesAsync(DateTime date, TimeOnly time);
    }
}

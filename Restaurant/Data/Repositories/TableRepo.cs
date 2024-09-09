using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;

namespace Restaurant.Data.Repositories
{
    public class TableRepo : ITableRepo
    {

        private readonly RestaurantContext _context;
      
        // Constructor to initialize the context
        public TableRepo(RestaurantContext context) 
        {
            _context = context;
           
        }

        // Adds a new table to the database
        public async Task AddTablesAsync(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table), "Table cannot be null.");
            }

            try
            {
                await _context.Tables.AddAsync(table);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Logga felet
                throw new InvalidOperationException("An error occurred while adding the table.", ex);
            }
        }

        // Deletes a table by its ID
        public async Task<bool> DeleteTablesAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
                return false;

            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;

        }

        // Retrieves all tables from the database
        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            try
            {
                return await _context.Tables.ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException("An error occurred while retrieving all tables.", ex);
            }
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date, TimeOnly time)
        {


            var bookedTables = await _context.Reservations
          .Where(r => r.Date == date.Date && r.Time == time)
          .Select(r => r.TableId)
          .ToListAsync();

           
            var availableTables = await _context.Tables
                .Where(t => !bookedTables.Contains(t.Id))
                .ToListAsync();

            return availableTables;

        }

    

        // Retrieves a specific table by its ID
        public async Task<Table> GetTableByIdsAsync(int id)
        {
            try
            {
                return await _context.Tables.FindAsync(id);
            }
            catch (Exception ex)
            {
               
                throw new InvalidOperationException("An error occurred while retrieving the table.", ex);
            }
        }

        // Updates an existing table
        public async Task UpdateTablesAsync(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table), "Table cannot be null.");
            }

            try
            {
                _context.Tables.Update(table);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException("An error occurred while updating the table.", ex);
            }
        }
            
    }
}

       

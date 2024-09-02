using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Data.Repositories;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.IServices;
using TableModel = Restaurant.Models.Table;

namespace Restaurant.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepo _tableRepo;
        private readonly RestaurantContext _context;
        private readonly IReservationRepo _reservationRepo;

        // Constructor injection for repository and context dependencies
        public TableService(ITableRepo tableRepo, RestaurantContext context, IReservationRepo reservationRepo)
        {
            _tableRepo = tableRepo;
            _context = context;
            _reservationRepo = reservationRepo;

        }
        // Retrieve all tables and convert them to DTOs
        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            try
            {
                var tableList = await _tableRepo.GetAllTablesAsync();
                return tableList.Select(t => new TableDTO
                {
                    TableId = t.Id,
                    Number = t.Number,
                    Seats = t.Seats
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving all tables: {ex.Message}");
                throw;
            }
        }
    
        // Add a new table based on the provided DTO
        public async Task AddTablesAsync(TableDTO tableDTO)
        {
            if (tableDTO == null)
            {
                throw new ArgumentNullException(nameof(tableDTO), "TableDTO cannot be null.");
            }

            var newTable = new Table
            {
                Number = tableDTO.Number,
                Seats = tableDTO.Seats
            };

            try
            {
                await _tableRepo.AddTablesAsync(newTable);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
          
                Console.WriteLine($"An error occurred while adding the table: {ex.Message}");
                throw;
            }
        }
        // Delete a table by its ID
        public async Task<bool> DeleteTablesAsync(int tableId)
        {
            try
            {
                return await _tableRepo.DeleteTablesAsync(tableId);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while deleting the table: {ex.Message}");
                throw;
            }
        }

        // Retrieve a single table by its ID and convert to DTO
        public async Task<TableDTO> GetTableByIdAsync(int tableId)
        {
            try
            {
                var table = await _tableRepo.GetTableByIdsAsync(tableId);
                if (table == null) return null;

                return new TableDTO
                {
                    TableId = table.Id,
                    Number = table.Number,
                    Seats = table.Seats
                };
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving the table by ID: {ex.Message}");
                throw;
            }
        }
        // Update an existing table based on DTO
        public async Task UpdateTablesAsync(TableDTO tableDTO)
        {
            if (tableDTO == null)
            {
                throw new ArgumentNullException(nameof(tableDTO), "TableDTO cannot be null.");
            }

            try
            {
                var table = await _context.Tables.FindAsync(tableDTO.TableId);
                if (table == null)
                {
                    throw new KeyNotFoundException("Table not found.");
                }

                // Update the table with new values from DTO
                table.Number = tableDTO.Number;
                table.Seats = tableDTO.Seats;

                _context.Tables.Update(table);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while updating the table: {ex.Message}");
                throw;
            };
        }

        public async Task<IEnumerable<TableDTO>> GetAvailableTablesAsync(DateTime date, TimeOnly time)
        {
            try
            {
                // Retrieve reservations for the specified date and time from the reservation repository
                var reservations = await _reservationRepo.GetReservationByDatesAsync(date, time);

                // Retrieve all tables from the table repository
                var allTables = await _tableRepo.GetAllTablesAsync();

                // Extract the IDs of the tables that are reserved
                var reservedTableIds = reservations
                    .Where(r => r.Date.Date == date.Date && r.Time == time)
                    .Select(r => r.TableId)
                    .Distinct();

                // Filter out the reserved tables from the list of all tables
                var availableTables = allTables
                    .Where(t => !reservedTableIds.Contains(t.Id))
                    .Select(t => new TableDTO
                    {
                        TableId = t.Id,
                        Number = t.Number,
                        Seats = t.Seats
                    })
                    .ToList();

                return availableTables;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while retrieving available tables: {ex.Message}");
                throw;
            }
        }

        



    }
}

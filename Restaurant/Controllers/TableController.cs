using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Models.DTOs;
using Restaurant.Services.IServices;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        // GET: api/Table
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables()
        {
            try
            {
                var tablesList = await _tableService.GetAllTablesAsync();
                return Ok(tablesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the tables.");
            }
        }
        // GET: api/Table/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int id)
        {
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
                if (table == null)
                {
                    return NotFound("Table not found.");
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the table.");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] TableDTO tableDTO)
        {
            try
            {
                await _tableService.AddTablesAsync(tableDTO);
                return CreatedAtAction(nameof(GetTableById), new { id = tableDTO.TableId }, tableDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the table.");
            }
        }
        // PUT: api/Table/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTable(int id, [FromBody] TableDTO tableDTO)
        {
            if (id != tableDTO.TableId)
            {
                return BadRequest("Table ID mismatch.");
            }

            try
            {
                await _tableService.UpdateTablesAsync(tableDTO);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Table not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the table.");
            }
        }
        // DELETE: api/Table/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            try
            {
                var success = await _tableService.DeleteTablesAsync(id);
                if (!success)
                {
                    return NotFound("Table not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the table.");
            }
        }
        // GET: api/Table/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTables([FromQuery] DateTime date, [FromQuery] TimeOnly time)
        {
            try
            {
                // Retrieve available tables based on the provided date and time
                var availableTables = await _tableService.GetAvailableTablesAsync(date, time);

                // Check if any available tables are found
                if (!availableTables.Any())
                {
                    return NotFound("No available tables found.");
                }

                return Ok(availableTables);
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving available tables: {ex.Message}");
            }

        }
    }
}

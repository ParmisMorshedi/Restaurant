using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models.DTOs;
using Restaurant.Services.IServices;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetAllMenus()
        {
            try
            {
                var menus = await _menuService.GetAllMenuAsync();
                return Ok(menus);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error status code for unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving menu items.");
            }
        }

        // GET: api/Menu/
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenuById(int id)
        {
            try
            {
                var menu = await _menuService.GetMenuByIdAsync(id);
                if (menu == null)
                {
                   
                    return NotFound();
                }
                return Ok(menu);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error status code for unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the menu item.");
            }
        }

        // POST: api/Menu
        [HttpPost]
        public async Task<ActionResult<MenuDTO>> AddMenu(MenuDTO menuDTO)
        {
            try
            {
                // Add the new menu item
                await _menuService.AddMenuAsync(menuDTO);
                // Return a 201 Created status code with the location of the newly created item
                return CreatedAtAction(nameof(GetMenuById), new { id = menuDTO.MenuId }, menuDTO);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the menu item.");
            }
        }

        // PUT: api/Menu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, MenuDTO menuDTO)
        {
            if (id != menuDTO.MenuId)
            {
                return BadRequest("Menu ID mismatch.");
            }
            try
            {
                var updated = await _menuService.UpdateMenuAsync(menuDTO);
                if (!updated)
                {
                    
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the menu item.");
            }
        }

        // DELETE: api/Menu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var deleted = await _menuService.DeleteMenuAsync(id);
                if (!deleted)
                {
                   
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error status code for unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the menu item.");
            }
        }
    }
}

using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Services.IServices;

namespace Restaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepo _menuRepo;

        // Constructor injection for repository dependency
        public MenuService(IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }

        // Retrieve all menu items and convert them to DTOs

        public async Task<IEnumerable<MenuDTO>> GetAllMenuAsync()
        {
            try
            {
                var menus = await _menuRepo.GetAllMenusAsync();

                // Convert each Menu entity to MenuDTO
                return menus.Select(m => new MenuDTO
                {
                    MenuId = m.Id,
                    DishName = m.DishName,
                    Price = m.Price,
                    Description = m.Description,

                });
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving all menus: {ex.Message}");
                throw;
            }
        }
        // Retrieve a single menu item by its ID and convert to DTO
        public async Task<MenuDTO> GetMenuByIdAsync(int menuId)
        {
            try
            {
                var menu = await _menuRepo.GetMenuByIdsAsync(menuId);
                if (menu == null) return null;

                return new MenuDTO
                {
                    MenuId = menu.Id,
                    DishName = menu.DishName,
                    Price = menu.Price,
                    Description = menu.Description
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving the menu by ID: {ex.Message}");
                throw;
            }
        }
        // Add a new menu item based on DTO
        public async Task AddMenuAsync(MenuDTO menuDTO)
        {
            if (menuDTO == null)
            {
                throw new ArgumentNullException(nameof(menuDTO), "MenuDTO cannot be null.");
            }
            try
            {
                // Convert MenuDTO to Menu entity
                var menu = new Menu
                {
                    DishName = menuDTO.DishName,
                    Price = menuDTO.Price,
                    Description = menuDTO.Description
                };

                await _menuRepo.AddMenusAsync(menu);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while adding the menu: {ex.Message}");
                throw;
            }

        }
        // Update an existing menu item based on DTO
        public async Task<bool> UpdateMenuAsync(MenuDTO menuDTO)
        {
            if (menuDTO == null)
            {
                throw new ArgumentNullException(nameof(menuDTO), "MenuDTO cannot be null.");
            }

            try
            {
                var menu = await _menuRepo.GetMenuByIdsAsync(menuDTO.MenuId);
                if (menu == null) return false;

                // Update the Menu entity with new values from DTO
                menu.DishName = menuDTO.DishName;
                menu.Price = menuDTO.Price;
                menu.Description = menuDTO.Description;
               

                await _menuRepo.UpdateMenusAsync(menu);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while updating the menu: {ex.Message}");
                throw;
            }
        }
        // Delete a menu item by its ID

        public async Task<bool> DeleteMenuAsync(int menuId)
        {
            try
            {
                return await _menuRepo.DeleteMenusAsync(menuId);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while deleting the menu: {ex.Message}");
                throw;
            }
        }
    }
}

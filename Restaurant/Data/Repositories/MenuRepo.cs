using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Data.Repositories
{
    public class MenuRepo : IMenuRepo
    {
        private readonly RestaurantContext _context;

        // Constructor to initialize the context
        public MenuRepo(RestaurantContext context)
        {
            _context = context;
        }
        // Adds a new menu item to the database
        public async Task AddMenusAsync(Menu menu)
        {
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu), "Menu cannot be null.");
            }
            try
            {
                _context.Menues.Add(menu);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while adding the menu: {ex.Message}");
                throw;
            }
        }
        // Deletes a menu item by its ID
        public async Task<bool> DeleteMenusAsync(int menuId)
        {
            // Finds the menu item by its ID
            var menu = await _context.Menues.FindAsync(menuId);
            // If the menu item is not found, return false
            if (menu == null) return false;

            _context.Menues.Remove(menu);
            await _context.SaveChangesAsync();
            // Returns true indicating the deletion was successful
            return true;
        }

        // Retrieves all menu items from the database
        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            // Asynchronously retrieves the list of menu items
            return await _context.Menues.ToListAsync();
        }

        // Retrieves a specific menu item by its ID
        public async Task<Menu> GetMenuByIdsAsync(int menuId)
        {
            return await _context.Menues.FindAsync(menuId);
        }

        // Updates an existing menu item
        public async Task<bool> UpdateMenusAsync(Menu menu)
        {
            var existingMenu = await _context.Menues.FindAsync(menu.Id);
            if (existingMenu == null)
            {
                return false;
            }

            // Update properties
            existingMenu.DishName = menu.DishName;
            existingMenu.Price = menu.Price;
            existingMenu.Description = menu.Description;
            existingMenu.IsAvailable = menu.IsAvailable; // Make sure this is updated

            // Save changes
            await _context.SaveChangesAsync();
            return true;

        }
    }
}

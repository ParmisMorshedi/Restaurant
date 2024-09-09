using Restaurant.Models;



namespace Restaurant.Data.Repositories.IRepositories
{
    public interface IMenuRepo
    {
        // Retrieves all menus asynchronously.
        Task<IEnumerable<Menu>> GetAllMenusAsync();

        // Retrieves a menu by its ID asynchronously.
        Task<Menu> GetMenuByIdsAsync(int menuId);
        Task AddMenusAsync(Menu menu);
        Task<bool> DeleteMenusAsync(int menuId);
        Task <bool>UpdateMenusAsync(Menu menu);
    }
}

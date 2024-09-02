using Restaurant.Models.DTOs;

namespace Restaurant.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDTO>> GetAllMenuAsync();
        Task<MenuDTO> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(MenuDTO menuDTO);
        Task<bool> UpdateMenuAsync(MenuDTO menuDTO);
        Task<bool> DeleteMenuAsync(int menuId);
    }
}

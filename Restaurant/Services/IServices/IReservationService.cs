using Restaurant.Models;
using Restaurant.Models.DTOs;

namespace Restaurant.Services.IServices
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task<ReservationDTO> GetReservationsByIdAsync(int reservationId);

        Task<IEnumerable<ReservationDTO>> GetReservationByDatesAsync(DateTime date, TimeOnly time);
        Task<bool> CheckReservationExistsAsync(DateTime date, TimeOnly time);
    
        Task AddReservationsAsync(ReservationDTO reservationDTO);
        Task UpdateReservationsAsync(ReservationDTO reservationDTO);
        Task<bool> DeleteReservationsAsync(int reservationId);
       
    }
}

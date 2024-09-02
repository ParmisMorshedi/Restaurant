using Restaurant.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Data.Repositories.IRepositories
{
    public interface IReservationRepo
    {
        // Retrieves all reservations asynchronously.
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();

        // Retrieves a reservation by its ID asynchronously.
        Task<Reservation> GetReservationAsync(int id);

        // Retrieves reservations by a specific date and time asynchronously.
        Task<IEnumerable<Reservation>> GetReservationByDatesAsync(DateTime date, TimeOnly time);

        // Checks if a reservation exists for a specific date and time asynchronously.
        Task<bool> CheckReservationExistsAsync(DateTime date, TimeOnly time);

        Task AddReservationsAsync(Reservation reservation);

        // Deletes a reservation by its ID asynchronously. Returns true if successful.
        Task<bool>DeleteReservationsAsync(int id);
        Task UpdateReservationsAsync(Reservation reservation);
        
    }
}

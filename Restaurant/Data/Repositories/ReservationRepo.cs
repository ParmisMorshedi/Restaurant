using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;

namespace Restaurant.Data.Repositories
{
    public class ReservationRepo : IReservationRepo
    {
        private readonly RestaurantContext _context;

        // Constructor to initialize the context
        public ReservationRepo(RestaurantContext context) 
        {
            _context = context;
        }


        // Adds a new reservation to the database
        public async Task AddReservationsAsync(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");
            }

            try
            {
                await _context.Reservations.AddAsync(reservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while adding the reservation: {ex.Message}");
                throw;
            }

        }   


        // Deletes a reservation by its ID
        public async Task<bool> DeleteReservationsAsync(int reservationId)
        {
            // Finds the reservation item by its ID
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                return false; 
            }

            _context.Reservations.Remove(reservation);
            return await _context.SaveChangesAsync() > 0;
        }



        // Retrieves all reservations from the database
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();

        }



        // Retrieves a specific reservation by its ID
        public async Task<Reservation> GetReservationAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }



        // Updates an existing reservation
        public async Task UpdateReservationsAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();  
        }



        // Retrieves reservations based on the date and time
        public async Task<IEnumerable<Reservation>> GetReservationByDatesAsync(DateTime date,TimeOnly time)
        {
            // Filters reservations based on the specified date and time
            return await _context.Reservations
                                 .Where(r => r.Date.Date == date.Date && r.Time == time)
                                 .ToListAsync();
        }



        // Checks if a reservation exists based on the date and time
        public async Task<bool> CheckReservationExistsAsync(DateTime date, TimeOnly time)
        {
            // Determines if any reservations exist with the specified date and time
            return await _context.Reservations
                .AnyAsync(r => r.Date.Date == date.Date && r.Time == time);
        }

     
        

    }
}

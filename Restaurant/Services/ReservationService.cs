using Restaurant.Data;
using Restaurant.Models.DTOs;
using Restaurant.Services.IServices;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;
using Restaurant.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Services
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepo _reservationRepo;
        private readonly ITableRepo _tableRepo;
        private readonly ICustomerRepo _customerRepo;

        // Constructor injection for repository dependencies
        public ReservationService(IReservationRepo reservationRepo, ITableRepo tableRepo, ICustomerRepo customerRepo)
        {
            _reservationRepo = reservationRepo;
            _tableRepo = tableRepo;
            _customerRepo = customerRepo;

        }

        // Add a new reservation based on the provided DTO
        public async Task AddReservationsAsync(ReservationDTO reservationDTO)
        {
            if (reservationDTO == null)
            {
                throw new ArgumentNullException(nameof(reservationDTO), "ReservationDTO cannot be null.");
            }

            // Validate if the table and customer exist
            var table = await _tableRepo.GetTableByIdsAsync(reservationDTO.TableId);
            var customer = await _customerRepo.GetCustomerByIdsAsync(reservationDTO.CustomerId);

            if (table == null || customer == null)
            {
                throw new ArgumentException("Invalid TableId or CustomerId.");
            }

            // Check if the reservation already exists
            var isAvailable = await _reservationRepo.CheckReservationExistsAsync(reservationDTO.Date, reservationDTO.Time);
            if (isAvailable)
            {
                throw new InvalidOperationException("Table is already reserved for this time.");
            }

            // Create and add the new reservation
            var newReservation = new Reservation
            {
                TableId = reservationDTO.TableId,
                CustomerId = reservationDTO.CustomerId,
                Time = reservationDTO.Time,
                Date = reservationDTO.Date,
                NumberOfGuests = reservationDTO.NumberOfGuests
            };

            try
            {
                await _reservationRepo.AddReservationsAsync(newReservation);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while adding the reservation: {ex.Message}");
                throw;
            }
        }

        // Delete a reservation by its ID
        public async Task<bool> DeleteReservationsAsync(int reservationId)
        {
            try
            {
                return await _reservationRepo.DeleteReservationsAsync(reservationId);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while deleting the reservation: {ex.Message}");
                throw;
            }
        }

        // Retrieve all reservations and convert them to DTOs
        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync()
        {
            try
            {
                var reservations = await _reservationRepo.GetAllReservationsAsync();
                return reservations.Select(r => new ReservationDTO
                {
                    ReservationId = r.Id,
                    TableId = r.TableId,
                    CustomerId = r.CustomerId,
                    Time = r.Time,
                    Date = r.Date,
                    NumberOfGuests = r.NumberOfGuests
                }).ToList();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while retrieving all reservations: {ex.Message}");
                throw;
            }
        }

        // Retrieve a single reservation by its ID and convert to DTO
        public async Task<ReservationDTO> GetReservationsByIdAsync(int reservationId)
        {

            try
            {
                var reservation = await _reservationRepo.GetReservationAsync(reservationId);
                if (reservation == null) return null;

                return new ReservationDTO
                {
                    ReservationId = reservation.Id,
                    TableId = reservation.TableId,
                    CustomerId = reservation.CustomerId,
                    Time = reservation.Time,
                    Date = reservation.Date,
                    NumberOfGuests = reservation.NumberOfGuests
                };
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving the reservation by ID: {ex.Message}");
                throw;
            }
        }
       

        

        // Update an existing reservation based on DTO
        public async Task UpdateReservationsAsync(ReservationDTO reservationDTO)
        {
            if (reservationDTO == null)
            {
                throw new ArgumentNullException(nameof(reservationDTO), "ReservationDTO cannot be null.");
            }

            try
            {
                var reservation = await _reservationRepo.GetReservationAsync(reservationDTO.ReservationId);
                if (reservation == null)
                {
                    throw new ArgumentException("Reservation not found.");
                }

                // Update the reservation with new values from DTO
                reservation.Time = reservationDTO.Time;
                reservation.Date = reservationDTO.Date;
                reservation.NumberOfGuests = reservationDTO.NumberOfGuests;

                // Check if the updated reservation conflicts with existing reservations
                var isAvailable = await _reservationRepo.CheckReservationExistsAsync(reservationDTO.Date, reservationDTO.Time);
                if (isAvailable)
                {
                    throw new InvalidOperationException("Table is already reserved for this time.");
                }

                await _reservationRepo.UpdateReservationsAsync(reservation);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while updating the reservation: {ex.Message}");
                throw;
            }
        }

        // Retrieve reservations by date and time, and convert them to DTOs
        public async Task<IEnumerable<ReservationDTO>> GetReservationByDatesAsync(DateTime date, TimeOnly time)
        {
            try
            {
                var reservations = await _reservationRepo.GetReservationByDatesAsync(date, time);
                if (reservations == null || !reservations.Any())
                {
                    return Enumerable.Empty<ReservationDTO>();
                }

                return reservations.Select(r => new ReservationDTO
                {
                    ReservationId = r.Id,
                    TableId = r.TableId,
                    CustomerId = r.CustomerId,
                    Time = r.Time,
                    Date = r.Date,
                    NumberOfGuests = r.NumberOfGuests
                }).ToList();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving reservations by date and time: {ex.Message}");
                throw;
            }
        }

        // Check if a reservation exists for the specified date and time
        public async Task<bool> CheckReservationExistsAsync(DateTime date, TimeOnly time)
        {
            try
            {
                return await _reservationRepo.CheckReservationExistsAsync(date, time);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"An error occurred while checking reservation existence: {ex.Message}");
                throw;
            }
        }

   
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingSystem.Models
{
    internal class Reservation
    {
        public int ReservationId { get; set; }
        public Customer Customer { get; set; } = new Customer();
        public Room Room { get; set; } = new Room();
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }

        public int Nights => (CheckOut - CheckIn).Days;

        public decimal Subtotal => Room.Rate * Nights;
        public decimal Tax => Math.Round(Subtotal * Settings.TaxRate, 2, MidpointRounding.AwayFromZero);
        public decimal Total => Subtotal + Tax;


        public List<string> Validate()
        {
            var errors = new List<string>();

            if (CheckIn.date < DateTime.Today)
            {
                errors.Add("Check-in date cannot be in the past.");
            }
            if (CheckOut.date <= CheckIn.date)
            {
                errors.Add("Check-out date must be after check-in date.");
            }

            if (NumberOfGuests < 1)
            {
                errors.Add("Number of guests must be at least 1.");
            }

            if (NumberOfGuests > Room.MaxOccupancy)
            {
                errors.Add($"Number of guests exceeds room's maximum occupancy of {Room.MaxOccupancy}.");
            }
            return errors;
        }
    }

    
}

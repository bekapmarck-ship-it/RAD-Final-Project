using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingSystem.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string FName { get; set; } = "";
        public string LName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; }
    }
}

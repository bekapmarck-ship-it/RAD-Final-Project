using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingSystem.Models
{
    internal class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; } = "";
        public decimal Rate { get; set; }
        public int MaxOccupancy { get; set; }
    }
}

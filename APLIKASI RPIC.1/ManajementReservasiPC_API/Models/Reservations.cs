using System;

namespace ManajementReservasiPC_API.Models
{
    public class Reservation
    {
        public int PCNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

using System.Collections.Generic;
using System;

public class ReservationManager
{
    public class PC
    {
        public int Number { get; set; }
        public string Specification { get; set; }
        public bool IsReserved { get; set; }
    }

    public class Reservation
    {
        public int ReservationId { get; set; }
        public int PCNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    private List<PC> pcs;
    private List<Reservation> reservations;

}



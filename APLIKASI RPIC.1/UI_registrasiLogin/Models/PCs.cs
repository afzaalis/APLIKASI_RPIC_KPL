using System;

namespace UI_registrasiLogin.Models
{
    public class PC
    {
        public int Number { get; set; }
        public string Specification { get; set; }
        public ReservationState State { get; set; } = ReservationState.Available;
    }
}

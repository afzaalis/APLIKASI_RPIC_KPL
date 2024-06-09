using System;

namespace UI_registrasiLogin.Models
{
    public class Reservation
    {
        public int PCNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationState State { get; set; } = ReservationState.Reserved;

        // Update state based on current time
        public void UpdateState(DateTime currentTime)
        {
            if (State == ReservationState.Reserved && currentTime >= StartTime && currentTime < EndTime)
            {
                State = ReservationState.InUse;
            }
            else if (State == ReservationState.InUse && currentTime >= EndTime)
            {
                State = ReservationState.Completed;
            }
        }
    }
}

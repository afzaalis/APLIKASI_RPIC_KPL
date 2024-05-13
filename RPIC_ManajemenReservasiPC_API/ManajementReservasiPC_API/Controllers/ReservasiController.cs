using Microsoft.AspNetCore.Mvc;

namespace ManajementReservasiPC_API.Controllers
{
    public class ReservasiController : Controller
    {
        public class ReservationManager
        {
            public class PC
            {
                public int Number { get; set; }
                public string Specification { get; set; }
                public bool IsReserved { get; set; }
            }

            // Struktur data untuk merepresentasikan reservasi
            public class Reservation
            {
                public int PCNumber { get; set; }                public DateTime StartTime { get; set; }
                public DateTime EndTime { get; set; }
            }

            // Kelas utama untuk manajemen reservasi PC
          
                private List<PCConfiguration> pcConfigurations;
            private List<PC> pcs;
            private List<Reservation> reservations;

            public ReservationManager(List<PCConfiguration> pcConfigurations)
            {
                this.pcConfigurations = pcConfigurations;
                pcs = new List<PC>();
                reservations = new List<Reservation>();
            }


            public void InitializePCsFromConfiguration()
            {
                foreach (var pcConfig in pcConfigurations)
                {
                    pcs.Add(new PC { Number = pcConfig.Number, Specification = pcConfig.Specification, IsReserved = false });
                }
            }
        }
    }
}

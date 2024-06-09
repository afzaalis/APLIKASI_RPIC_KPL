using ManajementReservasiPC_API.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ManajementReservasiPC_API.Managers
{
    public class ReservationManager
    {
        private List<PC> pcs;
        private List<Reservation> reservations;
        private const string jsonFilePath = "D:\\New folder\\UI_registrasiLogin\\ManajementReservasiPC_API\\DataPC\\dataPC.json";

        public ReservationManager()
        {
            pcs = new List<PC>();
            reservations = new List<Reservation>();
            LoadPCsFromJson();
        }

        private void LoadPCsFromJson()
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                var pcConfigurations = JsonSerializer.Deserialize<PCConfiguration>(json);
                if (pcConfigurations != null)
                {
                    pcs = pcConfigurations.PCConfigurations ?? new List<PC>();
                    reservations = pcConfigurations.Reservations ?? new List<Reservation>();
                }
            }
        }

        private void SavePCsToJson()
        {
            var pcConfigurations = new PCConfiguration { PCConfigurations = pcs, Reservations = reservations };
            string json = JsonSerializer.Serialize(pcConfigurations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, json);
        }

        public IEnumerable<PC> GetPCs()
        {
            return pcs;
        }

        public Reservation GetReservation(int pcNumber)
        {
            return reservations.FirstOrDefault(r => r.PCNumber == pcNumber);
        }

        public void AddReservation(Reservation reservation)
        {
            var pc = pcs.FirstOrDefault(p => p.Number == reservation.PCNumber);
            if (pc != null && !pc.IsReserved)
            {
                pc.IsReserved = true;
                reservations.Add(reservation);
                SavePCsToJson();
            }
        }

        public void UpdateReservation(int pcNumber, Reservation updatedReservation)
        {
            var reservation = reservations.FirstOrDefault(r => r.PCNumber == pcNumber);
            if (reservation != null)
            {
                reservation.StartTime = updatedReservation.StartTime;
                reservation.EndTime = updatedReservation.EndTime;
                SavePCsToJson();
            }
        }

        public void DeleteReservation(int pcNumber)
        {
            var reservation = reservations.FirstOrDefault(r => r.PCNumber == pcNumber);
            if (reservation != null)
            {
                reservations.Remove(reservation);
                var pc = pcs.FirstOrDefault(p => p.Number == pcNumber);
                if (pc != null)
                {
                    pc.IsReserved = false;
                    SavePCsToJson();
                }
            }
        }
    }
}

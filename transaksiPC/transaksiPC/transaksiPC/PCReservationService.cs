using System;
using System.Collections.Generic;
using ReservationSystem.Models;

namespace ReservationSystem.Services
{
    public class PCReservationService
    {
        private readonly List<PC> _pcs;

        public PCReservationService()
        {
            // Inisialisasi daftar PC
            _pcs = new List<PC>
            {
                new PC { Id = 1, Name = "PC1", IsReserved = false },
                new PC { Id = 2, Name = "PC2", IsReserved = false },
                new PC { Id = 3, Name = "PC3", IsReserved = false }
            };
        }

        public void ShowPCStatus()
        {
            Console.WriteLine("PC Status:");
            foreach (var pc in _pcs)
            {
                Console.WriteLine($"ID: {pc.Id}, Name: {pc.Name}, Reserved: {(pc.IsReserved ? "Yes" : "No")}");
            }
        }

        public bool ReservePC(int pcId)
        {
            var pc = GetPCById(pcId);
            if (pc == null || pc.IsReserved)
            {
                return false;
            }

            pc.IsReserved = true;
            return true;
        }

        public bool ReleasePC(int pcId)
        {
            var pc = GetPCById(pcId);
            if (pc == null || !pc.IsReserved)
            {
                return false;
            }

            pc.IsReserved = false;
            return true;
        }

        private PC GetPCById(int id)
        {
            return _pcs.Find(pc => pc.Id == id);
        }
    }
}

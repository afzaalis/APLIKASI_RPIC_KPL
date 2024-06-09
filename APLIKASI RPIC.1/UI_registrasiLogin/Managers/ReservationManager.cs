using System;
using System.Collections.Generic;
using System.Linq;
using UI_registrasiLogin.Models;

namespace UI_registrasiLogin
{
    public class ReservationManager
    {
        //generics
        private List<PC> pcs;
        private List<Reservation> reservations;
        private List<ReservationRule> rules;

        // Define the state machine for the PC
        public ReservationManager()
        {
            pcs = new List<PC>();
            reservations = new List<Reservation>();
            rules = new List<ReservationRule>();

            // Sample data
            pcs.Add(new PC { Number = 1, Specification = "Specs1", State = ReservationState.Available });
            pcs.Add(new PC { Number = 2, Specification = "Specs2", State = ReservationState.Available });
            pcs.Add(new PC { Number = 3, Specification = "Specs3", State = ReservationState.Reserved });
        }

        public void AddRule(string pcSpecification, TimeSpan maxReservationTime)
        {
            rules.Add(new ReservationRule { PCSpecification = pcSpecification, MaxReservationTime = maxReservationTime });
        }

        public bool AddPC(int number, string specification, ReservationState state)
        {
            // Check if the PC number already exists
            if (pcs.Any(p => p.Number == number))
            {
                Console.WriteLine($"PC with number {number} already exists.");
                return false;
            }

            // Add the PC
            pcs.Add(new PC { Number = number, Specification = specification, State = state });
            Console.WriteLine($"PC with number {number} added successfully.");
            return true;
        }

        public void UpdatePCState(int pcNumber, ReservationState state)
        {
            var pc = pcs.FirstOrDefault(p => p.Number == pcNumber);
            if (pc != null)
            {
                pc.State = state;
            }
        }

        public List<PC> GetPCs()
        {
            return pcs;
        }

        public bool AddReservation(int pcNumber, DateTime startTime, DateTime endTime)
        {
            var pc = pcs.FirstOrDefault(p => p.Number == pcNumber);
            if (pc == null || pc.State != ReservationState.Available)
            {
                Console.WriteLine("PC is not available for reservation.");
                return false;
            }

            var rule = rules.FirstOrDefault(r => r.PCSpecification == pc.Specification);
            if (rule != null && (endTime - startTime) > rule.MaxReservationTime)
            {
                Console.WriteLine($"PC with specification {pc.Specification} can only be reserved for {rule.MaxReservationTime.TotalHours} hours.");
                return false;
            }

            if (!IsPCAvailable(pcNumber, startTime, endTime))
            {
                Console.WriteLine("PC is not available at the requested time.");
                return false;
            }

            pc.State = ReservationState.Reserved;
            reservations.Add(new Reservation { PCNumber = pcNumber, StartTime = startTime, EndTime = endTime });
            Console.WriteLine("Reservation successfully added.");
            return true;
        }

        private bool IsPCAvailable(int pcNumber, DateTime startTime, DateTime endTime)
        {
            return !reservations.Any(r => r.PCNumber == pcNumber &&
                                           ((startTime >= r.StartTime && startTime < r.EndTime) ||
                                           (endTime > r.StartTime && endTime <= r.EndTime)));
        }

        //code reuse
        public bool PCExists(int pcNumber)
        {
            return pcs.Any(p => p.Number == pcNumber);
        }
    }
}

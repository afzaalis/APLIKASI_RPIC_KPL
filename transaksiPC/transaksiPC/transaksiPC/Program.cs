using System;
using ReservationSystem.Services;

namespace ReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var pcReservationService = new PCReservationService();

            Console.WriteLine("Welcome to PC Reservation System!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Show PC Status");
                Console.WriteLine("2. Reserve PC");
                Console.WriteLine("3. Release PC");
                Console.WriteLine("4. Exit");

                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        pcReservationService.ShowPCStatus();
                        break;

                    case "2":
                        Console.Write("Enter PC ID to reserve: ");
                        int pcId = int.Parse(Console.ReadLine());
                        if (pcReservationService.ReservePC(pcId))
                        {
                            Console.WriteLine("PC reserved successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to reserve PC. It might be already reserved or not exist.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter PC ID to release: ");
                        pcId = int.Parse(Console.ReadLine());
                        if (pcReservationService.ReleasePC(pcId))
                        {
                            Console.WriteLine("PC released successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to release PC. It might not be reserved or not exist.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}

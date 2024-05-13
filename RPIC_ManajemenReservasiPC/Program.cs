using System;
using System.Collections.Generic;

// Struktur data untuk merepresentasikan PC
public class PC
{
    public int Number { get; set; }
    public string Specification { get; set; }
    public bool IsReserved { get; set; }
}

// Struktur data untuk merepresentasikan reservasi
public class Reservation
{
    public int PCNumber { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

// Kelas utama untuk manajemen reservasi PC
public class ReservationManager
{
    private List<PC> pcs;
    private List<Reservation> reservations;

    public ReservationManager()
    {
        pcs = new List<PC>();
        reservations = new List<Reservation>();
    }

    // Fungsi untuk menambahkan PC baru
    public void AddPC(int number, string specification)
    {
        pcs.Add(new PC { Number = number, Specification = specification, IsReserved = false });
    }

    // Fungsi untuk menambahkan reservasi
    public bool AddReservation(int pcNumber, DateTime startTime, DateTime endTime)
    {
        // Periksa apakah PC tersedia pada waktu yang diminta
        if (!IsPCAvailable(pcNumber, startTime, endTime))
        {
            Console.WriteLine("PC tidak tersedia pada waktu yang diminta.");
            return false;
        }

        // Tandai PC sebagai terpesan
        foreach (var pc in pcs)
        {
            if (pc.Number == pcNumber)
            {
                pc.IsReserved = true;
                break;
            }
        }

        // Tambahkan reservasi baru
        reservations.Add(new Reservation { PCNumber = pcNumber, StartTime = startTime, EndTime = endTime });
        Console.WriteLine("Reservasi berhasil ditambahkan.");
        return true;
    }

    // Fungsi untuk memeriksa ketersediaan PC pada waktu yang diminta
    private bool IsPCAvailable(int pcNumber, DateTime startTime, DateTime endTime)
    {
        foreach (var reservation in reservations)
        {
            if (reservation.PCNumber == pcNumber)
            {
                if ((startTime >= reservation.StartTime && startTime < reservation.EndTime) ||
                    (endTime > reservation.StartTime && endTime <= reservation.EndTime) ||
                    (startTime <= reservation.StartTime && endTime >= reservation.EndTime))
                {
                    return false; // PC tidak tersedia
                }
            }
        }
        return true; // PC tersedia
    }

    // Fungsi untuk mencetak daftar PC yang tersedia
    public void PrintAvailablePCList()
    {
        Console.WriteLine("Daftar PC Tersedia:");
        foreach (var pc in pcs)
        {
            if (!pc.IsReserved)
            {
                Console.WriteLine($"No PC: {pc.Number}, Spesifikasi: {pc.Specification}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ReservationManager manager = new ReservationManager();

        // Menambahkan beberapa PC
        manager.AddPC(1, "Specs1");
        manager.AddPC(2, "Specs2");
        manager.AddPC(3, "Specs3");

        // Menampilkan daftar PC yang tersedia
        manager.PrintAvailablePCList();

        // Inputan pelanggan
        Console.WriteLine("Masukkan nomor PC yang ingin Anda reservasi:");
        int pcNumber = int.Parse(Console.ReadLine());

        Console.WriteLine("Masukkan waktu mulai reservasi (yyyy-MM-dd HH:mm):");
        DateTime startTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);

        Console.WriteLine("Masukkan waktu selesai reservasi (yyyy-MM-dd HH:mm):");
        DateTime endTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);

        // Menambahkan reservasi
        manager.AddReservation(pcNumber, startTime, endTime);
    }
}
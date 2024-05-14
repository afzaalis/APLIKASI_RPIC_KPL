using System;
using System.Collections.Generic;

// States for PC and Reservation
public enum ReservationState
{
    Available,
    Reserved,
    InUse,
    Completed
}

// Struktur data untuk merepresentasikan PC
public class PC
{
    public int Number { get; set; }
    public string Specification { get; set; }
    public ReservationState State { get; set; }
}

// Struktur data untuk merepresentasikan reservasi
public class Reservation
{
    public int PCNumber { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ReservationState State { get; set; }
}

// Reservation rule table
public class ReservationRule
{
    public string PCSpecification { get; set; }
    public TimeSpan MaxReservationTime { get; set; }
}

// Kelas utama untuk manajemen reservasi PC
public class ReservationManager
{
    private List<PC> pcs;
    private List<Reservation> reservations;
    private List<ReservationRule> rules;

    public ReservationManager()
    {
        pcs = new List<PC>();
        reservations = new List<Reservation>();
        rules = new List<ReservationRule>();
    }

    // Fungsi untuk menambahkan aturan reservasi
    public void AddRule(string pcSpecification, TimeSpan maxReservationTime)
    {
        rules.Add(new ReservationRule { PCSpecification = pcSpecification, MaxReservationTime = maxReservationTime });
    }

    // Fungsi untuk menambahkan PC baru
    public void AddPC(int number, string specification)
    {
        pcs.Add(new PC { Number = number, Specification = specification, State = ReservationState.Available });
    }

    // Fungsi untuk menambahkan reservasi
    public bool AddReservation(int pcNumber, DateTime startTime, DateTime endTime)
    {
        var pc = pcs.Find(p => p.Number == pcNumber);
        if (pc == null || pc.State != ReservationState.Available)
        {
            Console.WriteLine("PC tidak tersedia untuk reservasi.");
            return false;
        }

        var rule = rules.Find(r => r.PCSpecification == pc.Specification);
        if (rule != null && (endTime - startTime) > rule.MaxReservationTime)
        {
            Console.WriteLine($"PC dengan spesifikasi {pc.Specification} hanya dapat direservasi selama {rule.MaxReservationTime.TotalHours} jam.");
            return false;
        }

        // Tandai PC sebagai terpesan
        pc.State = ReservationState.Reserved;
        reservations.Add(new Reservation { PCNumber = pcNumber, StartTime = startTime, EndTime = endTime, State = ReservationState.Reserved });
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

    // Fungsi untuk memperbarui state reservasi
    public void UpdateState()
    {
        DateTime now = DateTime.Now;
        foreach (var reservation in reservations)
        {
            if (reservation.State == ReservationState.Reserved && reservation.StartTime <= now && reservation.EndTime > now)
            {
                reservation.State = ReservationState.InUse;
                pcs.Find(p => p.Number == reservation.PCNumber).State = ReservationState.InUse;
            }
            else if (reservation.State == ReservationState.InUse && reservation.EndTime <= now)
            {
                reservation.State = ReservationState.Completed;
                pcs.Find(p => p.Number == reservation.PCNumber).State = ReservationState.Available;
            }
        }
    }

    // Fungsi untuk mencetak daftar PC yang tersedia
    public void PrintAvailablePCList()
    {
        Console.WriteLine("Daftar PC Tersedia:");
        foreach (var pc in pcs)
        {
            if (pc.State == ReservationState.Available)
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

        // Menambahkan aturan reservasi
        manager.AddRule("Specs1", TimeSpan.FromHours(2));
        manager.AddRule("Specs2", TimeSpan.FromHours(3));
        manager.AddRule("Specs3", TimeSpan.FromHours(4));

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
        if (manager.AddReservation(pcNumber, startTime, endTime))
        {
            manager.UpdateState();
        }

        // Menampilkan daftar PC yang tersedia setelah reservasi
        manager.PrintAvailablePCList();
    }
}

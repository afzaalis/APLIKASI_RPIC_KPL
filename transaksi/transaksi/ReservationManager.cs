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
    private TransactionManager transactionManager;

    public ReservationManager(TransactionManager transactionManager)
    {
        pcs = new List<PC>();
        reservations = new List<Reservation>();
        this.transactionManager = transactionManager;
    }

    // Metode lainnya sama seperti sebelumnya...

    public bool AddTransaction(int pcNumber, DateTime startTime, DateTime endTime)
    {
        if (!IsPCAvailable(pcNumber, startTime, endTime))
        {
            Console.WriteLine("PC tidak tersedia pada waktu yang diminta.");
            return false;
        }

        transactionManager.AddTransaction(pcNumber, startTime, endTime);
        Console.WriteLine("Transaksi berhasil ditambahkan.");
        return true;
    }
}

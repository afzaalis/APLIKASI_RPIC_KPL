public class Transaction
{
    public int TransactionId { get; set; }
    public int PCNumber { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsCompleted { get; set; }
}

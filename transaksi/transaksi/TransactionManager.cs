using System.Collections.Generic;

public class TransactionManager
{
    private List<Transaction> transactions;
    private ReservationManager reservationManager;

    public TransactionManager(ReservationManager reservationManager)
    {
        transactions = new List<Transaction>();
        this.reservationManager = reservationManager;
    }

    public void AddTransaction(int pcNumber, DateTime startTime, DateTime endTime)
    {
        var transactionId = transactions.Count + 1;
        transactions.Add(new Transaction { TransactionId = transactionId, PCNumber = pcNumber, StartTime = startTime, EndTime = endTime, IsCompleted = false });
    }

    public void CompleteTransaction(int transactionId)
    {
        var transaction = transactions.Find(t => t.TransactionId == transactionId);
        if (transaction != null)
        {
            transaction.IsCompleted = true;
            reservationManager.AddReservation(transaction.PCNumber, transaction.StartTime, transaction.EndTime);
        }
    }
}

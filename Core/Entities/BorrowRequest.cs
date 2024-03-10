namespace Grejjer.Core;

public class BorrowRequest
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Borrower { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ReplyDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public BorrowRequest(int itemId, string borrower)
    {
        ItemId = itemId;
        Borrower = borrower;
        Status = RequestStatus.Pending;
        RequestDate = DateTime.Now;
    }
}

public enum RequestStatus
{
    Pending,
    Accepted,
    Delivered,
    Rejected,
    Returned,
    Cancelled
}
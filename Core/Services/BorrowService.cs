using Microsoft.EntityFrameworkCore;
using Grejjer.Infrastructure;

namespace Grejjer.Core;

public class BorrowService
{
    private readonly GrejjerDbContext _dbContext;

    public BorrowService(GrejjerDbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    //ITEM LIST PAGE
    public async Task<Dictionary<Item, bool>> GetAllItemsWithStatus()
    {
        var items = await _dbContext.Items.ToListAsync();

        var itemsWithStatus = new Dictionary<Item, bool>();

        foreach (var item in items)
        {
            itemsWithStatus.Add(item, await IsItemAvailableAsync(item.Id));
        }
        return itemsWithStatus;
    }

    //ITEM DETAILS PAGE
    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _dbContext.Items.FindAsync(id);
    }

    public async Task<bool> IsItemAvailableAsync(int id)
    {
        bool b = await _dbContext.BorrowRequests.Where(r => r.ItemId == id &&
                                                             (r.Status == RequestStatus.Accepted ||
                                                             r.Status == RequestStatus.Delivered)).AnyAsync();
        //If its true that an accepted or delivered request exists, the item is _not_ available,
        //so we return the opposite of b which is !b
        return !b;
    }

    public async Task<BorrowRequest> CreateBorrowRequestAsync(string _name, int itemId)
    {
        string name = _name.Trim().ToLower();
        var request = new BorrowRequest(itemId, name);

        //Check if borrowrequest already exists
        //TODO: Also set to rejected if the request is Accepted or Delivered
        if (await _dbContext.BorrowRequests.Where(r => r.ItemId == itemId && r.Borrower == name && r.Status == RequestStatus.Pending).AnyAsync())
        {
            request.Status = RequestStatus.Rejected;
        }

        _dbContext.BorrowRequests.Add(request);
        await _dbContext.SaveChangesAsync();
        return request;
    }

    // ADMIN FUNCTIONS
    public async Task AcceptRequestAsync(int id)
    {
        BorrowRequest? bs = await _dbContext.BorrowRequests.FindAsync(id) ?? throw new ArgumentException("No request with that id");
        bs.Status = RequestStatus.Accepted;
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetRequestStatus(int id, RequestStatus status)
    {
        BorrowRequest? bs = await _dbContext.BorrowRequests.FindAsync(id) ?? throw new ArgumentException("No request with that id");
        bs.Status = status;
        await _dbContext.SaveChangesAsync();
    }

    // List<Item> GetPendingRequests()
    // AcceptRequest(id)
    // RejectRequest(id)

    // List<Item> GetLoanedItems()
    // ReturnItem(id)
}
using Microsoft.EntityFrameworkCore;
using Grejjer.Core;

namespace Grejjer.Infrastructure;

public class GrejjerDbContext : DbContext
{
    public GrejjerDbContext(DbContextOptions<GrejjerDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<BorrowRequest> BorrowRequests { get; set; }
}
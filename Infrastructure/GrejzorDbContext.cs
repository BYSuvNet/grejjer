using Microsoft.EntityFrameworkCore;
using Grejzor.Core;

namespace Grejzor.Infrastructure;

public class GrejzorDbContext : DbContext
{
    public GrejzorDbContext(DbContextOptions<GrejzorDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<BorrowRequest> BorrowRequests { get; set; }
}
using Microsoft.EntityFrameworkCore;
using Grejzor.Core;

namespace Grejzor.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GrejzorDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<GrejzorDbContext>>()))
        {
            context.Items.AddRange(
                new Item
                {
                    Id = 1,
                    Name = "Hammare",
                    Description = "En hammare"
                },
                new Item
                {
                    Id = 2,
                    Name = "Skruvmejsel",
                    Description = "En skruvmejsel"
                },
                new Item
                {
                    Id = 3,
                    Name = "Cykel",
                    Description = "En blå cykel"
                },
                new Item
                {
                    Id = 4,
                    Name = "Skruvmejsel",
                    Description = "En skruvmejsel"
                }
            );
            context.SaveChanges();
        }
    }
}
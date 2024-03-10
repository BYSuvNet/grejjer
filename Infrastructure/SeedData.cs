using Microsoft.EntityFrameworkCore;
using Grejjer.Core;

namespace Grejjer.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GrejjerDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<GrejjerDbContext>>()))
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
using Grejjer.Components;
using Grejjer.Infrastructure;
using Grejjer.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddDbContext<GrejjerDbContext>(options => options.UseInMemoryDatabase("GrejjerDb"));
builder.Services.AddScoped<BorrowService>();

var app = builder.Build();

// FIXME Remove this when we have a proper database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.MapGet("/api/items/{id:int}", async (BorrowService borrowService, int id) => await borrowService.GetItemByIdAsync(id));
app.MapGet("/api/requests", async (GrejjerDbContext db) => await db.BorrowRequests.ToListAsync());
app.MapPost("/api/requests/{id:int}/accept", async (BorrowService borrowService, int id) => await borrowService.AcceptRequestAsync(id));

app.Run();

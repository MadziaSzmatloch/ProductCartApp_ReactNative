using Cart.Application;
using Cart.Infrastructure;
using Cart.Infrastructure.Seeders;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
    //await ItemsSeeder.SeedItemsAsync(store);
    await UsersSeeder.SeedUsersAsync(store);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapGet("/", () => "Cart");

app.UseAuthorization();

app.MapControllers();

app.Run();

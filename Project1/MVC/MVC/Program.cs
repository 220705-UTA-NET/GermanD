using MVC.Services;

namespace MVC;

public static class Program
{
    public static void Main(string[] args)
    {
        //        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES", false);

       
        var shopService = new ShopService(new HttpClient()); // create a shop service instance using the HttpClient class

        var incomingOrder = new OrderTaker(shopService) // create an order taker instance using the shop service
            .TakeOrder(); // take the order

        Console.WriteLine(shopService.PlaceOrder(incomingOrder)); // prints out the order
    }
}





/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); */

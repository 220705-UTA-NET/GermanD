using API.DTOs;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args); // create builder

// Dependency Injection (DI) & Inversion of Control (IoC)
builder.Services.AddScoped<IItemRepository, ItemRepository>(); // add item repository
builder.Services.AddScoped<IMenuRepository, MenuRepository>(); // add menu repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); // add order repository
builder.Services.AddScoped<IOrderService, OrderService>(); // add order service



var app = builder.Build(); // build app


// The minimal APIs of DotNet now allow to cut down on some boilerplate, including controller actions
// The MapXYZ methods *are* what controllers do
app.MapGet("/menus", ([FromServices] IMenuRepository menuRepository) => // map get menu list
    menuRepository.GetAllAsync()); 

app.MapGet("/orders/{id:guid}",
    ([FromServices] IOrderService orderService, [FromRoute] Guid id) => // map get order by id
        orderService.GetOrderById(id));

app.MapPost("/orders", ([FromServices] IOrderService orderService, [FromBody] IncomingOrder incomingOrder) => // map post order
    orderService.TakeOrderAsync(incomingOrder));


// Some conventions about HTTP Methods
// GET should not have a body
// POST should have a body
// DELETE can have a body, and preferably no response body
// PUT should have a body

app.Run(); // run app






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

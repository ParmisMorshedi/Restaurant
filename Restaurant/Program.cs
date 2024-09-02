using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Data.Repositories;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Services;
using Restaurant.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestaurantContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<ITableRepo, TableRepo>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepo, ReservationRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepo, MenuRepo>();




builder.Services.AddControllers();
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

app.Run();

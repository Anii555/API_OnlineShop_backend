using Amazon.ElasticMapReduce.Model;
using API_OnlineShop_backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); //настройки запросов с фронта
                      });
});

builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("NorthwindDatabase"));
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Внедрение БД 
builder.Services.AddDbContextPool<NorthwindContext>(options => options.UseSqlServer("Server=LAPTOP-B01QGVSN\\MSSQLSERVER03;Database=Northwind;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

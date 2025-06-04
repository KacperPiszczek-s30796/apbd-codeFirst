using CodeFirstAproach;
using CodeFirstAproach.DAL;
using CodeFirstAproach.middleware;
using CodeFirstAproach.Repositories.extensions;
using CodeFirstAproach.Services.extentions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructureServices();
builder.Services.AddInfrastructureRepositories();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<DbContext1>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=APBD_31_05_2025;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
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
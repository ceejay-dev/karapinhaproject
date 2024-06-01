using Karapinha.DAL;
using Karapinha.DAL.Repositories;
using Karapinha.Services;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with migrations assembly configuration
builder.Services.AddDbContext<KarapinhaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repository and service dependencies
builder.Services.AddTransient<IUtilizadorRepository, UtilizadorRepository>();
builder.Services.AddTransient<IUtilizadorService, UtilizadorService>();

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

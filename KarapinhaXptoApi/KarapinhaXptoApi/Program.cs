using Karapinha.DAL;
using Karapinha.DAL.Repositories;
using Karapinha.Services;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Services para o container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext com a configuração de assemby migration.
builder.Services.AddDbContext<KarapinhaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injecção de dependências.
builder.Services.AddTransient<IUtilizadorRepository, UtilizadorRepository>();
builder.Services.AddTransient<IUtilizadorService, UtilizadorService>();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();

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

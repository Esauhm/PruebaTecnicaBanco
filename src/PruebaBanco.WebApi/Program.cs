using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Data;
using PruebaBanco.WebApi.Managers.Tarjeta;
using PruebaBanco.WebApi.Managers.Transacciones;
using PruebaBanco.WebApi.Mapping;
using PruebaBanco.WebApi.Repository.Tarjeta;
using PruebaBanco.WebApi.Repository.Transacciones;

var builder = WebApplication.CreateBuilder(args);
const string namePolicy = "AllowOrigin";

// Add services to the container.

// DB CONFIGURATION
builder.Services.AddDbContext<BancoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("devConnection"));
});

//Repositorios
#region Repositori
    builder.Services.AddScoped<ITarjetaRepository, TarjetaRepository>();
    builder.Services.AddScoped<ITransaccionesRepository, TransaccionesRepository>();
#endregion

//Manager
#region Manager
    builder.Services.AddScoped<ITarjetaManager, TarjetaManager>();
    builder.Services.AddScoped<ITransaccionesManager, TransaccionesManager>();
#endregion

// Inyectar Context
builder.Services.AddScoped<IBancoDbContext, BancoDbContext>();

//Mapper
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(namePolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(namePolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

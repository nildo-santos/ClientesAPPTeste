using Clientes.Application.Interfaces;
using Clientes.Application.Mappers;
using Clientes.Application.Services;
using Clientes.Domain.Interfaces.Repositorio;
using Clientes.Infra.Data.Contexts;
using Clientes.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<DataContext>(options =>
{
  
    options.UseInMemoryDatabase("ClientesDb");
});
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddAutoMapper(p => p.AddProfile(typeof(MappingProfile)));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
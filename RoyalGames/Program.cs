using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalGames.Aplications.Services;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RoyalGamesContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Altereção produto
builder.Services.AddScoped<ILogAlteracaoProdutoRepository, LogAlteracaoProdutoRepository>();
builder.Services.AddScoped<LogAlteracaoProdutoService>();


// Configura o sistema de autenticação da aplicação.
// Aqui estamos dizendo que o tipo de autenticação padrão será JWT Bearer.
// Ou seja: a API vai esperar receber um Token JWT nas requisições.

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
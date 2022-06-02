using FlightStatusApp.Config;
using FlightStatusApp.Context;
using FlightStatusApp.Repositories;
using FlightStatusApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FlightContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("PGConnection")));
builder.AddJwtConfiguration();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<IRepository, FlightRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<AuthorizationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First ());
});

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
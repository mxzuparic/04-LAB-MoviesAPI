using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Data;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext") ?? throw new InvalidOperationException("Connection" +
"string MoviesContext not found.")));
builder.Services.AddScoped<MoviesService>();
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

app.Run();

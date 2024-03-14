using GymSpot.API.Data;
using GymSpot.API.Mappings;
using GymSpot.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GymSpotDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymSpotAPIConnectionString"));
});

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

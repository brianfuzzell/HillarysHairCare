using HillarysHairCare.Models;
using HillarysHairCare.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HillarysHairDbContext>(builder.Configuration["HillarysHairDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/stylists", (HillarysHairDbContext db) =>
{
    return db.Stylists
        .Select(s => new StylistDTO
        {
            Id = s.Id,
            Name = s.Name,
            isActive = s.isActive
        })
        .ToList();
});

app.MapPost("/api/stylists", (HillarysHairDbContext db, StylistDTO stylistDTO) =>
{
    var stylist = new Stylist
    {
        Name = stylistDTO.Name,
        isActive = true
    };

    db.Stylists.Add(stylist);
    db.SaveChanges();

    return Results.Created($"/api/stylists/{stylist.Id}", new StylistDTO
    {
        Id = stylist.Id,
        Name = stylist.Name,
        isActive = stylist.isActive
    });
});

app.MapGet("/api/services", (HillarysHairDbContext db) =>
{
    return db.Services
        .Select(s => new ServiceDTO
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Price = s.Price
        })
        .ToList();
});

app.MapPost("/api/services", (HillarysHairDbContext db, ServiceDTO serviceDTO) =>
{
    var service = new Service
    {
        Name = serviceDTO.Name,
        Description = serviceDTO.Description,
        Price = serviceDTO.Price
    };

    db.Services.Add(service);
    db.SaveChanges();

    return Results.Created($"/api/services/{service.Id}", new ServiceDTO
    {
        Id = service.Id,
        Name = service.Name,
        Description = service.Description,
        Price = service.Price
    });
});

app.Run();

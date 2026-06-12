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

app.MapGet("/api/appointments", (HillarysHairDbContext db) =>
{
    return db.Appointments
        .Include(a => a.Customer)
        .Include(a => a.Stylist)
        .Include(a => a.AppointmentServices)
            .ThenInclude(aps => aps.Service)
        .Select(a => new AppointmentDTO
        {
            Id = a.Id,
            CustomerId = a.CustomerId,
            Customer = new CustomerDTO
            {
                Id = a.Customer.Id,
                Name = a.Customer.Name,
                Email = a.Customer.Email,
                Phone = a.Customer.Phone
            },
            StylistId = a.StylistId,
            Stylist = new StylistDTO
            {
                Id = a.Stylist.Id,
                Name = a.Stylist.Name,
                isActive = a.Stylist.isActive
            },
            AppointmentTime = a.AppointmentTime,
            IsCancelled = a.IsCancelled,
            AppointmentServices = a.AppointmentServices
                .Select(aps => new AppointmentServiceDTO
                {
                    Id = aps.Id,
                    AppointmentId = aps.AppointmentId,
                    ServiceId = aps.ServiceId,
                    Service = new ServiceDTO
                    {
                        Id = aps.Service.Id,
                        Name = aps.Service.Name,
                        Description = aps.Service.Description,
                        Price = aps.Service.Price
                    }
                }).ToList(),
            TotalCost = a.AppointmentServices.Sum(aps => aps.Service.Price)
        })
        .ToList();
});

app.MapGet("/api/appointments/{id}", (HillarysHairDbContext db, int id) =>
{
    var appointment = db.Appointments
        .Include(a => a.Customer)
        .Include(a => a.Stylist)
        .Include(a => a.AppointmentServices)
            .ThenInclude(aps => aps.Service)
        .FirstOrDefault(a => a.Id == id);

    if (appointment == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new AppointmentDTO
    {
        Id = appointment.Id,
        CustomerId = appointment.CustomerId,
        Customer = new CustomerDTO
        {
            Id = appointment.Customer.Id,
            Name = appointment.Customer.Name,
            Email = appointment.Customer.Email,
            Phone = appointment.Customer.Phone
        },
        StylistId = appointment.StylistId,
        Stylist = new StylistDTO
        {
            Id = appointment.Stylist.Id,
            Name = appointment.Stylist.Name,
            isActive = appointment.Stylist.isActive
        },
        AppointmentTime = appointment.AppointmentTime,
        IsCancelled = appointment.IsCancelled,
        AppointmentServices = appointment.AppointmentServices
            .Select(aps => new AppointmentServiceDTO
            {
                Id = aps.Id,
                AppointmentId = aps.AppointmentId,
                ServiceId = aps.ServiceId,
                Service = new ServiceDTO
                {
                    Id = aps.Service.Id,
                    Name = aps.Service.Name,
                    Description = aps.Service.Description,
                    Price = aps.Service.Price
                }
            }).ToList(),
        TotalCost = appointment.AppointmentServices.Sum(aps => aps.Service.Price)
    });
});

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

app.MapPut("/api/stylists/{id}/deactivate", (HillarysHairDbContext db, int id) =>
{
    var stylist = db.Stylists.Find(id);
    if (stylist == null)
    {
        return Results.NotFound();
    }

    stylist.isActive = false;
    db.SaveChanges();

    return Results.NoContent();
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

app.MapGet("/api/customers", (HillarysHairDbContext db) =>
{
    return db.Customers
        .Select(c => new CustomerDTO
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        })
        .ToList();
});

app.MapPost("/api/customers", (HillarysHairDbContext db, CustomerDTO customerDTO) =>
{
    var customer = new Customer
    {
        Name = customerDTO.Name,
        Email = customerDTO.Email,
        Phone = customerDTO.Phone
    };

    db.Customers.Add(customer);
    db.SaveChanges();

    return Results.Created($"/api/customers/{customer.Id}", new CustomerDTO
    {
        Id = customer.Id,
        Name = customer.Name,
        Email = customer.Email,
        Phone = customer.Phone
    });
});

app.Run();

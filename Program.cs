using HillarysHairCare.Models;
using HillarysHairCare.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HillarysHairCareDbContext>(builder.Configuration["HillarysHairCareDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Appointment Endpoints

// GET all appointments
app.MapGet("/api/appointments", (HillarysHairCareDbContext db) =>
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
                FirstName = a.Customer.FirstName,
                LastName = a.Customer.LastName,
                Email = a.Customer.Email,
                PhoneNumber = a.Customer.PhoneNumber
            },
            StylistId = a.StylistId,
            Stylist = new StylistDTO
            {
                Id = a.Stylist.Id,
                FirstName = a.Stylist.FirstName,
                LastName = a.Stylist.LastName,
                IsActive = a.Stylist.IsActive
            },
            AppointmentDate = a.AppointmentDate,
            IsCanceled = a.IsCanceled,
            Services = a.AppointmentServices.Select(aps => new ServiceDTO
            {
                Id = aps.Service.Id,
                Name = aps.Service.Name,
                Description = aps.Service.Description,
                Price = aps.Service.Price
            }).ToList()
        })
        .ToList();
});

// GET appointment by ID
app.MapGet("/api/appointments/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var appointment = db.Appointments
        .Include(a => a.Customer)
        .Include(a => a.Stylist)
        .Include(a => a.AppointmentServices)
            .ThenInclude(aps => aps.Service)
        .Where(a => a.Id == id)
        .Select(a => new AppointmentDTO
        {
            Id = a.Id,
            CustomerId = a.CustomerId,
            Customer = new CustomerDTO
            {
                Id = a.Customer.Id,
                FirstName = a.Customer.FirstName,
                LastName = a.Customer.LastName,
                Email = a.Customer.Email,
                PhoneNumber = a.Customer.PhoneNumber
            },
            StylistId = a.StylistId,
            Stylist = new StylistDTO
            {
                Id = a.Stylist.Id,
                FirstName = a.Stylist.FirstName,
                LastName = a.Stylist.LastName,
                IsActive = a.Stylist.IsActive
            },
            AppointmentDate = a.AppointmentDate,
            IsCanceled = a.IsCanceled,
            Services = a.AppointmentServices.Select(aps => new ServiceDTO
            {
                Id = aps.Service.Id,
                Name = aps.Service.Name,
                Description = aps.Service.Description,
                Price = aps.Service.Price
            }).ToList()
        })
        .FirstOrDefault();

    if (appointment == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(appointment);
});

// Stylist Endpoints

// GET all stylists
app.MapGet("/api/stylists", (HillarysHairCareDbContext db) =>
{
    return db.Stylists
        .Select(s => new StylistDTO
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            IsActive = s.IsActive
        })
        .ToList();
});

// GET stylist by ID
app.MapGet("/api/stylists/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var stylist = db.Stylists
        .Include(s => s.Appointments)
            .ThenInclude(a => a.Customer)
        .Include(s => s.Appointments)
            .ThenInclude(a => a.AppointmentServices)
                .ThenInclude(aps => aps.Service)
        .Where(s => s.Id == id)
        .Select(s => new StylistDTO
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            IsActive = s.IsActive,
            Appointments = s.Appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                Customer = new CustomerDTO
                {
                    Id = a.Customer.Id,
                    FirstName = a.Customer.FirstName,
                    LastName = a.Customer.LastName,
                    Email = a.Customer.Email,
                    PhoneNumber = a.Customer.PhoneNumber
                },
                StylistId = a.StylistId,
                AppointmentDate = a.AppointmentDate,
                IsCanceled = a.IsCanceled,
                Services = a.AppointmentServices.Select(aps => new ServiceDTO
                {
                    Id = aps.Service.Id,
                    Name = aps.Service.Name,
                    Description = aps.Service.Description,
                    Price = aps.Service.Price
                }).ToList()
            }).ToList()
        })
        .FirstOrDefault();

    if (stylist == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(stylist);
});

// Customer Endpoints

// GET all customers
app.MapGet("/api/customers", (HillarysHairCareDbContext db) =>
{
    return db.Customers
        .Select(c => new CustomerDTO
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber
        })
        .ToList();
});

// GET customer by ID
app.MapGet("/api/customers/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var customer = db.Customers
        .Include(c => c.Appointments)
            .ThenInclude(a => a.Stylist)
        .Include(c => c.Appointments)
            .ThenInclude(a => a.AppointmentServices)
                .ThenInclude(aps => aps.Service)
        .Where(c => c.Id == id)
        .Select(c => new CustomerDTO
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Appointments = c.Appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                StylistId = a.StylistId,
                Stylist = new StylistDTO
                {
                    Id = a.Stylist.Id,
                    FirstName = a.Stylist.FirstName,
                    LastName = a.Stylist.LastName,
                    IsActive = a.Stylist.IsActive
                },
                AppointmentDate = a.AppointmentDate,
                IsCanceled = a.IsCanceled,
                Services = a.AppointmentServices.Select(aps => new ServiceDTO
                {
                    Id = aps.Service.Id,
                    Name = aps.Service.Name,
                    Description = aps.Service.Description,
                    Price = aps.Service.Price
                }).ToList()
            }).ToList()
        })
        .FirstOrDefault();

    if (customer == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(customer);
});

// Service Endpoints

// GET all services
app.MapGet("/api/services", (HillarysHairCareDbContext db) =>
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

// DELETE/UPDATE Endpoints

// Deactivate a stylist (PUT - sets IsActive to false)
app.MapPut("/api/stylists/{id}/deactivate", (HillarysHairCareDbContext db, int id) =>
{
    var stylist = db.Stylists.FirstOrDefault(s => s.Id == id);

    if (stylist == null)
    {
        return Results.NotFound();
    }

    stylist.IsActive = false;
    db.SaveChanges();

    return Results.NoContent();
});

// Reactivate a stylist (PUT - sets IsActive to true)
app.MapPut("/api/stylists/{id}/reactivate", (HillarysHairCareDbContext db, int id) =>
{
    var stylist = db.Stylists.FirstOrDefault(s => s.Id == id);

    if (stylist == null)
    {
        return Results.NotFound();
    }

    stylist.IsActive = true;
    db.SaveChanges();

    return Results.NoContent();
});

// Cancel an appointment (PUT - sets IsCanceled to true)
app.MapPut("/api/appointments/{id}/cancel", (HillarysHairCareDbContext db, int id) =>
{
    var appointment = db.Appointments.FirstOrDefault(a => a.Id == id);

    if (appointment == null)
    {
        return Results.NotFound();
    }

    appointment.IsCanceled = true;
    db.SaveChanges();

    return Results.NoContent();
});

// Delete an appointment (actually removes from database)
app.MapDelete("/api/appointments/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var appointment = db.Appointments
        .Include(a => a.AppointmentServices)
        .FirstOrDefault(a => a.Id == id);

    if (appointment == null)
    {
        return Results.NotFound();
    }

    // Remove associated AppointmentServices first (cascade delete)
    db.AppointmentServices.RemoveRange(appointment.AppointmentServices);
    db.Appointments.Remove(appointment);
    db.SaveChanges();

    return Results.NoContent();
});

// Delete a customer
app.MapDelete("/api/customers/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var customer = db.Customers
        .Include(c => c.Appointments)
            .ThenInclude(a => a.AppointmentServices)
        .FirstOrDefault(c => c.Id == id);

    if (customer == null)
    {
        return Results.NotFound();
    }

    // Remove all appointments and their services
    foreach (var appointment in customer.Appointments)
    {
        db.AppointmentServices.RemoveRange(appointment.AppointmentServices);
    }
    db.Appointments.RemoveRange(customer.Appointments);
    db.Customers.Remove(customer);
    db.SaveChanges();

    return Results.NoContent();
});

// Delete a service
app.MapDelete("/api/services/{id}", (HillarysHairCareDbContext db, int id) =>
{
    var service = db.Services
        .Include(s => s.AppointmentServices)
        .FirstOrDefault(s => s.Id == id);

    if (service == null)
    {
        return Results.NotFound();
    }

    // Remove associated AppointmentServices first
    db.AppointmentServices.RemoveRange(service.AppointmentServices);
    db.Services.Remove(service);
    db.SaveChanges();

    return Results.NoContent();
});

// POST (Create) Endpoints

// Create a new appointment
app.MapPost("/api/appointments", (HillarysHairCareDbContext db, Appointment appointment) =>
{
    // Validate that customer exists
    var customer = db.Customers.Find(appointment.CustomerId);
    if (customer == null)
    {
        return Results.BadRequest("Customer not found");
    }

    // Validate that stylist exists and is active
    var stylist = db.Stylists.Find(appointment.StylistId);
    if (stylist == null)
    {
        return Results.BadRequest("Stylist not found");
    }
    if (!stylist.IsActive)
    {
        return Results.BadRequest("Cannot book appointment with inactive stylist");
    }

    // Set default values
    appointment.IsCanceled = false;

    db.Appointments.Add(appointment);
    db.SaveChanges();

    return Results.Created($"/api/appointments/{appointment.Id}", appointment);
});

// Add services to an appointment
app.MapPost("/api/appointments/{appointmentId}/services", (HillarysHairCareDbContext db, int appointmentId, List<int> serviceIds) =>
{
    var appointment = db.Appointments.Find(appointmentId);
    if (appointment == null)
    {
        return Results.NotFound("Appointment not found");
    }

    // Validate all services exist
    foreach (var serviceId in serviceIds)
    {
        var service = db.Services.Find(serviceId);
        if (service == null)
        {
            return Results.BadRequest($"Service with ID {serviceId} not found");
        }

        // Add AppointmentService entry
        db.AppointmentServices.Add(new AppointmentService
        {
            AppointmentId = appointmentId,
            ServiceId = serviceId
        });
    }

    db.SaveChanges();

    return Results.Created($"/api/appointments/{appointmentId}", null);
});

// Create a new stylist
app.MapPost("/api/stylists", (HillarysHairCareDbContext db, Stylist stylist) =>
{
    // New stylists are active by default
    stylist.IsActive = true;

    db.Stylists.Add(stylist);
    db.SaveChanges();

    return Results.Created($"/api/stylists/{stylist.Id}", stylist);
});

// Create a new customer
app.MapPost("/api/customers", (HillarysHairCareDbContext db, Customer customer) =>
{
    db.Customers.Add(customer);
    db.SaveChanges();

    return Results.Created($"/api/customers/{customer.Id}", customer);
});

// Create a new service
app.MapPost("/api/services", (HillarysHairCareDbContext db, Service service) =>
{
    db.Services.Add(service);
    db.SaveChanges();

    return Results.Created($"/api/services/{service.Id}", service);
});

app.Run();
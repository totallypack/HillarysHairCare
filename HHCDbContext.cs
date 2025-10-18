using Microsoft.EntityFrameworkCore;
using HillarysHairCare.Models;

public class HillarysHairCareDbContext : DbContext
{
    public DbSet<Stylist> Stylists { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<AppointmentService> AppointmentServices { get; set; }

    public HillarysHairCareDbContext(DbContextOptions<HillarysHairCareDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for Stylists
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, FirstName = "Sarah", LastName = "Johnson", IsActive = true },
            new Stylist { Id = 2, FirstName = "Mike", LastName = "Chen", IsActive = true },
            new Stylist { Id = 3, FirstName = "Emily", LastName = "Rodriguez", IsActive = false },
            new Stylist { Id = 4, FirstName = "Jessica", LastName = "Martinez", IsActive = true },
            new Stylist { Id = 5, FirstName = "David", LastName = "Thompson", IsActive = true },
            new Stylist { Id = 6, FirstName = "Amanda", LastName = "Wilson", IsActive = true },
            new Stylist { Id = 7, FirstName = "Kevin", LastName = "Anderson", IsActive = true }
        });

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer { Id = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@example.com", PhoneNumber = "615-555-0101" },
            new Customer { Id = 2, FirstName = "Emma", LastName = "Davis", Email = "emma.davis@example.com", PhoneNumber = "615-555-0102" },
            new Customer { Id = 3, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@example.com", PhoneNumber = "615-555-0103" },
            new Customer { Id = 4, FirstName = "Sophia", LastName = "Garcia", Email = "sophia.garcia@example.com", PhoneNumber = "615-555-0104" },
            new Customer { Id = 5, FirstName = "William", LastName = "Miller", Email = "william.miller@example.com", PhoneNumber = "615-555-0105" },
            new Customer { Id = 6, FirstName = "Olivia", LastName = "Taylor", Email = "olivia.taylor@example.com", PhoneNumber = "615-555-0106" },
            new Customer { Id = 7, FirstName = "James", LastName = "Anderson", Email = "james.anderson@example.com", PhoneNumber = "615-555-0107" },
            new Customer { Id = 8, FirstName = "Isabella", LastName = "Thomas", Email = "isabella.thomas@example.com", PhoneNumber = "615-555-0108" },
            new Customer { Id = 9, FirstName = "Benjamin", LastName = "Jackson", Email = "benjamin.jackson@example.com", PhoneNumber = "615-555-0109" },
            new Customer { Id = 10, FirstName = "Mia", LastName = "White", Email = "mia.white@example.com", PhoneNumber = "615-555-0110" },
            new Customer { Id = 11, FirstName = "Lucas", LastName = "Harris", Email = "lucas.harris@example.com", PhoneNumber = "615-555-0111" },
            new Customer { Id = 12, FirstName = "Charlotte", LastName = "Martin", Email = "charlotte.martin@example.com", PhoneNumber = "615-555-0112" },
            new Customer { Id = 13, FirstName = "Henry", LastName = "Thompson", Email = "henry.thompson@example.com", PhoneNumber = "615-555-0113" },
            new Customer { Id = 14, FirstName = "Amelia", LastName = "Moore", Email = "amelia.moore@example.com", PhoneNumber = "615-555-0114" },
            new Customer { Id = 15, FirstName = "Alexander", LastName = "Lee", Email = "alexander.lee@example.com", PhoneNumber = "615-555-0115" }
        });

        // Seed data for Services
        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Name = "Haircut", Description = "Basic haircut and style", Price = 45.00M },
            new Service { Id = 2, Name = "Color", Description = "Full hair coloring service", Price = 120.00M },
            new Service { Id = 3, Name = "Highlights", Description = "Partial highlights", Price = 85.00M },
            new Service { Id = 4, Name = "Beard Trim", Description = "Beard shaping and trim", Price = 25.00M },
            new Service { Id = 5, Name = "Deep Conditioning", Description = "Deep conditioning treatment", Price = 35.00M }
        });

        // Seed data for Appointments
        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
            new Appointment { Id = 1, CustomerId = 1, StylistId = 1, AppointmentDate = new DateTime(2025, 10, 20, 10, 0, 0), IsCanceled = false },
            new Appointment { Id = 2, CustomerId = 2, StylistId = 2, AppointmentDate = new DateTime(2025, 10, 20, 14, 0, 0), IsCanceled = false },
            new Appointment { Id = 3, CustomerId = 3, StylistId = 1, AppointmentDate = new DateTime(2025, 10, 21, 11, 0, 0), IsCanceled = true },
            new Appointment { Id = 4, CustomerId = 4, StylistId = 4, AppointmentDate = new DateTime(2025, 10, 22, 9, 0, 0), IsCanceled = false },
            new Appointment { Id = 5, CustomerId = 5, StylistId = 5, AppointmentDate = new DateTime(2025, 10, 22, 13, 0, 0), IsCanceled = false },
            new Appointment { Id = 6, CustomerId = 6, StylistId = 6, AppointmentDate = new DateTime(2025, 10, 23, 10, 0, 0), IsCanceled = false },
            new Appointment { Id = 7, CustomerId = 7, StylistId = 7, AppointmentDate = new DateTime(2025, 10, 23, 15, 0, 0), IsCanceled = false },
            new Appointment { Id = 8, CustomerId = 8, StylistId = 1, AppointmentDate = new DateTime(2025, 10, 24, 11, 0, 0), IsCanceled = true }
        });

        // Seed data for AppointmentServices
        modelBuilder.Entity<AppointmentService>().HasData(new AppointmentService[]
        {
            // Appointment 1: John - Haircut + Beard Trim
            new AppointmentService { Id = 1, AppointmentId = 1, ServiceId = 1 },
            new AppointmentService { Id = 2, AppointmentId = 1, ServiceId = 4 },

            // Appointment 2: Emma - Color + Deep Conditioning
            new AppointmentService { Id = 3, AppointmentId = 2, ServiceId = 2 },
            new AppointmentService { Id = 4, AppointmentId = 2, ServiceId = 5 },

            // Appointment 3: Michael - Haircut (canceled)
            new AppointmentService { Id = 5, AppointmentId = 3, ServiceId = 1 },

            // Appointment 4: Sophia - Highlights + Deep Conditioning
            new AppointmentService { Id = 6, AppointmentId = 4, ServiceId = 3 },
            new AppointmentService { Id = 7, AppointmentId = 4, ServiceId = 5 },

            // Appointment 5: William - Haircut + Beard Trim
            new AppointmentService { Id = 8, AppointmentId = 5, ServiceId = 1 },
            new AppointmentService { Id = 9, AppointmentId = 5, ServiceId = 4 },

            // Appointment 6: Olivia - Color
            new AppointmentService { Id = 10, AppointmentId = 6, ServiceId = 2 },

            // Appointment 7: James - Haircut
            new AppointmentService { Id = 11, AppointmentId = 7, ServiceId = 1 },

            // Appointment 8: Isabella - Highlights + Color (canceled)
            new AppointmentService { Id = 12, AppointmentId = 8, ServiceId = 2 },
            new AppointmentService { Id = 13, AppointmentId = 8, ServiceId = 3 }
        });
    }
}
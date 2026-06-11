using Microsoft.EntityFrameworkCore;
using HillarysHairCare.Models;

public class HillarysHairDbContext : DbContext
{
    public DbSet<Appointment> Checkouts { get; set; }
    public DbSet<AppointmentService> Genres { get; set; }
    public DbSet<Customer> Materials { get; set; }
    public DbSet<Service> MaterialTypes { get; set; }
    public DbSet<Stylist> Patrons { get; set; }

    public HillarysHairDbContext(DbContextOptions<HillarysHairDbContext> context) : base(context)
    {

    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, Name = "Marge Simpson",    isActive = true  },
            new Stylist { Id = 2, Name = "Selma Bouvier",    isActive = true  },
            new Stylist { Id = 3, Name = "Patty Bouvier",    isActive = true  },
            new Stylist { Id = 4, Name = "Lindsey Naegle",   isActive = true  },
            new Stylist { Id = 5, Name = "Helen Lovejoy",    isActive = true  },
            // Inactive — booked appointments before they left
            new Stylist { Id = 6, Name = "Sideshow Bob",     isActive = false },
            new Stylist { Id = 7, Name = "Snake Jailbird",   isActive = false },
        });

        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer { Id = 1,  Name = "Homer Simpson",             Email = "homer@springfield.net",  Phone = "555-0101" },
            new Customer { Id = 2,  Name = "Bart Simpson",              Email = "bart@springfield.net",   Phone = "555-0102" },
            new Customer { Id = 3,  Name = "Lisa Simpson",              Email = "lisa@springfield.net",   Phone = "555-0103" },
            new Customer { Id = 4,  Name = "Ned Flanders",              Email = "ned@leftorium.com",      Phone = "555-0104" },
            new Customer { Id = 5,  Name = "Montgomery Burns",          Email = "mburns@burnscorp.com",   Phone = "555-0105" },
            new Customer { Id = 6,  Name = "Apu Nahasapeemapetilon",    Email = "apu@kwikmart.com",       Phone = "555-0106" },
            new Customer { Id = 7,  Name = "Chief Clancy Wiggum",       Email = "cwiggum@spd.gov",        Phone = "555-0107" },
            new Customer { Id = 8,  Name = "Krusty the Clown",          Email = "krusty@krustyco.com",    Phone = "555-0108" },
            // Edge case: child customer
            new Customer { Id = 9,  Name = "Ralph Wiggum",              Email = "ralph@spd.gov",          Phone = "555-0109" },
            // Edge case: very elderly customer, minimal contact info
            new Customer { Id = 10, Name = "Hans Moleman",              Email = "",                       Phone = "555-0110" },
        });

        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Name = "Haircut",          Description = "Basic cut and trim",                       Price = 25.00m  },
            new Service { Id = 2, Name = "Color Treatment",  Description = "Full single-color treatment",              Price = 75.00m  },
            new Service { Id = 3, Name = "Perm",             Description = "Permanent wave styling",                   Price = 60.00m  },
            new Service { Id = 4, Name = "Highlights",       Description = "Partial or full highlights",               Price = 90.00m  },
            new Service { Id = 5, Name = "Blowout",          Description = "Wash and blowout style",                   Price = 35.00m  },
            new Service { Id = 6, Name = "Shampoo & Style",  Description = "Shampoo, condition, and style",            Price = 45.00m  },
            new Service { Id = 7, Name = "Beard Trim",       Description = "Beard shaping and trim",                   Price = 15.00m  },
            new Service { Id = 8, Name = "Kids Cut",         Description = "Haircut for ages 12 and under",            Price = 18.00m  },
            // Edge case: premium service with high price
            new Service { Id = 9, Name = "Full Makeover",    Description = "Color, highlights, cut, and style package", Price = 200.00m },
        });

        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
            // Standard completed appointments
            new Appointment { Id = 1,  CustomerId = 1,  StylistId = 1, AppointmentTime = new DateTime(2026, 5, 10, 10, 0, 0), IsCancelled = false },
            new Appointment { Id = 2,  CustomerId = 2,  StylistId = 2, AppointmentTime = new DateTime(2026, 5, 16, 14, 0, 0), IsCancelled = false },
            new Appointment { Id = 3,  CustomerId = 3,  StylistId = 1, AppointmentTime = new DateTime(2026, 6,  1, 13, 0, 0), IsCancelled = false },
            new Appointment { Id = 4,  CustomerId = 5,  StylistId = 4, AppointmentTime = new DateTime(2026, 6,  5,  9, 0, 0), IsCancelled = false },
            new Appointment { Id = 5,  CustomerId = 6,  StylistId = 5, AppointmentTime = new DateTime(2026, 6, 18, 10, 30, 0), IsCancelled = false },
            new Appointment { Id = 6,  CustomerId = 9,  StylistId = 1, AppointmentTime = new DateTime(2026, 6, 25, 11, 0, 0), IsCancelled = false },
            // Cancelled appointments
            new Appointment { Id = 7,  CustomerId = 4,  StylistId = 3, AppointmentTime = new DateTime(2026, 5, 20, 11, 0, 0), IsCancelled = true  },
            new Appointment { Id = 8,  CustomerId = 1,  StylistId = 2, AppointmentTime = new DateTime(2026, 6, 10, 15, 0, 0), IsCancelled = true  },
            // Edge case: appointment booked with a now-inactive stylist (before they left)
            new Appointment { Id = 9,  CustomerId = 8,  StylistId = 6, AppointmentTime = new DateTime(2026, 5, 28, 16, 0, 0), IsCancelled = false },
            // Edge case: appointment created but time not yet scheduled (null AppointmentTime)
            new Appointment { Id = 10, CustomerId = 10, StylistId = 3, AppointmentTime = null,                                IsCancelled = false },
        });

        modelBuilder.Entity<AppointmentService>().HasData(new AppointmentService[]
        {
            // Appointment 1 — Homer: Haircut + Beard Trim
            new AppointmentService { Id = 1,  AppointmentId = 1, ServiceId = 1 },
            new AppointmentService { Id = 2,  AppointmentId = 1, ServiceId = 7 },
            // Appointment 2 — Bart: Kids Cut (he's technically a kid)
            new AppointmentService { Id = 3,  AppointmentId = 2, ServiceId = 8 },
            // Appointment 3 — Lisa: Highlights
            new AppointmentService { Id = 4,  AppointmentId = 3, ServiceId = 4 },
            // Appointment 4 — Burns: Full Makeover (money is no object)
            new AppointmentService { Id = 5,  AppointmentId = 4, ServiceId = 9 },
            // Appointment 5 — Apu: Haircut
            new AppointmentService { Id = 6,  AppointmentId = 5, ServiceId = 1 },
            // Appointment 6 — Ralph: Kids Cut
            new AppointmentService { Id = 7,  AppointmentId = 6, ServiceId = 8 },
            // Appointment 7 (cancelled) — Ned: Shampoo & Style
            new AppointmentService { Id = 8,  AppointmentId = 7, ServiceId = 6 },
            // Appointment 8 (cancelled) — Homer: Haircut
            new AppointmentService { Id = 9,  AppointmentId = 8, ServiceId = 1 },
            // Appointment 9 — Krusty: Perm + Color Treatment (his signature look)
            new AppointmentService { Id = 10, AppointmentId = 9, ServiceId = 3 },
            new AppointmentService { Id = 11, AppointmentId = 9, ServiceId = 2 },
            // Appointment 10 — Hans Moleman: Haircut (if time ever gets scheduled)
            new AppointmentService { Id = 12, AppointmentId = 10, ServiceId = 1 },
        });
    }
}
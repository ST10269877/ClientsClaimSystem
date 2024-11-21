using ClientsClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsClaimSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure precision for HourlyRate
            modelBuilder.Entity<Claim>()
                .Property(c => c.HourlyRate)
                .HasColumnType("decimal(18, 2)");

            // Seed data for Lecturer entity
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer
                {
                    LecturerID = 1,
                    Username = "lecturer",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),  // Set a default password
                    Email = "lecturer@example.com"
                }
            );

            // Seed data for Admin entity
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminID = 1,
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123")  // Set a default password
                }
            );
        }
    }
}

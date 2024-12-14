using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;
namespace EventPlanner.Data
{
    public class EventplannerContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gathering> Gatherings { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = @"Data Source=.;Initial Catalog=EventPlanner;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.CategoryName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(c => c.Gatherings)
                      .WithOne(g => g.GatheringCategory)
                      .HasForeignKey(g => g.GatheringId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Gathering>(entity =>
            {
                entity.HasKey(g => g.GatheringId);
                entity.Property(g => g.GatheringName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(g => g.Registrations)
                      .WithOne(r => r.RegistrationGathering)
                      .HasForeignKey(r => r.RegistrationId)
                      .OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(r => r.RegistrationId);

                entity.HasOne(r => r.RegistrationOwner)
                      .WithMany(u => u.Registrations)
                      .HasForeignKey(u => u.RegistrationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Suggestion>(entity =>
            {
                entity.HasKey(s => s.SuggestionId);

                entity.Property(s => s.SuggestionName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasOne(s => s.SuggestionOwner)
                      .WithMany(u => u.Suggestions)
                      .HasForeignKey(u => u.SuggestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                //var userRole = new Role() { Id = 1, Name = "User" };
                //var adminRole = new Role() { Id = 2, Name = "Admin" };

                //var defaultUser = new User()
                //{
                //    UserId = 1,
                //    UserName = "DefaultUser",
                //    UserPassword = "123456",
                //    UserAge = 18,
                //    RoleId = 1                    
                //};

                //var defaultAdmin = new User()
                //{
                //    UserId = 2,
                //    UserName = "DefaultAdmin",
                //    UserPassword = "password123",
                //    UserAge = 17,
                //    RoleId = 2
                //};

                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(u => u.Suggestions)
                      .WithOne(s => s.SuggestionOwner)
                      .HasForeignKey(s => s.SuggestionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Registrations)
                      .WithOne(r => r.RegistrationOwner)
                      .HasForeignKey(r => r.RegistrationId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "User" },
                new Role() { Id = 2, Name = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    UserName = "DefaultUser",
                    UserPassword = "123456",
                    UserAge = 18,
                    UserRole = 1
                },
                new User()
                {
                    UserId = 2,
                    UserName = "DefaultAdmin",
                    UserPassword = "password123",
                    UserAge = 17,
                    UserRole = 2
                });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(ro => ro.Id);

                entity.Property(ro => ro.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasMany(ro => ro.UsersWithRole)
                      .WithOne(u => u.Role)
                      .HasForeignKey(u => u.UserRole)
                      .OnDelete(DeleteBehavior.Restrict);
            });   


            




        }
    }
}

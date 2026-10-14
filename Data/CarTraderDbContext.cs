using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarTrader.Data.Models;
using CarTrader.Data.Enums;
using Microsoft.AspNetCore.Identity;
// Identity seed constants are provided inline below for the default admin user.

namespace CarTrader.Data
{
    public class CarTraderDbContext : IdentityDbContext
    {
        public CarTraderDbContext(DbContextOptions<CarTraderDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<UserVehicle> UserVehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserVehicle>()
                .HasKey(uv => new { uv.VehicleId, uv.UserId });

            builder.Entity<UserVehicle>()
                .HasOne(uv => uv.Vehicle)
                .WithMany(v => v.UserVehicles)
                .HasForeignKey(uv => uv.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserVehicle>()
                .HasOne(uv => uv.User)
                .WithMany()
                .HasForeignKey(uv => uv.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vehicle>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<IdentityUser>().HasData(new IdentityUser
            //{
            //    Id = DefaultUserId,
            //    UserName = DefaultUserEmail,
            //    NormalizedUserName = DefaultUserEmail.ToUpperInvariant(),
            //    Email = DefaultUserEmail,
            //    NormalizedEmail = DefaultUserEmail.ToUpperInvariant(),
            //    EmailConfirmed = true,
            //    PasswordHash = DefaultUserPasswordHash,
            //    SecurityStamp = DefaultSecurityStamp,
            //    ConcurrencyStamp = DefaultConcurrencyStamp
            //});

            // Seed a default admin user inline (email: admin@cartrader.com, password: Admin123!)
            // Using fixed identifiers so EF Core migrations remain stable.
            var adminId = "2a1b3c4d-5e6f-4789-abcd-0123456789ab";
            var adminUser = new IdentityUser
            {
                Id = adminId,
                UserName = "admin@cartrader.com",
                NormalizedUserName = "ADMIN@CARTRADER.COM",
                Email = "admin@cartrader.com",
                NormalizedEmail = "ADMIN@CARTRADER.COM",
                EmailConfirmed = true,
                SecurityStamp = "a1b2c3d4-e5f6-4789-abcd-000000000001",
                ConcurrencyStamp = "c1d2e3f4-1234-4789-abcd-000000000002"
            };

            // Compute password hash for the seeded user
            var hasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<Category>().HasData
            (
                new Category { Id = 1, Name = "Sedan" },
                new Category { Id = 2, Name = "SUV" },
                new Category { Id = 3, Name = "Truck" },
                new Category { Id = 4, Name = "Coupe" },
                new Category { Id = 5, Name = "Convertible" }
            );

            builder.Entity<Vehicle>().HasData
            (
                new Vehicle
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Camry",
                    Description = "Reliable midsize sedan with good fuel economy.",
                    Year = 2020,
                    Price = 24000,
                    Mileage = 25000,
                    EngineSize = 2500,
                    Doors = DoorCount.Four,
                    FuelType = FuelType.Petrol,
                    TransmissionType = TransmissionType.Automatic,
                    Condition = VehicleCondition.Good,
                    ImageUrl = "https://media.ed.edmunds-media.com/toyota/camry/2020/oem/2020_toyota_camry_sedan_trd_fq_oem_1_500.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 1
                },
                new Vehicle
                {
                    Id = 2,
                    Make = "Honda",
                    Model = "Civic",
                    Description = "Compact car with a sporty feel and low running costs.",
                    Year = 2019,
                    Price = 20000,
                    Mileage = 30000,
                    EngineSize = 2000,
                    Doors = DoorCount.Four,
                    FuelType = FuelType.Petrol,
                    TransmissionType = TransmissionType.Manual,
                    Condition = VehicleCondition.Excellent,
                    ImageUrl = "https://media.ed.edmunds-media.com/honda/civic/2019/oem/2019_honda_civic_4dr-hatchback_sport-touring_fq_oem_6_500.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 1
                },
                new Vehicle
                {
                    Id = 3,
                    Make = "Ford",
                    Model = "F-150",
                    Description = "Powerful pickup truck suitable for work and towing.",
                    Year = 2021,
                    Price = 35000,
                    Mileage = 15000,
                    EngineSize = 3500,
                    Doors = DoorCount.Four,
                    FuelType = FuelType.Gas,
                    TransmissionType = TransmissionType.Automatic,
                    Condition = VehicleCondition.LikeNew,
                    ImageUrl = "https://media.ed.edmunds-media.com/ford/f-150/2021/oem/2021_ford_f-150_crew-cab-pickup_raptor_fq_oem_1_500.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 3
                },
                new Vehicle
                {
                    Id = 4,
                    Make = "Chevrolet",
                    Model = "Tahoe",
                    Description = "Full-size SUV with spacious interior and strong towing capacity.",
                    Year = 2020,
                    Price = 50000,
                    Mileage = 20000,
                    EngineSize = 5300,
                    Doors = DoorCount.Four,
                    FuelType = FuelType.Petrol,
                    TransmissionType = TransmissionType.Automatic,
                    Condition = VehicleCondition.Good,
                    ImageUrl = "https://media.ed.edmunds-media.com/chevrolet/tahoe/2026/oem/2026_chevrolet_tahoe_4dr-suv_z71_fq_oem_2_500.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 2
                },
                new Vehicle
                {
                    Id = 5,
                    Make = "BMW",
                    Model = "M4",
                    Description = "High-performance coupe with track-capable handling.",
                    Year = 2021,
                    Price = 70000,
                    Mileage = 10000,
                    EngineSize = 3000,
                    Doors = DoorCount.Two,
                    FuelType = FuelType.Petrol,
                    TransmissionType = TransmissionType.SemiAutomatic,
                    Condition = VehicleCondition.LikeNew,
                    ImageUrl = "https://media.ed.edmunds-media.com/bmw/m4/2026/oem/2026_bmw_m4_coupe_competition-xdrive_fq_oem_1_500.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 4
                }
            );
        }
    }
}

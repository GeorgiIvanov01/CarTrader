using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarTrader.Data.Models;
using CarTrader.Data.Enums;
using Microsoft.AspNetCore.Identity;
using static CarTrader.GCommon.InitialIdentitySeedConstants;

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

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = DefaultUserId,
                UserName = DefaultUserEmail,
                NormalizedUserName = DefaultUserEmail.ToUpper(),
                Email = DefaultUserEmail,
                NormalizedEmail = DefaultUserEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = DefaultUserPasswordHash,
                SecurityStamp = DefaultSecurityStamp,
                ConcurrencyStamp = DefaultConcurrencyStamp
            });

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
                    ImageUrl = "https://example.com/images/toyota-camry-2020.jpg",
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
                    ImageUrl = "https://example.com/images/honda-civic-2019.jpg",
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
                    ImageUrl = "https://example.com/images/ford-f150-2021.jpg",
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
                    ImageUrl = "https://example.com/images/chevrolet-tahoe-2020.jpg",
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
                    ImageUrl = "https://example.com/images/bmw-m4-2021.jpg",
                    IsDeleted = false,
                    SellerId = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                    CategoryId = 4
                }
            );
        }
    }
}

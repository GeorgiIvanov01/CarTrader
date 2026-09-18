using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarTrader.Data.Models
{
    public class UserVehicle
    {
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

using ReservacionVehicleBD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ReservacionVehicleBD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

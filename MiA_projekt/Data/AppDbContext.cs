using MiA_projekt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiA_projekt.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Models;
using System.Security.Cryptography.X509Certificates;

namespace RunGroupWebApp.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {//: base(options) to pass the DBContextOptions to the ApplicationDBContext 
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}

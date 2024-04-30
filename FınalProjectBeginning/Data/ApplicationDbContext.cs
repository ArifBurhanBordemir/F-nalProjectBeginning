using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FınalProjectBeginning.Models;
using System.Reflection.Metadata;

namespace FınalProjectBeginning.Data
{
    public class ApplicationDbContext : IdentityDbContext<CetUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<FınalProjectBeginning.Models.Event> Event { get; set; } = default!;
        public DbSet<Event> Events { get; set; }
    }
}

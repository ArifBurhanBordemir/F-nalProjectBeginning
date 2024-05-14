using FınalProjectBeginning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Management.Smo;
using System.Reflection.Metadata;

namespace FinalProjectBeginning.Data
{
    public class ApplicationDbContext : IdentityDbContext<CetUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events{ get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<CetUser> CetUsers { get; set; }
        public DbSet<Participate> Participates { get; set; }
        //public DbSet<Takip_Takipçi> TakipEdenUserlar { get; set; }
        //public DbSet<Takip_Takipçi> TakipEdilenKişiler { get; set; }

        public DbSet<Takip_Takipci> Takip_Takipcis { get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participate>()
                .HasOne(e => e.CetUser)
                .WithMany(ea => ea.Participates)
                .HasForeignKey(ei => ei.CetUserId);

            modelBuilder.Entity<Participate>()
                .HasOne(e => e.Event)
                .WithMany(ea => ea.Participates)
                .HasForeignKey(ei => ei.EventId);



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.Entity<Takip_Takipci>()
                .HasOne(x => x.TakipEdilenKisi)
                .WithMany(y => y.TakipEdenUsers)
                .HasForeignKey(z => z.TakipEdilenKisiId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Takip_Takipci>()
                .HasOne(x => x.TakipEdenUser)
                .WithMany(y => y.TakipEdilenKisis)
                .HasForeignKey(z => z.TakipEdenUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }

    }


}

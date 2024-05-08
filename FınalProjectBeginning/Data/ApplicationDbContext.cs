using FınalProjectBeginning.Models;
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
        //public DbSet<Takip_Takipçi> TakipEdenUserlar { get; set; }
        //public DbSet<Takip_Takipçi> TakipEdilenKişiler { get; set; }









        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(x => x.Takipçiler).WithMany(x => x.Takipler)
        //        .Map(x => x.ToTable("Followers")
        //            .MapLeftKey("UserId")
        //            .MapRightKey("FollowerId"));


        //    modelBuilder.Entity<CetUser>()
        //        .HasMany(tt => tt.Takipler)
        //        .WithMany(t => t.Takipçiler)
        //        .UsingEntity(j => j.ToTable("Takip_Takipçi"));

        //    //modelBuilder.Entity<CetUser>()
        //    //    .HasMany(tt => tt.Takipler)
        //    //    .WithMany(t => t.Takipçiler)
        //    //    .UsingEntity(j => j.ToTable("Takip_Takipci"));


        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Takip_Takipçi>()
        //        .HasKey(tt => new { tt.TakipEdenUserId, tt.TakipEdilenKişiId });

        //    modelBuilder.Entity<Takip_Takipçi>()
        //        .HasOne(tt => tt.TakipEdenUser)
        //        .WithMany(u => u.TakipEdenUserlar)
        //        .HasForeignKey(tt => tt.TakipEdenUserId)
        //        .OnDelete(DeleteBehavior.Restrict); // Değiştirildi

        //    modelBuilder.Entity<Takip_Takipçi>()
        //        .HasOne(tt => tt.TakipEdilenKişi)
        //        .WithMany(u => u.TakipEdilenKişiler)
        //        .HasForeignKey(tt => tt.TakipEdilenKişiId)
        //        .OnDelete(DeleteBehavior.Restrict); // Değiştirildi
        //}


    }


}

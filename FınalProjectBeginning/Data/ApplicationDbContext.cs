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
        //public DbSet<Takip_Takipçi> TakipEdenUserlar { get; set; }
        //public DbSet<Takip_Takipçi> TakipEdilenKişiler { get; set; }
        //public DbSet<CetUser> CetUsers { get; set; }
        //public DbSet<Event> Eventss { get; set; }

        //public DbSet<Participate> Participates { get; set; }

        public DbSet<Takip_Takipçi> Takip_Takipçis { get; set; }


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

            modelBuilder.Entity<Takip_Takipçi>()
                .HasOne(x=>x.TakipEdilenKişi)
                .WithMany(y=>y.TakipEdenUsers)
                .HasForeignKey(z=>z.TakipEdilenKişiId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Takip_Takipçi>()
                .HasOne(x => x.TakipEdenUser)
                .WithMany(y => y.TakipEdilenKişis)
                .HasForeignKey(z => z.TakipEdenUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }





        public DbSet<CetUser> CetUsers { get; set; }
        public DbSet<Participate> Participates { get; set; }



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

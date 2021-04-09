using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

#nullable disable

namespace Repository.Contexts
{
    public partial class EventFunctionsContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersEvent> UsersEvents { get; set; }
        public EventFunctionsContext() : base()
        {
        }

        public EventFunctionsContext(DbContextOptions<EventFunctionsContext> options) : base(options)
        {
        }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// 
//                 optionsBuilder.UseSqlServer("Server=tcp:eventfunctions.database.windows.net,1433;Initial Catalog=testDB4;Persist Security Info=False;User ID=event;Password=functions1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
//            }
//         }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(
                eb =>
                {
                    eb.HasKey(ky => new {ky.Id});
                    eb.HasMany(u => u.Users)
                    .WithMany(g => g.Events).UsingEntity<UsersEvent>(
                        j => j.HasOne(k => k.User).WithMany(y => y.UsersEvents),
                        j => j.HasOne(k => k.Event).WithMany(y => y.UsersEvents));
                    eb.HasOne(u => u.Manager);
                });

            modelBuilder.Entity<UsersEvent>(
                eb =>
                {
                    eb.HasKey(ky => new {ky.EventId, ky.UserId});
                    eb.HasOne(p => p.Event).WithMany(o => o.UsersEvents).HasForeignKey(k => k.EventId);
                    eb.HasOne(p => p.User).WithMany(o => o.UsersEvents).HasForeignKey(k => k.UserId);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

#nullable disable

namespace Repository.Contexts
{
    public partial class EventFunctionsContext : DbContext
    {
        public EventFunctionsContext()
        {
        }

        public EventFunctionsContext(DbContextOptions<EventFunctionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersEvent> UsersEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                    optionsBuilder.UseSqlServer("Server=tcp:eventfunctions.database.windows.net,1433;Initial Catalog=EventFunctionsDB;Persist Security Info=False;User ID=event;Password=functions1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
//                optionsBuilder.UseSqlServer("Server=500-409;Database=EventFunctionsDBTest;Trusted_Connection=True;");
            }
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        //     modelBuilder.Entity<Event>(entity =>
        //     {
        //         entity.Property(e => e.Date).HasColumnType("date");

        //         entity.Property(e => e.Description).IsRequired();

        //         entity.Property(e => e.Name)
        //             .IsRequired()
        //             .HasMaxLength(128);

        //         entity.Property(e => e.Revenue).HasColumnType("decimal(18, 0)");

        //         entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 0)");

        //         entity.HasOne(d => d.EventType)
        //             .WithMany(p => p.Events)
        //             .HasForeignKey(d => d.EventTypeId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_eventTypeId");

        //         entity.HasOne(d => d.Location)
        //             .WithMany(p => p.Events)
        //             .HasForeignKey(d => d.LocationId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_locationId");

        //         entity.HasOne(d => d.Manager)
        //             .WithMany(p => p.Events)
        //             .HasForeignKey(d => d.ManagerId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_managerId");
        //     });

        //     modelBuilder.Entity<EventType>(entity =>
        //     {
        //         entity.Property(e => e.Name)
        //             .IsRequired()
        //             .HasMaxLength(100);
        //     });

        //     modelBuilder.Entity<Location>(entity =>
        //     {
        //         entity.Property(e => e.Address)
        //             .IsRequired()
        //             .HasMaxLength(128);

        //         entity.Property(e => e.Latitude).HasColumnName("latitude");

        //         entity.Property(e => e.Longtitude).HasColumnName("longtitude");

        //         entity.Property(e => e.Name)
        //             .IsRequired()
        //             .HasMaxLength(128);
        //     });

        //     modelBuilder.Entity<Review>(entity =>
        //     {
        //         entity.HasNoKey();

        //         entity.Property(e => e.Description).IsRequired();

        //         entity.Property(e => e.Id).ValueGeneratedOnAdd();

        //         entity.HasOne(d => d.Event)
        //             .WithMany()
        //             .HasForeignKey(d => d.EventId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_rvwEventId");

        //         entity.HasOne(d => d.User)
        //             .WithMany()
        //             .HasForeignKey(d => d.UserId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_rvwUserId");
        //     });

        //     modelBuilder.Entity<User>(entity =>
        //     {
        //         entity.Property(e => e.Name)
        //             .IsRequired()
        //             .HasMaxLength(128);

        //         entity.Property(e => e.Password)
        //             .IsRequired()
        //             .HasMaxLength(128)
        //             .HasDefaultValueSql("('userpassword')");

        //         entity.Property(e => e.PasswordSalt)
        //             .IsRequired()
        //             .HasMaxLength(128)
        //             .HasDefaultValueSql("('saltpassword')");
        //     });

        //     modelBuilder.Entity<UsersEvent>(entity =>
        //     {
        //         entity.HasOne(d => d.Event)
        //             .WithMany(p => p.UsersEvents)
        //             .HasForeignKey(d => d.EventId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_eventId");

        //         entity.HasOne(d => d.User)
        //             .WithMany(p => p.UsersEvents)
        //             .HasForeignKey(d => d.UserId)
        //             .OnDelete(DeleteBehavior.ClientSetNull)
        //             .HasConstraintName("fk_userId");
        //     });

        //     OnModelCreatingPartial(modelBuilder);
        // }

      //  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

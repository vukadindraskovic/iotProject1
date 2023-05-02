using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphqlService.Repository
{
    public partial class SensorDbContext : DbContext
    {
        public SensorDbContext()
        {
        }

        public SensorDbContext(DbContextOptions<SensorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SensorValue> SensorValues { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorValue>(entity =>
            {
                entity.ToTable("sensor_values");

                entity.Property(e => e.Id)
                    .HasMaxLength(35)
                    .HasColumnName("id");

                entity.Property(e => e.NotedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("noted_date");

                entity.Property(e => e.Outin)
                    .HasMaxLength(3)
                    .HasColumnName("outin");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(50)
                    .HasColumnName("room_id");

                entity.Property(e => e.Temp).HasColumnName("temp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

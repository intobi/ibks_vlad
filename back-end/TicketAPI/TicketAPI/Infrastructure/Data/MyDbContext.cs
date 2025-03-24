﻿using Microsoft.EntityFrameworkCore;
using TicketAPI.Domain.Data;

namespace TicketAPI.Infrastructure.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public virtual DbSet<InstalledEnvironment> InstalledEnvironments { get; set; }

        public virtual DbSet<LogType> LogTypes { get; set; }

        public virtual DbSet<Priority> Priorities { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketEventLog> TicketEventLogs { get; set; }

        public virtual DbSet<TicketReply> TicketReplies { get; set; }

        public virtual DbSet<TicketType> TicketTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<InstalledEnvironment>(entity =>
            {
                entity.ToTable("InstalledEnvironment", "Support");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<LogType>(entity =>
            {
                entity.ToTable("LogType", "Support");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority", "Support");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "Support");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket", "Support");

                entity.HasIndex(e => e.InstalledEnvironmentId, "IX_Ticket_InstalledEnvironmentId");

                entity.HasIndex(e => e.PriorityId, "IX_Ticket_PriorityId");

                entity.HasIndex(e => e.StatusId, "IX_Ticket_StatusId");

                entity.HasIndex(e => e.TicketTypeId, "IX_Ticket_TicketTypeId");

                entity.HasIndex(e => e.UserOid, "IX_Ticket_UserOID");

                entity.Property(e => e.ApplicationName).HasMaxLength(250);
                entity.Property(e => e.Browser).HasMaxLength(250);
                entity.Property(e => e.CreatedByOid).HasColumnName("CreatedByOID");
                entity.Property(e => e.Device).HasMaxLength(250);
                entity.Property(e => e.Title).HasMaxLength(250);
                entity.Property(e => e.Url).HasMaxLength(1000);
                entity.Property(e => e.UserOid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserOID");

                entity.HasOne(d => d.InstalledEnvironment).WithMany(p => p.Tickets).HasForeignKey(d => d.InstalledEnvironmentId);

                entity.HasOne(d => d.Priority).WithMany(p => p.Tickets).HasForeignKey(d => d.PriorityId);

                entity.HasOne(d => d.Status).WithMany(p => p.Tickets).HasForeignKey(d => d.StatusId);

                entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets).HasForeignKey(d => d.TicketTypeId);

                entity.HasOne(d => d.UserO).WithMany(p => p.Tickets).HasForeignKey(d => d.UserOid);
            });

            modelBuilder.Entity<TicketEventLog>(entity =>
            {
                entity.ToTable("TicketEventLog", "Support");

                entity.HasIndex(e => e.LogTypeId, "IX_TicketEventLog_LogTypeId");

                entity.HasIndex(e => e.TicketId, "IX_TicketEventLog_TicketId");

                entity.HasOne(d => d.LogType).WithMany(p => p.TicketEventLogs).HasForeignKey(d => d.LogTypeId);

                entity.HasOne(d => d.Ticket).WithMany(p => p.TicketEventLogs).HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<TicketReply>(entity =>
            {
                entity.HasKey(e => e.ReplyId);

                entity.ToTable("TicketReply", "Support");

                entity.Property(e => e.Tid).HasColumnName("TId");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.ToTable("TicketType", "Support");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Oid);

                entity.ToTable("User", "Support");

                entity.Property(e => e.Oid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OID");
                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

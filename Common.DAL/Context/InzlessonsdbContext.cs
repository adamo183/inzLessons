﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using inzLessons.Common.Models;

namespace inzLessons.Common.Context
{
    public partial class InzlessonsdbContext : DbContext
    {
        public InzlessonsdbContext()
        {
        }

        public InzlessonsdbContext(DbContextOptions<InzlessonsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allowedreservation> Allowedreservation { get; set; }
        public virtual DbSet<Lessoncondition> Lessoncondition { get; set; }
        public virtual DbSet<Lessonsgroup> Lessonsgroup { get; set; }
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Useringroup> Useringroup { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Password=testPass123;Username=35223954_inzdb;Database=35223954_inzdb;Host=serwer2148690.home.pl");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allowedreservation>(entity =>
            {
                entity.ToTable("allowedreservation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Groupid).HasColumnName("groupid");

                entity.Property(e => e.Reservationdateend).HasColumnName("reservationdateend");

                entity.Property(e => e.Reservationdatestart).HasColumnName("reservationdatestart");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Allowedreservation)
                    .HasForeignKey(d => d.Groupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_allowedreservation_group");
            });

            modelBuilder.Entity<Lessoncondition>(entity =>
            {
                entity.ToTable("lessoncondition");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Allowedhourinweek).HasColumnName("allowedhourinweek");

                entity.Property(e => e.Configname)
                    .HasColumnName("configname")
                    .HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Teacherid).HasColumnName("teacherid");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Lessoncondition)
                    .HasForeignKey(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_lessonscondition");
            });

            modelBuilder.Entity<Lessonsgroup>(entity =>
            {
                entity.ToTable("lessonsgroup");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Creationdate).HasColumnName("creationdate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Teacherid).HasColumnName("teacherid");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Lessonsgroup)
                    .HasForeignKey(d => d.Teacherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_lessonsgroup_users");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("membership");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(256);

                entity.Property(e => e.PasswordSalt)
                    .HasColumnName("password_salt")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Groupid).HasColumnName("groupid");

                entity.Property(e => e.Isonline).HasColumnName("isonline");

                entity.Property(e => e.Reservationdate).HasColumnName("reservationdate");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Useringroup>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Groupid })
                    .HasName("useringroup_pkey");

                entity.ToTable("useringroup");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Groupid).HasColumnName("groupid");

                entity.Property(e => e.Condidionid).HasColumnName("condidionid");

                entity.HasOne(d => d.Condidion)
                    .WithMany(p => p.Useringroup)
                    .HasForeignKey(d => d.Condidionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_condition_useringroup");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Useringroup)
                    .HasForeignKey(d => d.Groupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group_useringroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Useringroup)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_useringroup");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Createdate).HasColumnName("createdate");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Users)
                    .HasForeignKey<Users>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_membership");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_role_users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
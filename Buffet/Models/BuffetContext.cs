using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Buffet.Models;

public partial class BuffetContext : DbContext
{
    public BuffetContext()
    {
    }

    public BuffetContext(DbContextOptions<BuffetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ResTableTotal> ResTableTotals { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<RestaurantsType> RestaurantsTypes { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Table> Tables { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=Buffet;User ID=dev;Password=1234;Encrypt=False");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK_Admin");

            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminPass)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdminPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.BookId).ValueGeneratedNever();
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.TableId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Course");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
            entity.Property(e => e.CourseName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId).HasName("PK_Customer");

            entity.Property(e => e.CusId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CusEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CusImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CusName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CusPass)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CusPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ResTableTotal>(entity =>
        {
            entity.HasKey(e => e.TTotalId).HasName("PK_TableTotal");

            entity.ToTable("ResTableTotal");

            entity.Property(e => e.TTotalId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("T_TotalId");
            entity.Property(e => e.Ltotal).HasColumnName("LTotal");
            entity.Property(e => e.Mtotal).HasColumnName("MTotal");
            entity.Property(e => e.Stotal).HasColumnName("STotal");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.ResId).HasName("PK_Restaurant");

            entity.Property(e => e.ResId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResAvg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TableId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ThemeId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TypeId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RestaurantsType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.ToTable("Restaurants_Type");

            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK_Staff");

            entity.Property(e => e.StaffId).ValueGeneratedNever();
            entity.Property(e => e.StaffEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StaffImg)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StaffName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StaffPass)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StaffPhone)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK_Table");

            entity.Property(e => e.TableId)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeM.data.Models
{
    public partial class Employee_DbContext : DbContext
    {
        public Employee_DbContext()
        {
        }

        public Employee_DbContext(DbContextOptions<Employee_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ExpenseTbl> ExpenseTbls { get; set; } = null!;
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; } = null!;
        public virtual DbSet<GenderTbl> GenderTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CYBERSPACE\\SQLEXPRESS;Database=Employee_Db;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Contact, "UQ__Employee__F7C04665C7DBCBC7")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Contact).HasMaxLength(200);

                entity.Property(e => e.EmployeeName).HasMaxLength(200);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__Employee__Gender__0A9D95DB");
            });

            modelBuilder.Entity<ExpenseTbl>(entity =>
            {
                entity.HasKey(e => e.ExpenseId)
                    .HasName("PK__ExpenseT__1445CFD3D9B21F3A");

                entity.ToTable("ExpenseTbl");

                entity.Property(e => e.ExpenseId).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ExpenseName).HasMaxLength(200);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ExpenseTbls)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__ExpenseTb__Emplo__0E6E26BF");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ExpenseTbls)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__ExpenseTb__TypeI__0D7A0286");
            });

            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__ExpenseT__516F03B5AA0030EB");

                entity.ToTable("ExpenseType");

                entity.Property(e => e.TypeId).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<GenderTbl>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PK__GenderTb__4E24E9F717CA53BA");

                entity.ToTable("GenderTbl");

                entity.Property(e => e.GenderId).ValueGeneratedNever();

                entity.Property(e => e.Gender).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

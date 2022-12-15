using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend_sicpa.Models
{
    public partial class SicpaDbContext : DbContext
    {
        public SicpaDbContext()
        {
        }

        public SicpaDbContext(DbContextOptions<SicpaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentsEmployee> DepartmentsEmployees { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");

                entity.HasIndex(e => e.EnterprisesId, "fk_departments_enterprises_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(500)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.EnterprisesId)
                    .HasColumnType("int(11)")
                    .HasColumnName("enterprises_id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(500)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(500)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.HasOne(d => d.Enterprises)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.EnterprisesId)
                    .HasConstraintName("fk_departments_enterprises");
            });

            modelBuilder.Entity<DepartmentsEmployee>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.DepartmentsId, e.EmployeesId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("departments_employees");

                entity.HasIndex(e => e.DepartmentsId, "fk_departments_has_employees_departments1_idx");

                entity.HasIndex(e => e.EmployeesId, "fk_departments_has_employees_employees1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.DepartmentsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("departments_id");

                entity.Property(e => e.EmployeesId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employees_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(500)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(500)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.HasOne(d => d.Departments)
                    .WithMany(p => p.DepartmentsEmployees)
                    .HasForeignKey(d => d.DepartmentsId)
                    .HasConstraintName("fk_departments_has_employees_departments1");

                entity.HasOne(d => d.Employees)
                    .WithMany(p => p.DepartmentsEmployees)
                    .HasForeignKey(d => d.EmployeesId)
                    .HasConstraintName("fk_departments_has_employees_employees1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Age)
                    .HasColumnType("int(40)")
                    .HasColumnName("age");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(500)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(500)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Position)
                    .HasMaxLength(500)
                    .HasColumnName("position");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");

                entity.Property(e => e.Surname)
                    .HasMaxLength(500)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.ToTable("enterprises");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(500)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(500)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(500)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

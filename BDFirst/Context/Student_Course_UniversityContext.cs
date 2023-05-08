using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BDFirst.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BDFirst.Context
{
    public partial class Student_Course_UniversityContext : IdentityDbContext
    {
        public Student_Course_UniversityContext()
        {
        }

        public Student_Course_UniversityContext(DbContextOptions<Student_Course_UniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<SCRelation> SCRelations { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student_Course_University");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedNever()
                    .HasColumnName("Course_ID");

                entity.Property(e => e.CourseName)
                    .IsUnicode(false)
                    .HasColumnName("Course_Name");
            });

            modelBuilder.Entity<SCRelation>(entity =>
            {
                entity.ToTable("S_C_Relation");

                entity.Property(e => e.FCourseId).HasColumnName("F_Course_id");

                entity.Property(e => e.FEnrollId).HasColumnName("F_Enroll_id");

                entity.HasOne(d => d.FCourse)
                    .WithMany(p => p.SCRelations)
                    .HasForeignKey(d => d.FCourseId)
                    .HasConstraintName("FK__S_C_Relat__F_Cou__2B3F6F97");

                entity.HasOne(d => d.FEnroll)
                    .WithMany(p => p.SCRelations)
                    .HasForeignKey(d => d.FEnrollId)
                    .HasConstraintName("FK__S_C_Relat__F_Enr__276EDEB3");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.EnrollId);

                entity.ToTable("Student");

                entity.Property(e => e.EnrollId)
                    .ValueGeneratedNever()
                    .HasColumnName("Enroll_Id");

                entity.Property(e => e.Department)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            //OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

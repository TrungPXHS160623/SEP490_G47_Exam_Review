using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.Models;

public partial class QuizManagementContext : DbContext
{
    public QuizManagementContext()
    {
    }

    public QuizManagementContext(DbContextOptions<QuizManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamAssignment> ExamAssignments { get; set; }

    public virtual DbSet<ExamStatus> ExamStatuses { get; set; }

    public virtual DbSet<InstructorAssignment> InstructorAssignments { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConStr");
        optionsBuilder.UseSqlServer(config);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.Property(e => e.CampusName).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.HeadOfDepartmentId, "IX_Departments_HeadOfDepartmentId");

            entity.Property(e => e.DepartmentName).HasMaxLength(255);

            entity.HasOne(d => d.HeadOfDepartment).WithMany(p => p.Departments)
                .HasForeignKey(d => d.HeadOfDepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasIndex(e => e.CreaterId, "IX_Exams_CreaterId");

            entity.HasIndex(e => e.ExamStatusId, "IX_Exams_ExamStatusId");

            entity.HasIndex(e => e.SubjectId, "IX_Exams_SubjectId");

            entity.Property(e => e.ExamCode).HasMaxLength(50);
            entity.Property(e => e.ExamDuration).HasMaxLength(10);
            entity.Property(e => e.ExamStatusId).HasColumnName("ExamStatusId");
            entity.Property(e => e.ExamType).HasMaxLength(50);

            entity.HasOne(d => d.Creater).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CreaterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ExamStatus).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ExamStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams).HasForeignKey(d => d.SubjectId);
        });

        modelBuilder.Entity<ExamAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);

            entity.HasIndex(e => e.AssignedBy, "IX_ExamAssignments_AssignedBy");

            entity.HasIndex(e => e.AssignedTo, "IX_ExamAssignments_AssignedTo");

            entity.HasIndex(e => e.ExamId, "IX_ExamAssignments_ExamId");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.ExamAssignments).HasForeignKey(d => d.AssignedBy);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.ExamAssignments)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Exam).WithMany(p => p.ExamAssignments)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ExamStatus>(entity =>
        {
            entity.Property(e => e.ExamStatusId).HasColumnName("ExamStatusId");
            entity.Property(e => e.StatusContent).HasMaxLength(255);
        });

        modelBuilder.Entity<InstructorAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);

            entity.HasIndex(e => e.AssignedTo, "IX_InstructorAssignments_AssignedTo");

            entity.HasIndex(e => e.ExamId, "IX_InstructorAssignments_ExamId");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.InstructorAssignments)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Exam).WithMany(p => p.InstructorAssignments)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.MenuId });

            entity.HasIndex(e => e.MenuId, "IX_MenuRoles_MenuId");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRoles).HasForeignKey(d => d.MenuId);

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReviewId);

            entity.HasIndex(e => e.ExamId, "IX_Reports_ExamId");

            entity.HasIndex(e => e.UserId, "IX_Reports_UserId");

            entity.HasOne(d => d.Exam).WithMany(p => p.Reports).HasForeignKey(d => d.ExamId);

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Subjects_DepartmentId");

            entity.Property(e => e.SubjectName).HasMaxLength(255);

            entity.HasOne(d => d.Department).WithMany(p => p.Subjects).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.CampusId, "IX_Users_CampusId");

            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.Property(e => e.Mail).HasMaxLength(250);

            entity.HasOne(d => d.Campus).WithMany(p => p.Users).HasForeignKey(d => d.CampusId);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

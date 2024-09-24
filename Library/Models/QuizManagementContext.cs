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

        modelBuilder.Entity<Campus>().HasData(
    new Campus { CampusId = 1, CampusName = "Hanoi", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
    new Campus { CampusId = 2, CampusName = "Ho Chi Minh", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
);

        // 2. Seed data for ExamStatus table
        modelBuilder.Entity<ExamStatus>().HasData(
            new ExamStatus { ExamStatusId = 1, StatusContent = "Not started", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 2, StatusContent = "In progress", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 3, StatusContent = "Completed", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 4, StatusContent = "Cancelled", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 3. Seed data for UserRole table
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { RoleId = 1, RoleName = "Admin", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 2, RoleName = "Examiner", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 3, RoleName = "Lecturer", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 4, RoleName = "Head of Department", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 5, RoleName = "Program Developer", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 4. Seed data for User table
        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, Mail = "admin@fpt.edu.vn", CampusId = 1, RoleId = 1, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 2, Mail = "examiner@fpt.edu.vn", CampusId = 1, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 3, Mail = "lecturer@fpt.edu.vn", CampusId = 2, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 4, Mail = "head@fpt.edu.vn", CampusId = 1, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 5, Mail = "developer@fpt.edu.vn", CampusId = 2, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 6, Mail = "trunghp@fpt.edu.vn", CampusId = 1, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 5. Seed data for Department table
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Information Technology", HeadOfDepartmentId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Department { DepartmentId = 2, DepartmentName = "Data Science", HeadOfDepartmentId = 6, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 6. Seed data for Subject table
        modelBuilder.Entity<Subject>().HasData(
            new Subject { SubjectId = 1, SubjectName = "C# Programming", DepartmentId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 2, SubjectName = "Computer Science", DepartmentId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 3, SubjectName = "Machine Learning", DepartmentId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 7. Seed data for Exam table
        modelBuilder.Entity<Exam>().HasData(
            new Exam { ExamId = 1, ExamCode = "EXAM001", ExamDuration = "10w", ExamType = "Essay", SubjectId = 1, CreaterId = 2, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "EXAM002", ExamDuration = "10w", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 3, ExamCode = "EXAM003", ExamDuration = "10w", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 8. Seed data for ExamAssignment table
        modelBuilder.Entity<ExamAssignment>().HasData(
            new ExamAssignment { AssignmentId = 1, ExamId = 1, AssignedBy = 2, AssignedTo = 1, AssignmentDate = DateTime.Now, Status = "Pending", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamAssignment { AssignmentId = 2, ExamId = 2, AssignedBy = 2, AssignedTo = 2, AssignmentDate = DateTime.Now, Status = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamAssignment { AssignmentId = 3, ExamId = 3, AssignedBy = 2, AssignedTo = 2, AssignmentDate = DateTime.Now, Status = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 9. Seed data for InstructorAssignment table
        modelBuilder.Entity<InstructorAssignment>().HasData(
            new InstructorAssignment { AssignmentId = 1, ExamId = 1, AssignedTo = 3, AssignmentDate = DateTime.Now, Status = "Pending", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new InstructorAssignment { AssignmentId = 2, ExamId = 2, AssignedTo = 3, AssignmentDate = DateTime.Now, Status = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 10. Seed data for Menu table
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuName = "Dashboard", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 2, MenuName = "Exam Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 3, MenuName = "User Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 11. Seed data for MenuRole table
        modelBuilder.Entity<MenuRole>().HasData(
            new MenuRole { RoleId = 1, MenuId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 3, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 5, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );



        // 12. Seed data for Report table
        // Seeding data for Report
        modelBuilder.Entity<Report>().HasData(
            new Report { ReviewId = 1, ExamId = 1, UserId = 3, ReportContent = "Report 1", QuestionSolutionDetail = "Solution explanation 1", QuestionNumber = 1, Score = 90, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReviewId = 2, ExamId = 2, UserId = 3, ReportContent = "Report 2", QuestionSolutionDetail = "Solution explanation 2", QuestionNumber = 2, Score = 85, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReviewId = 3, ExamId = 3, UserId = 3, ReportContent = "Report 3", QuestionSolutionDetail = "Solution explanation 3", QuestionNumber = 3, Score = 75, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

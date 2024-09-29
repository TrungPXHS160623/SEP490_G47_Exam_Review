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

    public virtual DbSet<CampusUserSubject> CampusUserSubjects { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamStatus> ExamStatuses { get; set; }

    public virtual DbSet<InstructorAssignment> InstructorAssignments { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHistory> UserHistories { get; set; }

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

        modelBuilder.Entity<CampusUserSubject>(entity =>
        {
            entity.ToTable("CampusUserSubject");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Campus).WithMany(p => p.CampusUserSubjects)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK_CampusUserSubject_Campuses");

            entity.HasOne(d => d.Subject).WithMany(p => p.CampusUserSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_CampusUserSubject_Subjects");

            entity.HasOne(d => d.User).WithMany(p => p.CampusUserSubjects)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CampusUserSubject_Users");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasIndex(e => e.CreaterId, "IX_Exams_CreaterId");

            entity.HasIndex(e => e.ExamStatusId, "IX_Exams_ExamStatusId");

            entity.HasIndex(e => e.SubjectId, "IX_Exams_SubjectId");

            entity.Property(e => e.ExamCode).HasMaxLength(50);
            entity.Property(e => e.ExamDuration).HasMaxLength(100);
            entity.Property(e => e.ExamType).HasMaxLength(50);

            entity.HasOne(d => d.Campus).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK_Exams_Campuses");

            entity.HasOne(d => d.Creater).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CreaterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ExamStatus).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ExamStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams).HasForeignKey(d => d.SubjectId);
        });

        modelBuilder.Entity<ExamStatus>(entity =>
        {
            entity.Property(e => e.StatusContent).HasMaxLength(255);
        });

        modelBuilder.Entity<InstructorAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);

            entity.HasIndex(e => e.AssignedUserId, "IX_InstructorAssignments_AssignedTo");

            entity.HasIndex(e => e.ExamId, "IX_InstructorAssignments_ExamId");

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.InstructorAssignments)
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstructorAssignments_Users_AssignedTo");

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
            entity.Property(e => e.SubjectCode).HasMaxLength(255);
            entity.Property(e => e.SubjectName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.Property(e => e.Mail).HasMaxLength(250);

            entity.HasOne(d => d.Campus).WithMany(p => p.Users)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK_Users_Campuses");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.ToTable("UserHistory");

            entity.Property(e => e.LogDt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserHistory_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Campus>().HasData(
            new Campus { CampusId = 1, CampusName = "Hòa Lạc", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 2, CampusName = "Đà Nẵng", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 3, CampusName = "Cần Thơ", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 4, CampusName = "Hồ Chí Minh", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 2. Seed data for ExamStatus table
        modelBuilder.Entity<ExamStatus>().HasData(
            new ExamStatus { ExamStatusId = 1, StatusContent = "Not Assign", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 2, StatusContent = "Waiting to Assign", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 3, StatusContent = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 4, StatusContent = "Reviewing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 5, StatusContent = "Finish Review", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 6, StatusContent = "Complete", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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
            new User { UserId = 2, Mail = "examiner@fpt.edu.vn", CampusId = 2, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 3, Mail = "lecturer1@fpt.edu.vn", CampusId = 1, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 4, Mail = "lecturer2@fpt.edu.vn", CampusId = 1, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 5, Mail = "lecturer3@fpt.edu.vn", CampusId = 2, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 6, Mail = "lecturer4@fpt.edu.vn", CampusId = 2, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 7, Mail = "lecturer5@fpt.edu.vn", CampusId = 3, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 8, Mail = "lecturer6@fpt.edu.vn", CampusId = 3, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 9, Mail = "lecturer7@fpt.edu.vn", CampusId = 4, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 10, Mail = "head@fpt.edu.vn", CampusId = 1, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 11, Mail = "developer@fpt.edu.vn", CampusId = 2, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 12, Mail = "trunghp@fpt.edu.vn", CampusId = 3, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 6. Seed data for Subject table
        modelBuilder.Entity<Subject>().HasData(
            new Subject { SubjectId = 1, SubjectCode = "PRN231", SubjectName = "C# Programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 2, SubjectCode = "CSI123", SubjectName = "Computer Science", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 3, SubjectCode = "MLN123", SubjectName = "Machine Learning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        modelBuilder.Entity<CampusUserSubject>().HasData(
            new CampusUserSubject { Id = 1, SubjectId = 1, CampusId = 1, UserId = 3 },
            new CampusUserSubject { Id = 2, SubjectId = 1, CampusId = 2, UserId = 5 },
            new CampusUserSubject { Id = 3, SubjectId = 1, CampusId = 3, UserId = 7 },
            new CampusUserSubject { Id = 4, SubjectId = 1, CampusId = 4, UserId = 9 },
            new CampusUserSubject { Id = 5, SubjectId = 2, CampusId = 1, UserId = 3 },
            new CampusUserSubject { Id = 6, SubjectId = 2, CampusId = 2, UserId = 5 },
            new CampusUserSubject { Id = 7, SubjectId = 2, CampusId = 3, UserId = 7 },
            new CampusUserSubject { Id = 8, SubjectId = 2, CampusId = 4, UserId = 9 },
            new CampusUserSubject { Id = 9, SubjectId = 3, CampusId = 2, UserId = 6 }
        );

        // 7. Seed data for Exam table
        modelBuilder.Entity<Exam>().HasData(
            new Exam { ExamId = 1, ExamCode = "EXAM001", ExamDuration = "Block 10 (10 weeks)", ExamType = "Writing", SubjectId = 1, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "EXAM002", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 2, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 3, ExamCode = "EXAM003", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 9. Seed data for InstructorAssignment table
        modelBuilder.Entity<InstructorAssignment>().HasData(
            new InstructorAssignment { AssignmentId = 1, ExamId = 1, AssignedUserId = 3, AssignmentDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new InstructorAssignment { AssignmentId = 2, ExamId = 2, AssignedUserId = 3, AssignmentDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 10. Seed data for Menu table
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuLink = "/usermanagement", MenuName = "User management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 2, MenuLink = "/Admin/History", MenuName = "History", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 3, MenuLink = "/TestDepartment/ExamList", MenuName = "Exam List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 4, MenuLink = "/HeadDepartment/ExamList", MenuName = "Head Department List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 5, MenuLink = "/Lecture/ExamList", MenuName = "Lecture List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // 11. Seed data for MenuRole table
        modelBuilder.Entity<MenuRole>().HasData(
            new MenuRole { RoleId = 1, MenuId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 3, MenuId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 12. Seed data for Report table
        modelBuilder.Entity<Report>().HasData(
            new Report { ReviewId = 1, ExamId = 1, UserId = 3, ReportContent = "Report 1", QuestionSolutionDetail = "Solution explanation 1", QuestionNumber = 1, Score = 90, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReviewId = 2, ExamId = 2, UserId = 3, ReportContent = "Report 2", QuestionSolutionDetail = "Solution explanation 2", QuestionNumber = 2, Score = 85, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReviewId = 3, ExamId = 3, UserId = 3, ReportContent = "Report 3", QuestionSolutionDetail = "Solution explanation 3", QuestionNumber = 3, Score = 75, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

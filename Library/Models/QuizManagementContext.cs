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

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Campus> Campuses { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<ExamStatus> ExamStatuses { get; set; }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<ExamAssignment> ExamAssignments { get; set; }
    public virtual DbSet<InstructorAssignment> InstructorAssignments { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConStr");
        optionsBuilder.UseSqlServer(config);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<MenuRole>().HasKey(e => new { e.RoleId, e.MenuId });

        // Thiết lập quan hệ giữa Campus và User
        modelBuilder.Entity<Campus>()
            .HasMany(c => c.Users)
            .WithOne(u => u.Campus)
            .HasForeignKey(u => u.CampusId);

        // Thiết lập quan hệ giữa ExamStatus và Exam
        modelBuilder.Entity<ExamStatus>()
            .HasMany(es => es.Exams)
            .WithOne(e => e.ExamStatus)
            .HasForeignKey(e => e.ExamStatusID);

        // Thiết lập quan hệ giữa UserRole và User
        modelBuilder.Entity<UserRole>()
            .HasMany(ur => ur.Users)
            .WithOne(u => u.UserRole)
            .HasForeignKey(u => u.RoleId);

        // Thiết lập quan hệ giữa Department và User (HeadOfDepartment)
        modelBuilder.Entity<Department>()
            .HasOne(d => d.HeadOfDepartment)
            .WithMany()
            .HasForeignKey(d => d.HeadOfDepartmentId);

        // Thiết lập quan hệ giữa Department và Subject
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Subjects)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DepartmentId);

        // Thiết lập quan hệ giữa Subject và Exam
        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Exams)
            .WithOne(e => e.Subject)
            .HasForeignKey(e => e.SubjectId);

        // Thiết lập quan hệ giữa User và Exam (Creator)
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Creator)
            .WithMany()
            .HasForeignKey(e => e.CreaterId)
            .OnDelete(DeleteBehavior.Restrict);
           

        // Thiết lập quan hệ giữa Exam và ExamAssignment
        modelBuilder.Entity<Exam>()
            .HasMany(e => e.ExamAssignments)
            .WithOne(ea => ea.Exam)
            .HasForeignKey(ea => ea.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Thiết lập quan hệ giữa Exam và InstructorAssignment
        modelBuilder.Entity<Exam>()
            .HasMany(e => e.InstructorAssignments)
            .WithOne(ia => ia.Exam)
            .HasForeignKey(ia => ia.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Thiết lập quan hệ giữa ExamAssignment và AssignedToDepartment
        modelBuilder.Entity<ExamAssignment>()
            .HasOne(ea => ea.AssignedDepartment) // Mỗi ExamAssignment được giao cho một Department
            .WithMany() // Một Department có thể nhận nhiều ExamAssignment
            .HasForeignKey(ea => ea.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);

        // Thiết lập quan hệ giữa InstructorAssignment và User (AssignedTo)
        modelBuilder.Entity<InstructorAssignment>()
            .HasOne(ia => ia.AssignedUser)
            .WithMany()
            .HasForeignKey(ia => ia.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);

        // Thiết lập quan hệ giữa Exam và Report
        modelBuilder.Entity<Exam>()
            .HasMany(e => e.Reports)
            .WithOne(r => r.Exam)
            .HasForeignKey(r => r.ExamId);

        // Thiết lập quan hệ giữa User và Report
        modelBuilder.Entity<User>()
            .HasMany(u => u.Reports)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Thiết lập quan hệ giữa Menu và MenuRole
        modelBuilder.Entity<Menu>()
            .HasMany(m => m.MenuRoles)
            .WithOne(mr => mr.Menu)
            .HasForeignKey(mr => mr.MenuId);

        // Thiết lập quan hệ giữa UserRole và MenuRole
        modelBuilder.Entity<UserRole>()
            .HasMany(ur => ur.MenuRoles)
            .WithOne(mr => mr.Role)
            .HasForeignKey(mr => mr.RoleId);

      

        // 1. Seed data for Campus table
        modelBuilder.Entity<Campus>().HasData(
            new Campus { CampusId = 1, CampusName = "Hanoi", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 2, CampusName = "Ho Chi Minh", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 2. Seed data for ExamStatus table
        modelBuilder.Entity<ExamStatus>().HasData(
            new ExamStatus { ExamStatusID = 1, StatusContent = "Not started", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusID = 2, StatusContent = "In progress", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusID = 3, StatusContent = "Completed", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusID = 4, StatusContent = "Cancelled", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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
            new Exam { ExamId = 1, ExamCode = "EXAM001", ExamDuration = "10w", ExamType = "Essay", SubjectId = 1, CreaterId = 2, ExamStatusID = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "EXAM002", ExamDuration = "10w", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, ExamStatusID = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 3, ExamCode = "EXAM003", ExamDuration = "10w", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, ExamStatusID = 1, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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
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

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<MainQuestion> MainQuestions { get; set; }
    public virtual DbSet<QuestionHistory> QuestionHistories { get; set; }
    public virtual DbSet<QuestionSubject> QuestionSubjects { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<SubQuestion> SubQuestions { get; set; }
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConStr");
        optionsBuilder.UseSqlServer(config);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /// Đặt khóa chính cho User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.UserId)
            .ValueGeneratedOnAdd(); // Tự động tạo giá trị cho UserId

        // Thiết lập quan hệ giữa User và Campus
        modelBuilder.Entity<User>()
            .HasOne(u => u.Campus) // Mỗi User có một Campus
            .WithMany() // Một Campus có nhiều User
            .HasForeignKey(u => u.CampusId) // Khóa ngoại là CampusId
            .OnDelete(DeleteBehavior.Restrict); // Không xóa User khi Campus bị xóa

        // Thiết lập quan hệ giữa User và UserRole
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserRole) // Mỗi User có một UserRole
            .WithMany() // Một UserRole có nhiều User
            .HasForeignKey(u => u.RoleId) // Khóa ngoại là RoleId
            .OnDelete(DeleteBehavior.Restrict); // Không xóa User khi UserRole bị xóa

        modelBuilder.Entity<UserRole>()
            .HasKey(r => r.RoleId); // Đặt khóa chính cho UserRole

        // Thiết lập quan hệ giữa Department và HeadOfDepartment
        modelBuilder.Entity<Department>()
            .HasOne(d => d.HeadOfDepartment) // Mỗi Department có một HeadOfDepartment
            .WithMany() // Một HeadOfDepartment có thể là trưởng của nhiều Department
            .HasForeignKey(d => d.HeadOfDepartmentId) // Khóa ngoại là HeadOfDepartmentId
            .OnDelete(DeleteBehavior.Restrict); // Không xóa Department khi HeadOfDepartment bị xóa

        // Thiết lập quan hệ giữa Subject và Department
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Department) // Mỗi Subject thuộc một Department
            .WithMany() // Một Department có nhiều Subject
            .HasForeignKey(s => s.DepartmentId); // Khóa ngoại là DepartmentId

        // Thiết lập quan hệ giữa Exam và Subject
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.Subject) // Mỗi Exam thuộc một Subject
            .WithMany() // Một Subject có nhiều Exam
            .HasForeignKey(e => e.SubjectId); // Khóa ngoại là SubjectId

        // Thiết lập quan hệ giữa Exam và User
        modelBuilder.Entity<Exam>()
            .HasOne(e => e.User) // Mỗi Exam được tạo bởi một User
            .WithMany() // Một User có thể tạo nhiều Exam
            .HasForeignKey(e => e.UserId); // Khóa ngoại là UserId

        // Thiết lập quan hệ giữa ExamAssignment và Exam
        modelBuilder.Entity<ExamAssignment>()
            .HasOne(ea => ea.Exam) // Mỗi ExamAssignment liên kết với một Exam
            .WithMany() // Một Exam có thể có nhiều ExamAssignment
            .HasForeignKey(ea => ea.ExamId) // Khóa ngoại là ExamId
            .OnDelete(DeleteBehavior.Restrict); // Không xóa ExamAssignment khi Exam bị xóa

        // Thiết lập quan hệ giữa ExamAssignment và AssignedByUser
        modelBuilder.Entity<ExamAssignment>()
            .HasOne(ea => ea.AssignedByUser) // Mỗi ExamAssignment được giao bởi một User
            .WithMany() // Một User có thể giao nhiều ExamAssignment
            .HasForeignKey(ea => ea.AssignedBy) // Khóa ngoại là AssignedBy
            .OnDelete(DeleteBehavior.Restrict); // Không xóa ExamAssignment khi User giao bị xóa

        // Thiết lập quan hệ giữa ExamAssignment và AssignedToDepartment
        modelBuilder.Entity<ExamAssignment>()
            .HasOne(ea => ea.AssignedToDepartment) // Mỗi ExamAssignment được giao cho một Department
            .WithMany() // Một Department có thể nhận nhiều ExamAssignment
            .HasForeignKey(ea => ea.AssignedTo) // Khóa ngoại là AssignedTo
            .OnDelete(DeleteBehavior.Restrict); // Không xóa ExamAssignment khi Department nhận bị xóa

        modelBuilder.Entity<InstructorAssignment>()
           .HasOne(ia => ia.Exam) // Mỗi InstructorAssignment liên kết với một Exam
           .WithMany() // Một Exam có thể có nhiều InstructorAssignment
           .HasForeignKey(ia => ia.ExamId)
           .OnDelete(DeleteBehavior.Restrict); // Thay đổi từ Cascade sang Restrict

        modelBuilder.Entity<InstructorAssignment>()
            .HasOne(ia => ia.AssignedToUser) // Mỗi InstructorAssignment được giao cho một User
            .WithMany() // Một User có thể nhận nhiều InstructorAssignment
            .HasForeignKey(ia => ia.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi từ Cascade sang Restrict


        modelBuilder.Entity<Report>()
            .HasOne(r => r.Exam) // Mỗi Report liên kết với một Exam
            .WithMany() // Một Exam có thể có nhiều Report
            .HasForeignKey(r => r.ExamId)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi từ Cascade sang Restrict

        modelBuilder.Entity<Report>()
            .HasOne(r => r.User) // Mỗi Report liên kết với một User
            .WithMany() // Một User có thể có nhiều Report
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi từ Cascade sang Restrict

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<MainQuestion>(entity =>
        {
            entity.HasKey(e => e.MainId);

            entity.ToTable("MainQuestion");

            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.Images)
                .HasMaxLength(250)
                .HasColumnName("images");
            entity.Property(e => e.MainContent)
                .HasMaxLength(250)
                .HasColumnName("main_content");
            entity.Property(e => e.QuestionType).HasColumnName("question_type");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subject_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.MainQuestions)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_MainQuestion_QuestionSubject");
        });

        modelBuilder.Entity<QuestionHistory>(entity =>
        {
            entity.ToTable("QuestionHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("update_dt");

            entity.HasOne(d => d.Account).WithMany(p => p.QuestionHistories)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_QuestionHistory_Account");

            entity.HasOne(d => d.Main).WithMany(p => p.QuestionHistories)
                .HasForeignKey(d => d.MainId)
                .HasConstraintName("FK_QuestionHistory_MainQuestion");
        });

        modelBuilder.Entity<QuestionSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId);

            entity.ToTable("QuestionSubject");

            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("time");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(250)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SubQuestion>(entity =>
        {
            entity.HasKey(e => e.SubId);

            entity.ToTable("SubQuestion");

            entity.Property(e => e.SubId)
                .HasColumnName("sub_id");
            entity.Property(e => e.IsAnswer)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("is_answer");
            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.SubContent)
                .HasMaxLength(250)
                .HasColumnName("sub_content");

            entity.HasOne(d => d.Main).WithMany(p => p.SubQuestions)
                .HasForeignKey(d => d.MainId)
                .HasConstraintName("FK_SubQuestion_MainQuestion");
        });



        modelBuilder.Entity<UserRole>().HasData(
           new UserRole { RoleId = 1, RoleName = "Head of Department" },
           new UserRole { RoleId = 2, RoleName = "Lecturer" },
           new UserRole { RoleId = 3, RoleName = "Examiner" },
           new UserRole { RoleId = 4, RoleName = "Curriculum Developer" },
           new UserRole { RoleId = 5, RoleName = "Customer" }
        );

        modelBuilder.Entity<Campus>()
            .HasKey(c => c.CampusId);

        modelBuilder.Entity<Campus>().HasData(
            new Campus { CampusId = 1, CampusName = "Hoa Lac" },
            new Campus { CampusId = 2, CampusName = "Da Nang" },
            new Campus { CampusId = 3, CampusName = "Ho Chi Minh" },
            new Campus { CampusId = 4, CampusName = "Can Tho" },
            new Campus { CampusId = 5, CampusName = "Quy Nhon" }
        );

        


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
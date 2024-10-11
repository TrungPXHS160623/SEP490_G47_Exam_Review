using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=QuizManagement;User ID=sa;Password=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.Property(e => e.CampusName).HasMaxLength(100);
        });

        modelBuilder.Entity<CampusUserSubject>(entity =>
        {
            entity.ToTable("CampusUserSubject");

            entity.HasIndex(e => e.CampusId, "IX_CampusUserSubject_CampusId");

            entity.HasIndex(e => e.SubjectId, "IX_CampusUserSubject_SubjectId");

            entity.HasIndex(e => e.UserId, "IX_CampusUserSubject_UserId");

            entity.Property(e => e.Id).HasColumnName("id");

			entity.Property(e => e.IsLecturer)
		    .IsRequired()
		    .HasDefaultValue(false);
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
            entity.HasIndex(e => e.CampusId, "IX_Exams_CampusId");

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

            entity.HasIndex(e => e.AssignStatusId, "IX_InstructorAssignments_AssignStatusId");

            entity.HasOne(d => d.AssignedUser).WithMany(p => p.InstructorAssignments)
                .HasForeignKey(d => d.AssignedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstructorAssignments_Users_AssignedTo");

            entity.HasOne(d => d.Exam).WithMany(p => p.InstructorAssignments)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ExamStatus).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.AssignStatusId)
                .OnDelete(DeleteBehavior.Cascade);
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
            entity.HasKey(e => e.ReportId);

            entity.HasOne(d => d.Assignment).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK_Reports_InstructorAssignments");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.SubjectCode).HasMaxLength(255);
            entity.Property(e => e.SubjectName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.CampusId, "IX_Users_CampusId");

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

            entity.HasIndex(e => e.UserId, "IX_UserHistory_UserId");

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
            new Campus { CampusId = 1, CampusName = "Ha Noi", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 2, CampusName = "Da Nang", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 3, CampusName = "Can Tho", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 4, CampusName = "Ho Chi Minh", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Campus { CampusId = 5, CampusName = "Quy Nhon", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // 2. Seed data for ExamStatus table
        modelBuilder.Entity<ExamStatus>().HasData(
            new ExamStatus { ExamStatusId = 1, StatusContent = "Not Assign", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 2, StatusContent = "Waiting To Assign", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 3, StatusContent = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 4, StatusContent = "Reviewing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 5, StatusContent = "Exam With Errors", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new ExamStatus { ExamStatusId = 6, StatusContent = "Faultless Exam", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new ExamStatus { ExamStatusId = 7, StatusContent = "Complete", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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

            // Seed data for role admin
            new User { UserId = 1, Mail = "admin@fpt.edu.vn", CampusId = 1, RoleId = 1, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Examiner
            new User { UserId = 2, Mail = "lienkt@fpt.edu.vn", CampusId = 1, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 3, Mail = "hoanglm@fpt.edu.vn", CampusId = 2, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 4, Mail = "anhnq@fpt.edu.vn", CampusId = 3, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 5, Mail = "minhnh@fpt.edu.vn", CampusId = 4, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 6, Mail = "phongtl@fpt.edu.vn", CampusId = 5, RoleId = 2, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Lecturer
            new User { UserId = 7, Mail = "lanhbt@fpt.edu.vn", CampusId = 1, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 8, Mail = "khoadt@fpt.edu.vn", CampusId = 2, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 9, Mail = "hoangtm@fpt.edu.vn", CampusId = 3, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 10, Mail = "minhph@fpt.edu.vn", CampusId = 4, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 11, Mail = "trangnt@fpt.edu.vn", CampusId = 5, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 27, Mail = "quanpt@fpt.edu.vn", CampusId = 1, RoleId = 3, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


            // Seed data for Head of Department
            new User { UserId = 12, Mail = "namlh@fpt.edu.vn", CampusId = 1, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 13, Mail = "quangnv@fpt.edu.vn", CampusId = 1, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 14, Mail = "huylt@fpt.edu.vn", CampusId = 2, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 15, Mail = "tuanpv@fpt.edu.vn", CampusId = 2, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 16, Mail = "ngocdt@fpt.edu.vn", CampusId = 3, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 17, Mail = "minhth@fpt.edu.vn", CampusId = 3, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 18, Mail = "binhlt@fpt.edu.vn", CampusId = 4, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 19, Mail = "lanhnv@fpt.edu.vn", CampusId = 4, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 20, Mail = "duongkt@fpt.edu.vn", CampusId = 5, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 21, Mail = "phuonglt@fpt.edu.vn", CampusId = 5, RoleId = 4, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Program Developer
            new User { UserId = 22, Mail = "phucdt@fpt.edu.vn", CampusId = 1, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 23, Mail = "thanhnt@fpt.edu.vn", CampusId = 2, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 24, Mail = "hungpv@fpt.edu.vn", CampusId = 3, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 25, Mail = "anhpt@fpt.edu.vn", CampusId = 4, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 26, Mail = "truongvq@fpt.edu.vn", CampusId = 5, RoleId = 5, IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }


        );

        // 6. Seed data for Subject table
        modelBuilder.Entity<Subject>().HasData(

            // Seed data for software engineering major
            new Subject { SubjectId = 1, SubjectCode = "PRN211", SubjectName = "Basic Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 2, SubjectCode = "PRN221", SubjectName = "Advanced Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 3, SubjectCode = "PRN231", SubjectName = "Building Cross-Platform Back-End Application With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 4, SubjectCode = "MAE101", SubjectName = "Mathematics for Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 5, SubjectCode = "NWC203c", SubjectName = "Computer Networking", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for international business major
            new Subject { SubjectId = 6, SubjectCode = "ENM401", SubjectName = "Business English", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 7, SubjectCode = "ECO121", SubjectName = "Basic Macro Economics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 8, SubjectCode = "ECO201", SubjectName = "International Economics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 9, SubjectCode = "ACC101", SubjectName = "Principles of Accounting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 10, SubjectCode = "MKT101", SubjectName = "Marketing Principles", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }




        );

        modelBuilder.Entity<CampusUserSubject>().HasData(

            //Seed data for Heads of Department of Ha Noi campus
            new CampusUserSubject { Id = 1, SubjectId = 1, CampusId = 1, UserId = 12 },
            new CampusUserSubject { Id = 2, SubjectId = 2, CampusId = 1, UserId = 12 },
            new CampusUserSubject { Id = 3, SubjectId = 3, CampusId = 1, UserId = 12 },
            new CampusUserSubject { Id = 4, SubjectId = 4, CampusId = 1, UserId = 12 },
            new CampusUserSubject { Id = 5, SubjectId = 5, CampusId = 1, UserId = 12 },


            new CampusUserSubject { Id = 6, SubjectId = 6, CampusId = 1, UserId = 13 },
            new CampusUserSubject { Id = 7, SubjectId = 7, CampusId = 1, UserId = 13 },
            new CampusUserSubject { Id = 8, SubjectId = 8, CampusId = 1, UserId = 13 },
            new CampusUserSubject { Id = 9, SubjectId = 9, CampusId = 1, UserId = 13 },
            new CampusUserSubject { Id = 10, SubjectId = 10, CampusId = 1, UserId = 13 },

		//Seed data for Lecturer of Ha Noi campus
            new CampusUserSubject { Id = 11, SubjectId = 1, CampusId = 1, UserId = 7, IsLecturer = true },
            new CampusUserSubject { Id = 12, SubjectId = 2, CampusId = 1, UserId = 7, IsLecturer = true },
            new CampusUserSubject { Id = 13, SubjectId = 3, CampusId = 1, UserId = 7, IsLecturer = true },
            new CampusUserSubject { Id = 14, SubjectId = 4, CampusId = 1, UserId = 7, IsLecturer = true },
            new CampusUserSubject { Id = 15, SubjectId = 5, CampusId = 1, UserId = 7, IsLecturer = true },


            new CampusUserSubject { Id = 16, SubjectId = 6, CampusId = 1, UserId = 27, IsLecturer = true },
            new CampusUserSubject { Id = 17, SubjectId = 7, CampusId = 1, UserId = 27, IsLecturer = true },
            new CampusUserSubject { Id = 18, SubjectId = 8, CampusId = 1, UserId = 27, IsLecturer = true },
            new CampusUserSubject { Id = 19, SubjectId = 9, CampusId = 1, UserId = 27, IsLecturer = true },
            new CampusUserSubject { Id = 20, SubjectId = 10, CampusId = 1, UserId = 27, IsLecturer = true },

            // Seed data for Heads of Department of Can Tho campus
            new CampusUserSubject { Id = 21, SubjectId = 1, CampusId = 3, UserId = 16 },
            new CampusUserSubject { Id = 22, SubjectId = 2, CampusId = 3, UserId = 16 },
            new CampusUserSubject { Id = 23, SubjectId = 3, CampusId = 3, UserId = 16 },
            new CampusUserSubject { Id = 24, SubjectId = 4, CampusId = 3, UserId = 16 },
            new CampusUserSubject { Id = 25, SubjectId = 5, CampusId = 3, UserId = 16 },


            new CampusUserSubject { Id = 26, SubjectId = 6, CampusId = 3, UserId = 17 },
            new CampusUserSubject { Id = 27, SubjectId = 7, CampusId = 3, UserId = 17 },
            new CampusUserSubject { Id = 28, SubjectId = 8, CampusId = 3, UserId = 17 },
            new CampusUserSubject { Id = 29, SubjectId = 9, CampusId = 3, UserId = 17 },
            new CampusUserSubject { Id = 30, SubjectId = 10, CampusId = 3, UserId = 17 },

            // Seed data for Heads of Department of Ho Chi minh campus
            new CampusUserSubject { Id = 31, SubjectId = 1, CampusId = 4, UserId = 18 },
            new CampusUserSubject { Id = 32, SubjectId = 2, CampusId = 4, UserId = 18 },
            new CampusUserSubject { Id = 33, SubjectId = 3, CampusId = 4, UserId = 18 },
            new CampusUserSubject { Id = 34, SubjectId = 4, CampusId = 4, UserId = 18 },
            new CampusUserSubject { Id = 35, SubjectId = 5, CampusId = 4, UserId = 18 },


            new CampusUserSubject { Id = 36, SubjectId = 6, CampusId = 4, UserId = 19 },
            new CampusUserSubject { Id = 37, SubjectId = 7, CampusId = 4, UserId = 19 },
            new CampusUserSubject { Id = 38, SubjectId = 8, CampusId = 4, UserId = 19 },
            new CampusUserSubject { Id = 39, SubjectId = 9, CampusId = 4, UserId = 19 },
            new CampusUserSubject { Id = 40, SubjectId = 10, CampusId = 4, UserId = 19 },


            // Seed data for Heads of Department of Quy nhon campus
            new CampusUserSubject { Id = 41, SubjectId = 1, CampusId = 5, UserId = 20 },
            new CampusUserSubject { Id = 42, SubjectId = 2, CampusId = 5, UserId = 20 },
            new CampusUserSubject { Id = 43, SubjectId = 3, CampusId = 5, UserId = 20 },
            new CampusUserSubject { Id = 44, SubjectId = 4, CampusId = 5, UserId = 20 },
            new CampusUserSubject { Id = 45, SubjectId = 5, CampusId = 5, UserId = 20 },


            new CampusUserSubject { Id = 46, SubjectId = 6, CampusId = 5, UserId = 21 },
            new CampusUserSubject { Id = 47, SubjectId = 7, CampusId = 5, UserId = 21 },
            new CampusUserSubject { Id = 48, SubjectId = 8, CampusId = 5, UserId = 21 },
            new CampusUserSubject { Id = 49, SubjectId = 9, CampusId = 5, UserId = 21 },
            new CampusUserSubject { Id = 50, SubjectId = 10, CampusId = 5, UserId = 21 },

            // Seed data for Heads of Department of Da Nang campus
            new CampusUserSubject { Id = 51, SubjectId = 1, CampusId = 2, UserId = 14 },
            new CampusUserSubject { Id = 52, SubjectId = 2, CampusId = 2, UserId = 14 },
            new CampusUserSubject { Id = 53, SubjectId = 3, CampusId = 2, UserId = 14 },
            new CampusUserSubject { Id = 54, SubjectId = 4, CampusId = 2, UserId = 14 },
            new CampusUserSubject { Id = 55, SubjectId = 5, CampusId = 2, UserId = 14 },


            new CampusUserSubject { Id = 56, SubjectId = 6, CampusId = 2, UserId = 15 },
            new CampusUserSubject { Id = 57, SubjectId = 7, CampusId = 2, UserId = 15 },
            new CampusUserSubject { Id = 58, SubjectId = 8, CampusId = 2, UserId = 15 },
            new CampusUserSubject { Id = 59, SubjectId = 9, CampusId = 2, UserId = 15 },
            new CampusUserSubject { Id = 60, SubjectId = 10, CampusId = 2, UserId = 15 }

        );

        // 7. Seed data for Exam table
        modelBuilder.Entity<Exam>().HasData(

            // Ha Noi's Examiners create exams
            // Seed data for software engineering major
            new Exam { ExamId = 1, ExamCode = "PRN211_Q1_10_123456", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, ExamStatusId = 5, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "PRN211_Q2_5_654321", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, ExamStatusId = 5, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 3, ExamCode = "PRN221_Q1_10_789012", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, ExamStatusId = 6, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 4, ExamCode = "PRN221_Q2_5_210987", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 5, ExamCode = "PRN231_Q1_10_345678", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 6, ExamCode = "PRN231_Q2_5_876543", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 7, ExamCode = "MAE101_Q1_10_234567", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 8, ExamCode = "MAE101_Q2_5_765432", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 9, ExamCode = "NWC203c_Q1_10_345678", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 10, ExamCode = "NWC203c_Q2_5_876543", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for international business major
            new Exam { ExamId = 11, ExamCode = "ENM401_Q1_10_111222", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 6, CreaterId = 2, CampusId = 1, ExamStatusId = 7, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 12, ExamCode = "ENM401_Q2_5_222111", ExamDuration = "Block 10 (10 weeks)", ExamType = "Reading", SubjectId = 6, CreaterId = 2, CampusId = 1, ExamStatusId = 7, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 13, ExamCode = "ENM401_Q3_7_222333", ExamDuration = "Block 10 (10 weeks)", ExamType = "Writing", SubjectId = 6, CreaterId = 2, CampusId = 1, ExamStatusId = 7, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 14, ExamCode = "ENM401_Q4_9_333111", ExamDuration = "Block 10 (10 weeks)", ExamType = "Listening", SubjectId = 6, CreaterId = 2, CampusId = 1, ExamStatusId = 7, EstimatedTimeTest = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 15, ExamCode = "ECO121_Q1_10_333444", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 16, ExamCode = "ECO121_Q2_5_444333", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 17, ExamCode = "ECO201_Q1_10_555666", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 18, ExamCode = "ECO201_Q2_5_666555", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 19, ExamCode = "ACC101_Q1_10_777888", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 20, ExamCode = "ACC101_Q2_5_888777", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 21, ExamCode = "MKT101_Q1_10_999000", ExamDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 22, ExamCode = "MKT101_Q2_5_000999", ExamDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, ExamStatusId = 1, EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }




        );

        // 9. Seed data for InstructorAssignment table
        modelBuilder.Entity<InstructorAssignment>().HasData(

        // Examiner => Heads of departmant
        new InstructorAssignment { AssignmentId = 1, ExamId = 1, AssignedUserId = 12, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 2, ExamId = 2, AssignedUserId = 12, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 3, ExamId = 3, AssignedUserId = 12, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 4, ExamId = 11, AssignedUserId = 13, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 5, ExamId = 12, AssignedUserId = 13, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
		new InstructorAssignment { AssignmentId = 6, ExamId = 13, AssignedUserId = 13, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
		new InstructorAssignment { AssignmentId = 7, ExamId = 14, AssignedUserId = 13, AssignmentDate = DateTime.Now, AssignStatusId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


		// Head of departmant => Lecturers
		new InstructorAssignment { AssignmentId = 8, ExamId = 1, AssignedUserId = 7, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 9, ExamId = 2, AssignedUserId = 7, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 10, ExamId = 3, AssignedUserId = 7, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 11, ExamId = 11, AssignedUserId = 27, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new InstructorAssignment { AssignmentId = 12, ExamId = 12, AssignedUserId = 27, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
		new InstructorAssignment { AssignmentId = 13, ExamId = 13, AssignedUserId = 27, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
		new InstructorAssignment { AssignmentId = 14, ExamId = 14, AssignedUserId = 27, AssignmentDate = DateTime.Now, AssignStatusId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }



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
            new MenuRole { RoleId = 2, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 3, MenuId = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 12. Seed data for Report table
        modelBuilder.Entity<Report>().HasData(
            new Report { ReportId = 1, AssignmentId = 8, ReportContent = "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", QuestionSolutionDetail = "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", QuestionNumber = 1, Score = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 2, AssignmentId = 9, ReportContent = "In PRN211, question 2 has an outdated logic that leads to incorrect output.", QuestionSolutionDetail = "Revise the logic to ensure it follows the proper algorithmic steps.", QuestionNumber = 2, Score = 9, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 3, AssignmentId = 11, ReportContent = "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", QuestionSolutionDetail = "Provide a more detailed explanation of how supply and demand interact in a market.", QuestionNumber = 3, Score = 9, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 4, AssignmentId = 12, ReportContent = "In ENM401, question 2 has an error in the calculation of equilibrium price.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 4, Score = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new Report { ReportId = 5, AssignmentId = 13, ReportContent = "In ENM401, question 3 has an error in the calculation.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 5, Score = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new Report { ReportId = 6, AssignmentId = 14, ReportContent = "In ENM401, question 4 has an error.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 6, Score = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }



		);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	
}



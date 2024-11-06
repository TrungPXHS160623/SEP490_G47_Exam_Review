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


    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHistory> UserHistories { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; } = null!;

    public virtual DbSet<ReportFile> ReportFiles { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }


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

            entity.HasIndex(e => e.CampusId, "IX_CampusUserSubject_CampusId");

            entity.HasIndex(e => e.SubjectId, "IX_CampusUserSubject_SubjectId");

            entity.HasIndex(e => e.UserId, "IX_CampusUserSubject_UserId");

            entity.HasIndex(e => e.SemesterId, "IX_CampusUserSubject_SemesterId");

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
            entity.HasOne(d => d.Semester).WithMany(p => p.CampusUserSubjects)
               .HasForeignKey(d => d.SemesterId)
               .HasConstraintName("FK_CampusUserSubject_Semesters");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasIndex(e => e.CampusId, "IX_Exams_CampusId");

            entity.HasIndex(e => e.CreaterId, "IX_Exams_CreaterId");

            entity.HasIndex(e => e.ExamStatusId, "IX_Exams_ExamStatusId");

            entity.HasIndex(e => e.SemesterId, "IX_Exams_SemesterId");

            entity.HasIndex(e => e.SubjectId, "IX_Exams_SubjectId");

            entity.Property(e => e.ExamCode).HasMaxLength(50);

            entity.Property(e => e.ExamDuration).HasMaxLength(100);

            entity.Property(e => e.ExamType).HasMaxLength(50);

            entity.HasOne(d => d.Campus)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK_Exams_Campuses");

            entity.HasOne(d => d.Creater)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.CreaterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ExamStatus)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.ExamStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Semester)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_Exams_Semesters");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId);
        });

        modelBuilder.Entity<ExamStatus>(entity =>
        {
            entity.Property(e => e.StatusContent).HasMaxLength(255);
        });


        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.MenuId });

            entity.HasIndex(e => e.MenuId, "IX_MenuRoles_MenuId");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRoles).HasForeignKey(d => d.MenuId);

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasIndex(e => e.DeanId, "IX_Faculties_DeanId");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.FacultyName).HasMaxLength(100);

            entity.HasOne(d => d.Dean)
                .WithMany(p => p.Faculties)
                .HasForeignKey(d => d.DeanId);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasOne(d => d.Exam)
                .WithMany(p => p.Reports)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK_Reports_Exams");
        });
        modelBuilder.Entity<ReportFile>(entity =>
        {
            entity.HasKey(e => e.FileId);

            entity.HasIndex(e => e.ReportId, "IX_ReportFiles_ReportId");

            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.Property(e => e.FilePath).HasMaxLength(500);

            entity.Property(e => e.FileType).HasMaxLength(100);

            entity.HasOne(d => d.Report)
                .WithMany(p => p.ReportFiles)
                .HasForeignKey(d => d.ReportId);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.SubjectCode).HasMaxLength(255);
            entity.Property(e => e.SubjectName).HasMaxLength(255);

            // Cấu hình mối quan hệ với bảng Faculty
            entity.HasOne(s => s.Faculty)
                  .WithMany(f => f.Subjects)
                  .HasForeignKey(s => s.FacultyId)
                  .OnDelete(DeleteBehavior.Cascade);
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

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.ToTable("Semester");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.SemesterName).HasMaxLength(100);

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
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
            new ExamStatus { ExamStatusId = 1, StatusContent = "Unassigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 2, StatusContent = "Assigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 3, StatusContent = "Awaiting Lecturer Confirm", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 4, StatusContent = "Reviewing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 5, StatusContent = "Error", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 6, StatusContent = "OK", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 7, StatusContent = "Completed", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
            new ExamStatus { ExamStatusId = 8, StatusContent = "Rejected", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // Seed data for Faculty table
        modelBuilder.Entity<Faculty>().HasData(
			new Faculty { FacultyId = 1, FacultyName = "IB", Description = "The program focuses on international business practices and global market dynamics.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 2, FacultyName = "SE", Description = "A field dedicated to the principles and practices of software development and engineering.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 3, FacultyName = "AI", Description = "This discipline focuses on artificial intelligence, machine learning, and data-driven decision-making.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 4, FacultyName = "MC", Description = "A program combining multimedia, communications, and management for the digital media industry.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }, 
		    new Faculty { FacultyId = 5, FacultyName = "ENG", Description = "Dedicated to English language studies and cross-cultural communication.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 6, FacultyName = "JPN", Description = "Specializes in Japanese language, culture, and international relations.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 7, FacultyName = "KOR", Description = "Focuses on Korean language, culture, and regional studies.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 8, FacultyName = "CHN", Description = "Provides training in Chinese language, culture, and business practices.", DeanId = null, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );


        // 3. Seed data for UserRole table
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { RoleId = 1, RoleName = "Admin", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 2, RoleName = "Examiner", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 3, RoleName = "Lecturer", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 4, RoleName = "Head of Department", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new UserRole { RoleId = 5, RoleName = "Curriculum Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );


        // 4. Seed data for User table
        modelBuilder.Entity<User>().HasData(

            // Seed data for role admin
            new User { UserId = 1, Mail = "admin@fpt.edu.vn", CampusId = 1, RoleId = 1, FullName = "Admin", PhoneNumber = "0123456789", EmailFe = null, DateOfBirth = new DateTime(1980, 1, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


        // Seed data for Examiner
            new User { UserId = 2, Mail = "lienkt@fpt.edu.vn", CampusId = 1, RoleId = 2, FullName = "Liên Kết", PhoneNumber = "0123456789", EmailFe = "lienkt@fe.edu.vn", DateOfBirth = new DateTime(1990, 2, 1), Gender = false, Address = "TP Hồ Chí Minh", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 3, Mail = "hunglthe160235@fpt.edu.vn", CampusId = 1, RoleId = 2, FullName = "Hưng Lê", PhoneNumber = "0123456789", EmailFe = "hunglthe160235@fe.edu.vn", DateOfBirth = new DateTime(1995, 7, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 4, Mail = "hoanglm@fpt.edu.vn", CampusId = 2, RoleId = 2, FullName = "Hoàng Lâm", PhoneNumber = "0123456789", EmailFe = "hoanglm@fe.edu.vn", DateOfBirth = new DateTime(1992, 3, 1), Gender = true, Address = "Đà Nẵng", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


            // Seed data for Lecturer
            new User { UserId = 5, Mail = "lanhbt@fpt.edu.vn", CampusId = 1, RoleId = 3, FullName = "Lành Bích", PhoneNumber = "0123456789", EmailFe = "lanhbt@fe.edu.vn", DateOfBirth = new DateTime(1989, 8, 1), Gender = false, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 6, Mail = "quanpt@fpt.edu.vn", CampusId = 1, RoleId = 3, FullName = "Quân Phạm", PhoneNumber = "0123456789", EmailFe = "quanpt@fe.edu.vn", DateOfBirth = new DateTime(1992, 1, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 7, Mail = "trungpxhs160623@fpt.edu.vn", CampusId = 1, RoleId = 3, FullName = "Trung Phạm", PhoneNumber = "0123456789", EmailFe = "trungpxhs160623@fe.edu.vn", DateOfBirth = new DateTime(1995, 2, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new User { UserId = 8, Mail = "khoadt@fpt.edu.vn", CampusId = 2, RoleId = 3, FullName = "Khoa Đạt", PhoneNumber = "0123456789", EmailFe = "khoadt@fe.edu.vn", DateOfBirth = new DateTime(1988, 9, 1), Gender = true, Address = "Hải Phòng", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


			// Seed data for Head of Department
			new User { UserId = 9, Mail = "namlh@fpt.edu.vn", CampusId = 1, RoleId = 4, FullName = "Nam Lê", PhoneNumber = "0123456789", EmailFe = "namlh@fe.edu.vn", DateOfBirth = new DateTime(1988, 3, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 10, Mail = "quangnv@fpt.edu.vn", CampusId = 1, RoleId = 4, FullName = "Quang Nguyễn", PhoneNumber = "0123456789", EmailFe = "quangnv@fe.edu.vn", DateOfBirth = new DateTime(1986, 4, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 11, Mail = "tuanlmhe161245@fpt.edu.vn", CampusId = 1, RoleId = 4, FullName = "Tuấn Lê", PhoneNumber = "0123456789", EmailFe = "tuanlmhe161245@fe.edu.vn", DateOfBirth = new DateTime(1985, 1, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 12, Mail = "tungtkHS163077@fpt.edu.vn", CampusId = 1, RoleId = 4, FullName = "Tùng Khoa", PhoneNumber = "0123456789", EmailFe = "tungtkHS163077@fe.edu.vn", DateOfBirth = new DateTime(1995, 2, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new User { UserId = 13, Mail = "huylt@fpt.edu.vn", CampusId = 2, RoleId = 4, FullName = "Huy Lê", PhoneNumber = "0123456789", EmailFe = "huylt@fe.edu.vn", DateOfBirth = new DateTime(1985, 5, 1), Gender = true, Address = "TP Hồ Chí Minh", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new User { UserId = 14, Mail = "tuanpv@fpt.edu.vn", CampusId = 2, RoleId = 4, FullName = "Tuấn Phạm", PhoneNumber = "0123456789", EmailFe = "tuanpv@fe.edu.vn", DateOfBirth = new DateTime(1984, 6, 1), Gender = true, Address = "TP Hồ Chí Minh", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


			// Seed data for Program Developer
			new User { UserId = 15, Mail = "phucdt@fpt.edu.vn", CampusId = 1, RoleId = 5, FullName = "Phúc Đạt", PhoneNumber = "0123456789", EmailFe = null, DateOfBirth = new DateTime(1989, 1, 1), Gender = true, Address = "Hà Nội", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new User { UserId = 16, Mail = "thanhnt@fpt.edu.vn", CampusId = 2, RoleId = 5, FullName = "Thanh Nguyễn", PhoneNumber = "0123456789", EmailFe = null, DateOfBirth = new DateTime(1990, 2, 1), Gender = false, Address = "TP Hồ Chí Minh", IsActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }          
        );

        // 6. Seed data for Subject table
        modelBuilder.Entity<Subject>().HasData(

            // Seed data for software engineering major
            new Subject { SubjectId = 1, FacultyId = 2, SubjectCode = "PRN211", SubjectName = "Basic Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 2, FacultyId = 2, SubjectCode = "PRN221", SubjectName = "Advanced Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 3, FacultyId = 2, SubjectCode = "PRN231", SubjectName = "Building Cross-Platform Back-End Application With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 4, FacultyId = 2, SubjectCode = "MAE101", SubjectName = "Mathematics for Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 5, FacultyId = 2, SubjectCode = "NWC203c", SubjectName = "Computer Networking", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for international business major
            new Subject { SubjectId = 6, FacultyId = 1, SubjectCode = "ENM401", SubjectName = "Business English", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 7, FacultyId = 1, SubjectCode = "ECO121", SubjectName = "Basic Macro Economics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 8, FacultyId = 1, SubjectCode = "ECO201", SubjectName = "International Economics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 9, FacultyId = 1, SubjectCode = "ACC101", SubjectName = "Principles of Accounting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 10, FacultyId = 1, SubjectCode = "MKT101", SubjectName = "Marketing Principles", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        modelBuilder.Entity<CampusUserSubject>().HasData(

            //Seed data for Heads of Department of Ha Noi campus
            new CampusUserSubject { Id = 1, SubjectId = 1, CampusId = 1, UserId = 9, SemesterId = 1 },
            new CampusUserSubject { Id = 2, SubjectId = 2, CampusId = 1, UserId = 9, SemesterId = 1 },
            new CampusUserSubject { Id = 3, SubjectId = 3, CampusId = 1, UserId = 9, SemesterId = 1 },
			new CampusUserSubject { Id = 4, SubjectId = 4, CampusId = 1, UserId = 10, SemesterId = 1 },
			new CampusUserSubject { Id = 5, SubjectId = 5, CampusId = 1, UserId = 10, SemesterId = 1 },

			new CampusUserSubject { Id = 6, SubjectId = 6, CampusId = 1, UserId = 11, SemesterId = 2 },
            new CampusUserSubject { Id = 7, SubjectId = 7, CampusId = 1, UserId = 11, SemesterId = 2 },
            new CampusUserSubject { Id = 8, SubjectId = 8, CampusId = 1, UserId = 11, SemesterId = 2 },
            

            //Seed data for Lecturer of Ha Noi campus
            new CampusUserSubject { Id = 9, SubjectId = 1, CampusId = 1, UserId = 5, IsLecturer = true, SemesterId = 1 },
            new CampusUserSubject { Id = 10, SubjectId = 2, CampusId = 1, UserId = 5, IsLecturer = true, SemesterId = 1 },
            new CampusUserSubject { Id = 11, SubjectId = 3, CampusId = 1, UserId = 5, IsLecturer = true, SemesterId = 1 },
			new CampusUserSubject { Id = 12, SubjectId = 4, CampusId = 1, UserId = 6, IsLecturer = true, SemesterId = 1 },
			new CampusUserSubject { Id = 13, SubjectId = 5, CampusId = 1, UserId = 6, IsLecturer = true, SemesterId = 1 },

			new CampusUserSubject { Id = 14, SubjectId = 6, CampusId = 1, UserId = 7, IsLecturer = true, SemesterId = 2 },
            new CampusUserSubject { Id = 15, SubjectId = 7, CampusId = 1, UserId = 7, IsLecturer = true, SemesterId = 2 },
            new CampusUserSubject { Id = 16, SubjectId = 8, CampusId = 1, UserId = 7, IsLecturer = true, SemesterId = 2 },
        

            // Seed data for Heads of Department of Da Nang campus
            new CampusUserSubject { Id = 17, SubjectId = 1, CampusId = 2, UserId = 8, SemesterId = 1 },
            new CampusUserSubject { Id = 18, SubjectId = 2, CampusId = 2, UserId = 8, SemesterId = 1 },
            new CampusUserSubject { Id = 19, SubjectId = 3, CampusId = 2, UserId = 8, SemesterId = 1 },
            new CampusUserSubject { Id = 20, SubjectId = 4, CampusId = 2, UserId = 8, SemesterId = 1 },
            new CampusUserSubject { Id = 21, SubjectId = 5, CampusId = 2, UserId = 8, SemesterId = 1 }
        );

        // 7. Seed data for Exam table
        modelBuilder.Entity<Exam>().HasData(

            // Ha Noi's Examiners create exams
            // Seed data for software engineering major
            new Exam { ExamId = 1, ExamCode = "PRN211_Q1_10_123456", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "PRN211_Q2_5_654321", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 3, ExamCode = "PRN221_Q1_10_789012", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 4, ExamCode = "PRN221_Q2_5_210987", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 5, ExamCode = "PRN231_Q1_10_345678", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 6, ExamCode = "PRN231_Q2_5_876543", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 7, ExamCode = "MAE101_Q1_10_234567", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 8, ExamCode = "MAE101_Q2_5_765432", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 9, ExamCode = "NWC203c_Q1_10_345678", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 2, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), CreateDate = DateTime.Now, UpdateDate = DateTime.Now,  },
            new Exam { ExamId = 10, ExamCode = "NWC203c_Q2_5_876543", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 3, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 25), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for international business major
            new Exam { ExamId = 11, ExamCode = "ENM401_Q1_10_111222", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 12, ExamCode = "ENM401_Q2_5_222111", ExamDuration = "30'", TermDuration = "Block 10 (10 weeks)", ExamType = "Reading", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 13, ExamCode = "ENM401_Q3_7_222333", ExamDuration = "60'", TermDuration = "Block 10 (10 weeks)", ExamType = "Writing", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10..", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 14, ExamCode = "ENM401_Q4_9_333111", ExamDuration = "30'", TermDuration = "Block 10 (10 weeks)", ExamType = "Listening", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 15, ExamCode = "ECO121_Q1_10_333444", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 16, ExamCode = "ECO121_Q2_5_444333", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 17, ExamCode = "ECO201_Q1_10_555666", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 18, ExamCode = "ECO201_Q2_5_666555", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 19, ExamCode = "ACC101_Q1_10_777888", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 20, ExamCode = "ACC101_Q2_5_888777", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 21, ExamCode = "MKT101_Q1_10_999000", ExamDuration = "90'", TermDuration = "Block 10 (10 weeks)", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 22, ExamCode = "MKT101_Q2_5_000999", ExamDuration = "60'", TermDuration = "Block 5 (5 weeks)", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }


        );
        // 10. Seed data for Menu table
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuLink = "/usermanagement", MenuName = "User Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 2, MenuLink = "/Admin/History", MenuName = "User Log", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 3, MenuLink = "/Examiner/ExamList", MenuName = "Exam List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 4, MenuLink = "/HeadDepartment/ExamList", MenuName = "Exam Assign", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 5, MenuLink = "/Lecture/ExamList", MenuName = "List Asigned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 6, MenuLink = "/HeadDepartment/Report", MenuName = "View Report", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 7, MenuLink = "/HeadDepartment/ExamStatus", MenuName = "Exam Status", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 10, MenuLink = "/Examiner/usermanagement", MenuName = "Head Department Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 8, MenuLink = "/Admin/CampusManagement", MenuName = "Campus Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 11, MenuLink ="/Examiner/Create", MenuName = "Create Exam", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 12, MenuLink = "/HeadDepartment/lectureManagement", MenuName = "Lecture Management(UnderContrucst)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 13, MenuLink = "/Examiner/Statistical", MenuName = "Statistical", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 14, MenuLink = "/Admin/SemesterManagement", MenuName = "Semester Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 9, MenuLink = "/Admin/SubjectManagement", MenuName = "Subject Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // 11. Seed data for MenuRole table
        modelBuilder.Entity<MenuRole>().HasData(
            new MenuRole { RoleId = 1, MenuId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 9, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 14, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 10, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 6, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 7, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 11, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 13, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 12, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 3, MenuId = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
        );

        // 12. Seed data for Report table
        modelBuilder.Entity<Report>().HasData(
            new Report { ReportId = 1, ExamId = 1, ReportContent = "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", QuestionSolutionDetail = "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", QuestionNumber = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 2, ExamId = 2, ReportContent = "In PRN211, question 2 has an outdated logic that leads to incorrect output.", QuestionSolutionDetail = "Revise the logic to ensure it follows the proper algorithmic steps.", QuestionNumber = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 3, ExamId = 11, ReportContent = "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", QuestionSolutionDetail = "Provide a more detailed explanation of how supply and demand interact in a market.", QuestionNumber = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 4, ExamId = 12, ReportContent = "In ENM401, question 2 has an error in the calculation of equilibrium price.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 5, ExamId = 13, ReportContent = "In ENM401, question 3 has an error in the calculation.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Report { ReportId = 6, ExamId = 14, ReportContent = "In ENM401, question 4 has an error.", QuestionSolutionDetail = "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", QuestionNumber = 6, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

         );


        modelBuilder.Entity<Semester>().HasData(
            new Semester { SemesterId = 1, SemesterName = "Sp24", StartDate = new DateTime(2024, 1, 16), EndDate = new DateTime(2024, 5, 15), IsActive = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
            new Semester { SemesterId = 2, SemesterName = "Su24", StartDate = new DateTime(2024, 6, 1), EndDate = new DateTime(2024, 8, 15), IsActive = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
            new Semester { SemesterId = 3, SemesterName = "Fa24", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2025, 1, 15), IsActive = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
         );






        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}



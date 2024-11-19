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

    public virtual DbSet<CampusUserFaculty> CampusUserFaculties { get; set; }


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

        modelBuilder.Entity<CampusUserFaculty>(entity =>
        {
            entity.ToTable("CampusUserFaculty");

            // Khóa chính
            entity.HasKey(e => e.Id);

            // Indexes cho các cột liên kết
            entity.HasIndex(e => e.CampusId, "IX_CampusUserFaculty_CampusId");
            entity.HasIndex(e => e.UserId, "IX_CampusUserFaculty_UserId");
            entity.HasIndex(e => e.FacultyId, "IX_CampusUserFaculty_FacultyId");

            // Thiết lập khóa ngoại
            entity.HasOne(d => d.Campus)
                  .WithMany(p => p.CampusUserFaculties)
                  .HasForeignKey(d => d.CampusId)
                  .HasConstraintName("FK_CampusUserFaculty_Campuses");

            entity.HasOne(d => d.User)
                  .WithMany(p => p.CampusUserFaculties)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK_CampusUserFaculty_Users");

            entity.HasOne(d => d.Faculty)
                  .WithMany(p => p.CampusUserFaculties)
                  .HasForeignKey(d => d.FacultyId)
                  .HasConstraintName("FK_CampusUserFaculty_Faculties");
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

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.FacultyName).HasMaxLength(100);


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

            entity.Property(e => e.FilePath).HasColumnType("nvarchar(MAX)");

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
            new ExamStatus { ExamStatusId = 4, StatusContent = "Planned", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 5, StatusContent = "Error", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 6, StatusContent = "OK", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 7, StatusContent = "Completed", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new ExamStatus { ExamStatusId = 8, StatusContent = "Rejected", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // Seed data for Faculty table
        modelBuilder.Entity<Faculty>().HasData(
            new Faculty { FacultyId = 1, FacultyName = "Artificial Intelligence", Description = "The study and creation of systems that can perform tasks requiring human-like intelligence, such as learning and problem-solving.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 2, FacultyName = "BLOC", Description = "A field focusing on core skills in business, law, operations, and communication for professional success.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 3, FacultyName = "Business Administration", Description = "The study of managing and overseeing business operations, including planning, organization, and leadership", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 4, FacultyName = "Chinese", Description = "The study of the Chinese language, culture, and communication skills.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 5, FacultyName = "Computer Science", Description = "The study of computation, algorithms, programming, and the design of software and hardware systems.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 6, FacultyName = "Computing Fundamental", Description = "An introduction to core computing concepts, including basic programming, data processing, and system operations.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 7, FacultyName = "English", Description = "The study of the English language, including grammar, literature, and communication skills.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 8, FacultyName = "English Preparation Course", Description = "A course designed to enhance language skills in reading, writing, speaking, and listening, preparing students for academic or professional English proficiency.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 9, FacultyName = "Extra Classes", Description = "Additional sessions designed to provide supplementary learning in various subjects to reinforce or expand students' knowledge and skills.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 10, FacultyName = "Finance", Description = "The study of managing money, investments, financial systems, and the principles of budgeting and financial decision-making.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 11, FacultyName = "Graduate", Description = "A program or course designed for students who have completed an undergraduate degree, focusing on advanced studies and specialized knowledge in a particular field.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 12, FacultyName = "Graphic Design", Description = "The art of creating visual content to communicate messages, combining elements like typography, images, and colors to design logos, advertisements, websites, and more.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 13, FacultyName = "Information Assurance", Description = "The practice of protecting and managing information systems to ensure their confidentiality, integrity, and availability, focusing on risk management and security measures.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 14, FacultyName = "Information Technology Specialization", Description = "A focused study of advanced topics in IT, such as network management, cybersecurity, software development, and database administration.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 15, FacultyName = "Japanese", Description = "The study of the Japanese language, including its grammar, vocabulary, writing systems, and cultural aspects.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 16, FacultyName = "Korean", Description = "The study of the Korean language, including its grammar, vocabulary, writing systems (Hangul), and cultural nuances.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 17, FacultyName = "LAB", Description = "A practical learning environment where students conduct experiments, apply theoretical knowledge, and gain hands-on experience in various scientific or technical fields.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 18, FacultyName = "Little UK", Description = "A program or initiative that offers an immersive learning experience focused on British culture, language, and educational practices, often designed to enhance students' understanding of the UK.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 19, FacultyName = "Management", Description = "The study of planning, organizing, and overseeing resources and processes to achieve organizational goals efficiently and effectively.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 20, FacultyName = "Mathematics", Description = "The study of numbers, quantities, shapes, and patterns, focusing on problem-solving, logic, and abstract reasoning.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 21, FacultyName = "Multimedia Communications", Description = "The study of using various media formats—such as text, audio, video, and graphics—to communicate information effectively across different platforms and technologies.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 22, FacultyName = "On the job training", Description = "A practical learning process where employees acquire skills and knowledge by performing tasks and duties in a real work environment under the guidance of experienced colleagues or supervisors.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 23, FacultyName = "OR", Description = "The application of mathematical models, statistical analysis, and optimization techniques to solve complex decision-making problems and improve efficiency in business, logistics, and other systems.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 24, FacultyName = "Physical Training", Description = "The practice of improving physical fitness and performance through exercises, workouts, and conditioning to enhance strength, endurance, and overall health.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 25, FacultyName = "SAP", Description = "A leading enterprise resource planning (ERP) software that helps organizations manage business processes by integrating key functions like finance, supply chain, human resources, and customer relationships.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 26, FacultyName = "Soft Skill", Description = "Non-technical skills that relate to how individuals interact with others, such as communication, teamwork, problem-solving, adaptability, and leadership.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 27, FacultyName = "Software Engineering", Description = "The application of engineering principles to the design, development, testing, and maintenance of software systems, ensuring they are efficient, reliable, and scalable.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 28, FacultyName = "Start Your Business", Description = "A program or course focused on teaching the fundamentals of launching and managing a new business, including idea development, business planning, marketing, and financial management.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 29, FacultyName = "Traditional Instrument", Description = "The study and practice of playing musical instruments that are indigenous to specific cultures, often passed down through generations, such as the violin, guitar, sitar, or drums.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Faculty { FacultyId = 30, FacultyName = "Vietnamese", Description = "The study of the Vietnamese language, including its grammar, vocabulary, pronunciation, and cultural context.", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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

            // Seed data for Artificial Intelligence
            new Subject { SubjectId = 1, FacultyId = 1, SubjectCode = "ADY201m", SubjectName = "Data Science with Python & SQL", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 2, FacultyId = 1, SubjectCode = "AID301c", SubjectName = "AI in Production", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 3, FacultyId = 1, SubjectCode = "AIE301m", SubjectName = "AI for Trading", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 4, FacultyId = 1, SubjectCode = "AIG202c", SubjectName = "Artificial Intelligence", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 5, FacultyId = 1, SubjectCode = "AIH301m", SubjectName = "AI in Healthcare", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Subject { SubjectId = 6, FacultyId = 1, SubjectCode = "AIL303m", SubjectName = "Machine Learning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 7, FacultyId = 1, SubjectCode = "AIM301m", SubjectName = "AI for Medicine", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 8, FacultyId = 1, SubjectCode = "ASR301c", SubjectName = "AI for Scientific Research", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 9, FacultyId = 1, SubjectCode = "BDI301c", SubjectName = "Big Data", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 10, FacultyId = 1, SubjectCode = "BDI302c", SubjectName = "Big Data", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 11, FacultyId = 1, SubjectCode = "CPV301", SubjectName = "Computer Vision", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 12, FacultyId = 1, SubjectCode = "DAP391m", SubjectName = "AI-DS Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 13, FacultyId = 1, SubjectCode = "DAT301m", SubjectName = "AI Development with TensorFlow", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 14, FacultyId = 1, SubjectCode = "DBM302m", SubjectName = "Data Mining", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 15, FacultyId = 1, SubjectCode = "DPL301m", SubjectName = "Deep Learing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 16, FacultyId = 1, SubjectCode = "DPL302m", SubjectName = "Deep Learning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 17, FacultyId = 1, SubjectCode = "DSR301m", SubjectName = "Applied Data Science with R", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 18, FacultyId = 1, SubjectCode = "DWP301c", SubjectName = "Web Development with Python", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 19, FacultyId = 1, SubjectCode = "NLP301c", SubjectName = "Natural Language Processing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 20, FacultyId = 1, SubjectCode = "PRP201c", SubjectName = "Python programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 21, FacultyId = 1, SubjectCode = "REL301m", SubjectName = "Reinforcement Learning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },



            // Seed data for BLOC
            new Subject { SubjectId = 22, FacultyId = 2, SubjectCode = "BDP306b", SubjectName = "Final Project - Blockchain Development in Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Business Administration
            new Subject { SubjectId = 23, FacultyId = 3, SubjectCode = "ACC101", SubjectName = "Principles of Accounting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 24, FacultyId = 3, SubjectCode = "ACC302", SubjectName = "Managerial Accounting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 25, FacultyId = 3, SubjectCode = "ACC305", SubjectName = "Financial Statement Analysis", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 26, FacultyId = 3, SubjectCode = "ADS301m", SubjectName = "Google Ads và Seo", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 27, FacultyId = 3, SubjectCode = "CAA201", SubjectName = "Communications and advertising", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 28, FacultyId = 3, SubjectCode = "CIH201", SubjectName = "Contemporary issues in hotel and tourism management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 29, FacultyId = 3, SubjectCode = "DMA301m", SubjectName = "Digital Marketing Analytics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 30, FacultyId = 3, SubjectCode = "DMS301m", SubjectName = "Digital Marketing Strategy", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 31, FacultyId = 3, SubjectCode = "ECO102", SubjectName = "Business Environment", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 32, FacultyId = 3, SubjectCode = "ECO111", SubjectName = "Microeconomics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 33, FacultyId = 3, SubjectCode = "ECO201", SubjectName = "International Economics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 34, FacultyId = 3, SubjectCode = "EXE101", SubjectName = "Experiential Entrepreneurship 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 35, FacultyId = 3, SubjectCode = "EXE201", SubjectName = "Experiential Entrepreneurship 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 36, FacultyId = 3, SubjectCode = "FIM302c", SubjectName = "Financial modelling", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 37, FacultyId = 3, SubjectCode = "FIN201", SubjectName = "Monetary Economics and Global Economy", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 38, FacultyId = 3, SubjectCode = "FIN202", SubjectName = "Principles of Corporate Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 39, FacultyId = 3, SubjectCode = "FIN301", SubjectName = "Financial Markets and Institutions", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 40, FacultyId = 3, SubjectCode = "FIN303", SubjectName = "Advanced Corporate Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 41, FacultyId = 3, SubjectCode = "FIN306c", SubjectName = "Financial Reporting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 42, FacultyId = 3, SubjectCode = "FIN308", SubjectName = "International Corporate Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 43, FacultyId = 3, SubjectCode = "FIN402", SubjectName = "Derivatives", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 44, FacultyId = 3, SubjectCode = "FIN403", SubjectName = "Mergers and Acquisitions", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 45, FacultyId = 3, SubjectCode = "HRM201c", SubjectName = "Human Resource Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 46, FacultyId = 3, SubjectCode = "IBC201", SubjectName = "Cross Cultural Management and Negotiation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 47, FacultyId = 3, SubjectCode = "IBF301", SubjectName = "International Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 48, FacultyId = 3, SubjectCode = "IBI101", SubjectName = "Introduction to International business", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 49, FacultyId = 3, SubjectCode = "IBS301m", SubjectName = "International Business Strategy", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 50, FacultyId = 3, SubjectCode = "IEI301", SubjectName = "Import and Export", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 51, FacultyId = 3, SubjectCode = "IIP301", SubjectName = "International payment", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 52, FacultyId = 3, SubjectCode = "IPR102", SubjectName = "Intellectual Property Rights", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 53, FacultyId = 3, SubjectCode = "LAW102", SubjectName = "Business Law and Ethics Fundamentals", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 54, FacultyId = 3, SubjectCode = "LAW201c", SubjectName = "International Business Law", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 55, FacultyId = 3, SubjectCode = "LOG311", SubjectName = "Customs Operations", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 56, FacultyId = 3, SubjectCode = "MGT103", SubjectName = "Introduction to Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 57, FacultyId = 3, SubjectCode = "MKT101", SubjectName = "Marketing Principles", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 58, FacultyId = 3, SubjectCode = "MKT201", SubjectName = "Consumer Behavior", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 59, FacultyId = 3, SubjectCode = "MKT202", SubjectName = "Services Marketing Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 60, FacultyId = 3, SubjectCode = "MKT205c", SubjectName = "International Marketing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 61, FacultyId = 3, SubjectCode = "MKT208c", SubjectName = "Social media marketing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 62, FacultyId = 3, SubjectCode = "MKT209m", SubjectName = "Content Marketing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 63, FacultyId = 3, SubjectCode = "MKT301", SubjectName = "Marketing Research", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 64, FacultyId = 3, SubjectCode = "MKT304", SubjectName = "Integrated Marketing Communications", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 65, FacultyId = 3, SubjectCode = "MKT309m ", SubjectName = "Omnichannel marketing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 66, FacultyId = 3, SubjectCode = "OBE102c", SubjectName = "Organizational Behavior", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 67, FacultyId = 3, SubjectCode = "RES201", SubjectName = "Food Preparation & Science", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 68, FacultyId = 3, SubjectCode = "RES213", SubjectName = "Wines, Beers, Spirits 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 69, FacultyId = 3, SubjectCode = "RES301", SubjectName = "Food and Beverage Cost Control", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 70, FacultyId = 3, SubjectCode = "RMB301", SubjectName = "Business Research Methods", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 71, FacultyId = 3, SubjectCode = "RMB302", SubjectName = "Research Methods & Quantitative Analysis", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 72, FacultyId = 3, SubjectCode = "SAL301", SubjectName = "Professional Selling", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 73, FacultyId = 3, SubjectCode = "SCM201", SubjectName = "Supply Chain Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 74, FacultyId = 3, SubjectCode = "SCM301m", SubjectName = "Procurement and Global Sourcing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 75, FacultyId = 3, SubjectCode = "SYB302c", SubjectName = "Entrepreneurship", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


            // Seed data for Computing Fundamental
            new Subject { SubjectId = 76, FacultyId = 6, SubjectCode = "CSD201", SubjectName = "Data Structures and Algorithms", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 77, FacultyId = 6, SubjectCode = "CSD201-EX", SubjectName = "Data Structures and Algorithms", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 78, FacultyId = 6, SubjectCode = "CSD203", SubjectName = "Data Structures and Algorithm with Python", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 79, FacultyId = 6, SubjectCode = "DBI202", SubjectName = "Introduction to Databases", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 80, FacultyId = 6, SubjectCode = "DBI202-EX", SubjectName = "Introduction to Databases", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 81, FacultyId = 6, SubjectCode = "FER202", SubjectName = "Front-End web development with React", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 82, FacultyId = 6, SubjectCode = "JFE301", SubjectName = "Japanese IT Fundamentals", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 83, FacultyId = 6, SubjectCode = "OSG202", SubjectName = "Operating Systems", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 84, FacultyId = 6, SubjectCode = "PFP191", SubjectName = "Programming Fundamentals with Python", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 85, FacultyId = 6, SubjectCode = "PRE201c", SubjectName = "Excel Skills for Business", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 86, FacultyId = 6, SubjectCode = "PRF192", SubjectName = "Programming Fundamentals", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 87, FacultyId = 6, SubjectCode = "PRF192-EX", SubjectName = "Programming Fundamentals", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 88, FacultyId = 6, SubjectCode = "PRJ301", SubjectName = "Java Web Application Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 89, FacultyId = 6, SubjectCode = "PRJ301-EX", SubjectName = "Java Web Application Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 90, FacultyId = 6, SubjectCode = "PRJ302", SubjectName = "Java Web Application Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 91, FacultyId = 6, SubjectCode = "PRN212", SubjectName = "Basis Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 92, FacultyId = 6, SubjectCode = "PRN221", SubjectName = "Advanced Cross-Platform Application Programming With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 93, FacultyId = 6, SubjectCode = "PRN231", SubjectName = "Building Cross-Platform Back-End Application With .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 94, FacultyId = 6, SubjectCode = "PRN292c", SubjectName = "C# và .NET", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 95, FacultyId = 6, SubjectCode = "PRO192", SubjectName = "Object-Oriented Programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 96, FacultyId = 6, SubjectCode = "PRO192-EX", SubjectName = "Object-Oriented Programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 97, FacultyId = 6, SubjectCode = "PRO192c", SubjectName = "Object Oriented Programming with Java", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 98, FacultyId = 6, SubjectCode = "PRU212", SubjectName = "C# Programming and Unity", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 99, FacultyId = 6, SubjectCode = "SDN302", SubjectName = "Server-Side development with NodeJS, Express, and MongoDB", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 100, FacultyId = 6, SubjectCode = "WDP301", SubjectName = "Web Development Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 101, FacultyId = 6, SubjectCode = "WED201c", SubjectName = "Web Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for English
            new Subject { SubjectId = 102, FacultyId = 7, SubjectCode = "CHN113", SubjectName = "Elementary Chinese 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 103, FacultyId = 7, SubjectCode = "CHN113-EX", SubjectName = "Elementary Chinese 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 104, FacultyId = 7, SubjectCode = "CHN123", SubjectName = "Elementary Chinese 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 105, FacultyId = 7, SubjectCode = "CHN123-EX", SubjectName = "Elementary Chinese 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 106, FacultyId = 7, SubjectCode = "CHN132c", SubjectName = "Elementary Chinese 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 107, FacultyId = 7, SubjectCode = "CMC201c", SubjectName = "Creative Writing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 108, FacultyId = 7, SubjectCode = "EAW212", SubjectName = "Academic English Writing 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 109, FacultyId = 7, SubjectCode = "EAW222", SubjectName = "Academic English Writing 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 110, FacultyId = 7, SubjectCode = "EBC301c", SubjectName = "Business English Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 111, FacultyId = 7, SubjectCode = "ECN101", SubjectName = "Integrated Chinese 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 112, FacultyId = 7, SubjectCode = "ECN221", SubjectName = "Integrated Chinese 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 113, FacultyId = 7, SubjectCode = "ECR202", SubjectName = "Critical Reading in English", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 114, FacultyId = 7, SubjectCode = "ELI301", SubjectName = "Translation 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 115, FacultyId = 7, SubjectCode = "ELI401", SubjectName = "Translation 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 116, FacultyId = 7, SubjectCode = "ELR301", SubjectName = "Research Methods", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 117, FacultyId = 7, SubjectCode = "ELS401c", SubjectName = "Academic Listening and Speaking", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 118, FacultyId = 7, SubjectCode = "ENB301", SubjectName = "Business Writing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 119, FacultyId = 7, SubjectCode = "ENG303", SubjectName = "Advanced English Grammar", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 120, FacultyId = 7, SubjectCode = "ENM211c", SubjectName = "Business English Communication 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 121, FacultyId = 7, SubjectCode = "ENM301", SubjectName = "Intermediate Business English", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 122, FacultyId = 7, SubjectCode = "ENM302", SubjectName = "Business English– Level 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 123, FacultyId = 7, SubjectCode = "ENM401", SubjectName = "Upper Intermediate Business English", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 124, FacultyId = 7, SubjectCode = "ENP102", SubjectName = "English phonetics and phonology in use", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 125, FacultyId = 7, SubjectCode = "ENW492c", SubjectName = "Writing Research Papers", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 126, FacultyId = 7, SubjectCode = "EPE301c", SubjectName = "Professional Ethics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 127, FacultyId = 7, SubjectCode = "ERW412", SubjectName = "English Read-Think-Write 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 128, FacultyId = 7, SubjectCode = "LIT301", SubjectName = "British and American Literature", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 129, FacultyId = 7, SubjectCode = "LTG202", SubjectName = "Introduction to linguistics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 130, FacultyId = 7, SubjectCode = "SEM101", SubjectName = "Semantics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 131, FacultyId = 7, SubjectCode = "SSC302c", SubjectName = "Advanced Presentation skills", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


            // Seed data for English Preparation Course
            new Subject { SubjectId = 132, FacultyId = 8, SubjectCode = "ENT104", SubjectName = "English 2 (Top Notch 1)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 133, FacultyId = 8, SubjectCode = "ENT203", SubjectName = "English 3 (Top Notch 2)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 134, FacultyId = 8, SubjectCode = "TRS401", SubjectName = "English 4 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 135, FacultyId = 8, SubjectCode = "TRS501", SubjectName = "English 5 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 136, FacultyId = 8, SubjectCode = "TRS601", SubjectName = "English 6 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 137, FacultyId = 8, SubjectCode = "TRS601-CULI-TL", SubjectName = "English 6 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 138, FacultyId = 8, SubjectCode = "TRS601-KBU-TL", SubjectName = "English 6 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 139, FacultyId = 8, SubjectCode = "TRS601-TAR UMT-ML", SubjectName = "English 6 (University success)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },



            // Seed data for Extra Classes
            new Subject { SubjectId = 140, FacultyId = 9, SubjectCode = "EXE101g", SubjectName = "Group Experiential Entrepreneurship 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 141, FacultyId = 9, SubjectCode = "EXE201g", SubjectName = "Experiential Entrepreneurship 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Graduate
            new Subject { SubjectId = 142, FacultyId = 11, SubjectCode = "AIP490", SubjectName = "AI Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 143, FacultyId = 11, SubjectCode = "AIP491", SubjectName = "AI Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 144, FacultyId = 11, SubjectCode = "ELT492", SubjectName = "Graduation Thesis - English studies", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 145, FacultyId = 11, SubjectCode = "GDP491", SubjectName = "Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 146, FacultyId = 11, SubjectCode = "GDP492", SubjectName = "Capstone Project Graphic Design - Animation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 147, FacultyId = 11, SubjectCode = "GDP493", SubjectName = "Capstone Project Graphic Design - Interaction Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 148, FacultyId = 11, SubjectCode = "GDP494", SubjectName = "Capstone Project Graphic Design - Communication Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 149, FacultyId = 11, SubjectCode = "GRA497", SubjectName = "Capstone Project - Multimedia and Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 150, FacultyId = 11, SubjectCode = "GRF491", SubjectName = "Graduation Thesis - Finance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 151, FacultyId = 11, SubjectCode = "GRI491", SubjectName = "Graduation Thesis - International Business", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 152, FacultyId = 11, SubjectCode = "GRM491", SubjectName = "Graduation Thesis - Marketing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 153, FacultyId = 11, SubjectCode = "GRP490", SubjectName = "Graduation thesis (Business plan)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 154, FacultyId = 11, SubjectCode = "IAP491", SubjectName = "IA Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 155, FacultyId = 11, SubjectCode = "IOP490", SubjectName = "IoT Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 156, FacultyId = 11, SubjectCode = "JGP491", SubjectName = "Graduation Project - Japanese Studies", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 157, FacultyId = 11, SubjectCode = "SAP490", SubjectName = "SAP Interdisciplinary Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 158, FacultyId = 11, SubjectCode = "SEP490", SubjectName = "SE Capstone Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            // Seed data for Graphic Design

            new Subject { SubjectId = 159, FacultyId = 12, SubjectCode = "ADB201", SubjectName = "Book Design & Printing Technology", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 160, FacultyId = 12, SubjectCode = "ADE301", SubjectName = "Visual Communication Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 161, FacultyId = 12, SubjectCode = "ADH301", SubjectName = "Mobility Applications Design 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 162, FacultyId = 12, SubjectCode = "ADI201", SubjectName = "Brand Identity Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 163, FacultyId = 12, SubjectCode = "ADP301", SubjectName = "Packaging design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 164, FacultyId = 12, SubjectCode = "ADT401", SubjectName = "Mobility Applications Design 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 165, FacultyId = 12, SubjectCode = "AET102", SubjectName = "Aesthetic", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 166, FacultyId = 12, SubjectCode = "AFA201", SubjectName = "Human Anatomy for Artis", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 167, FacultyId = 12, SubjectCode = "AGD301", SubjectName = "Information Graphic Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 168, FacultyId = 12, SubjectCode = "AMR401", SubjectName = "3D Modeling & Rigging", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 169, FacultyId = 12, SubjectCode = "ANA401", SubjectName = "3D Character Animation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 170, FacultyId = 12, SubjectCode = "ANB401", SubjectName = "Background Painting for Animation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 171, FacultyId = 12, SubjectCode = "ANC301", SubjectName = "Character Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 172, FacultyId = 12, SubjectCode = "ANO301c", SubjectName = "Visual development for digital design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 173, FacultyId = 12, SubjectCode = "ANS201", SubjectName = "Idea & Script Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 174, FacultyId = 12, SubjectCode = "ANS301", SubjectName = "Storyboarding", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 175, FacultyId = 12, SubjectCode = "ANT401", SubjectName = "Traditional Animation Principles", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 176, FacultyId = 12, SubjectCode = "CAD201", SubjectName = "Water Color", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 178, FacultyId = 12, SubjectCode = "DID301", SubjectName = "Data visualization & Infographic design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 179, FacultyId = 12, SubjectCode = "DRD204", SubjectName = "Drawing - Speed drawing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 180, FacultyId = 12, SubjectCode = "DRP101", SubjectName = "Drawing - Plaster Statue, Portrait", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 181, FacultyId = 12, SubjectCode = "DRS102", SubjectName = "Drawing - Form, Still-life", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 182, FacultyId = 12, SubjectCode = "DTG102", SubjectName = "Visual Design Tools", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 183, FacultyId = 12, SubjectCode = "DTG302", SubjectName = "Visual Effects - Principles of Compositing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 184, FacultyId = 12, SubjectCode = "DTG303", SubjectName = "Principles of Animation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 185, FacultyId = 12, SubjectCode = "GDF201", SubjectName = "Fundamental of Graphic Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 186, FacultyId = 12, SubjectCode = "HOA102", SubjectName = "Art History", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 187, FacultyId = 12, SubjectCode = "PFD201", SubjectName = "Photography for Designer", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 188, FacultyId = 12, SubjectCode = "PST202", SubjectName = "Perspective", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 189, FacultyId = 12, SubjectCode = "TPG203", SubjectName = "Basic typography & Layout", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 190, FacultyId = 12, SubjectCode = "TPG302", SubjectName = "Typography & E-publication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 191, FacultyId = 12, SubjectCode = "VCM202", SubjectName = "Visual Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 192, FacultyId = 12, SubjectCode = "VNC104", SubjectName = "Vietnamese Culture", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 193, FacultyId = 12, SubjectCode = "WDL202", SubjectName = "Web layout design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 194, FacultyId = 12, SubjectCode = "WDU202c", SubjectName = "UI/UX Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Subject { SubjectId = 195, FacultyId = 12, SubjectCode = "WIR201", SubjectName = "Interaction design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },



             // Seed data for Graphic Design
             new Subject { SubjectId = 196, FacultyId = 13, SubjectCode = "CES202", SubjectName = "System Support and Trouble Shooting", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 197, FacultyId = 13, SubjectCode = "CRY303c", SubjectName = "Applied Cryptography", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 198, FacultyId = 13, SubjectCode = "DBS401", SubjectName = "Database Security", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 199, FacultyId = 13, SubjectCode = "FRS301", SubjectName = "Digital Forensics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 200, FacultyId = 13, SubjectCode = "FRS401c", SubjectName = "Network Forensics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 201, FacultyId = 13, SubjectCode = "HOD401", SubjectName = "Ethical Hacking and Offensive Security", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 202, FacultyId = 13, SubjectCode = "IAA202", SubjectName = "Risk Management in Information Systems", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 203, FacultyId = 13, SubjectCode = "IAM302", SubjectName = "Malware Analysis and Reverse Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 204, FacultyId = 13, SubjectCode = "IAO201c", SubjectName = "Introduction to Information Assurance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 205, FacultyId = 13, SubjectCode = "IAP301", SubjectName = "Policy Development in Information Assurance", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 206, FacultyId = 13, SubjectCode = "IAR401c", SubjectName = "Incident Response", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 207, FacultyId = 13, SubjectCode = "IAW301", SubjectName = "Web security", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 208, FacultyId = 13, SubjectCode = "NWC204", SubjectName = "Computer Networking", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 209, FacultyId = 13, SubjectCode = "OSP201", SubjectName = "Open Source Platform and Network Administration", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Information Technology Specialization
             new Subject { SubjectId = 210, FacultyId = 14, SubjectCode = "CEA201", SubjectName = "Computer Organization and Architecture", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 211, FacultyId = 14, SubjectCode = "DGT301", SubjectName = "Digital Signal Processing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 212, FacultyId = 14, SubjectCode = "EDT202c", SubjectName = "Emerging Digital Technologies", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 213, FacultyId = 14, SubjectCode = "ESP301m", SubjectName = "Microcontroller & embedded system programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 214, FacultyId = 14, SubjectCode = "IOP391", SubjectName = "IoT application development project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 215, FacultyId = 14, SubjectCode = "IOT102", SubjectName = "Internet of things", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 216, FacultyId = 14, SubjectCode = "ISC301", SubjectName = "e-Commerce", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 217, FacultyId = 14, SubjectCode = "PRC392c", SubjectName = "Cloud Computing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 218, FacultyId = 14, SubjectCode = "SST301", SubjectName = "Sensor Technology", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Japanese
             new Subject { SubjectId = 219, FacultyId = 15, SubjectCode = "JIJ301", SubjectName = "Basic issues of Japanese lexicology & phonetics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 220, FacultyId = 15, SubjectCode = "JIS401", SubjectName = "Japanese in Software", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 221, FacultyId = 15, SubjectCode = "JIT401", SubjectName = "Information Technology Japanese", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 222, FacultyId = 15, SubjectCode = "JJL301", SubjectName = "Japanese Literature", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 223, FacultyId = 15, SubjectCode = "JPD113", SubjectName = "Elementary Japanese 1-A1.1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 224, FacultyId = 15, SubjectCode = "JPD113-EX", SubjectName = "Japanese 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 225, FacultyId = 15, SubjectCode = "JPD123", SubjectName = "Elementary Japanese 1-A1.2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 226, FacultyId = 15, SubjectCode = "JPD133", SubjectName = "Elementary Japanese 1-A1/A2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 227, FacultyId = 15, SubjectCode = "JPD316", SubjectName = "Intermediate Japanese 1-B1/B2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 228, FacultyId = 15, SubjectCode = "JPD326", SubjectName = "Japanese Intermediate 2-B2.1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 229, FacultyId = 15, SubjectCode = "JSI201", SubjectName = "Information System", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Korean
             new Subject { SubjectId = 230, FacultyId = 16, SubjectCode = "KLE301", SubjectName = "Korean Literature", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 231, FacultyId = 16, SubjectCode = "KLI311", SubjectName = "Interpretation 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 232, FacultyId = 16, SubjectCode = "KLT311", SubjectName = "Translation 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 233, FacultyId = 16, SubjectCode = "KOR311", SubjectName = "Intermediate Korean Language 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 234, FacultyId = 16, SubjectCode = "KOR321", SubjectName = "Intermediate Korean Language 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 235, FacultyId = 16, SubjectCode = "KOR411", SubjectName = "Intermediate Korean Language 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 236, FacultyId = 16, SubjectCode = "KRC301", SubjectName = "Korean Culture", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 237, FacultyId = 16, SubjectCode = "KRL312", SubjectName = "Intermediate Korean 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 238, FacultyId = 16, SubjectCode = "KRL322", SubjectName = "Intermediate Korean 4", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 239, FacultyId = 16, SubjectCode = "KRP301", SubjectName = "Korean Phonetics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Little UK
             new Subject { SubjectId = 240, FacultyId = 18, SubjectCode = "LUK1", SubjectName = "LUK Global 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 241, FacultyId = 18, SubjectCode = "LUK5", SubjectName = "LUK Global 5", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Mathematics
             new Subject { SubjectId = 242, FacultyId = 20, SubjectCode = "MAD101", SubjectName = "Discrete mathematics", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 243, FacultyId = 20, SubjectCode = "MAE101", SubjectName = "Mathematics for Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 244, FacultyId = 20, SubjectCode = "MAE101-EX", SubjectName = "Mathematics for Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 245, FacultyId = 20, SubjectCode = "MAI391", SubjectName = "Mathematics for Machine Learning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 246, FacultyId = 20, SubjectCode = "MAS202", SubjectName = "Applied Statistics for Business", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 247, FacultyId = 20, SubjectCode = "MAS202-EX", SubjectName = "Applied Statistics for Business", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 248, FacultyId = 20, SubjectCode = "MAS291", SubjectName = "Statistics and Probability", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Multimedia Communications
             new Subject { SubjectId = 249, FacultyId = 21, SubjectCode = "BDI201", SubjectName = "Brand identity design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 250, FacultyId = 21, SubjectCode = "BRA301", SubjectName = "Brand Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 251, FacultyId = 21, SubjectCode = "CCM301", SubjectName = "Crisis Communications Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 252, FacultyId = 21, SubjectCode = "CCO201", SubjectName = "Corporate Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 253, FacultyId = 21, SubjectCode = "CSP201m", SubjectName = "Content Strategy for Professionals", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 254, FacultyId = 21, SubjectCode = "DTG111", SubjectName = "Visual Design Tools 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 255, FacultyId = 21, SubjectCode = "EVN201", SubjectName = "Event planning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 256, FacultyId = 21, SubjectCode = "IFT201c", SubjectName = "Innovation and Future thinking", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 257, FacultyId = 21, SubjectCode = "IMC301c", SubjectName = "Intergrated Marketing Communication in Digital World", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 258, FacultyId = 21, SubjectCode = "MCO201m", SubjectName = "Transmedia Storytelling", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 259, FacultyId = 21, SubjectCode = "MED201", SubjectName = "New Media Technology", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 260, FacultyId = 21, SubjectCode = "MEP301", SubjectName = "Multimedia Production Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 261, FacultyId = 21, SubjectCode = "MSM201c", SubjectName = "Meta social media marketing Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 262, FacultyId = 21, SubjectCode = "PRE301", SubjectName = "Public Relations principles and strategies", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 263, FacultyId = 21, SubjectCode = "RMC301m", SubjectName = "Research methods in Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 264, FacultyId = 21, SubjectCode = "SDP201", SubjectName = "Sound Production", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 265, FacultyId = 21, SubjectCode = "SEO201c", SubjectName = "Search Engine Optimization", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 266, FacultyId = 21, SubjectCode = "VDE301", SubjectName = "Digital Video Editing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 267, FacultyId = 21, SubjectCode = "VDP301", SubjectName = "Search Engine Optimization", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 268, FacultyId = 21, SubjectCode = "SEO201c", SubjectName = "Video Production", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 269, FacultyId = 21, SubjectCode = "WMC201", SubjectName = "Media Writing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 336, FacultyId = 21, SubjectCode = "BCJ201c", SubjectName = "Branding: The Creative Journey", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for On the job training
             new Subject { SubjectId = 271, FacultyId = 22, SubjectCode = "OJB202", SubjectName = "On-the-job training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 272, FacultyId = 22, SubjectCode = "OJE202", SubjectName = "On-the-job training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 273, FacultyId = 22, SubjectCode = "OJP202", SubjectName = "On-the-job training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 274, FacultyId = 22, SubjectCode = "OJS201", SubjectName = "On-the-job training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 275, FacultyId = 22, SubjectCode = "OJT202", SubjectName = "On-the-job training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for OR
             new Subject { SubjectId = 276, FacultyId = 23, SubjectCode = "GDQP", SubjectName = "Military training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 277, FacultyId = 23, SubjectCode = "ORT101", SubjectName = "Orientation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 278, FacultyId = 23, SubjectCode = "ORT102", SubjectName = "Orientation IT", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 279, FacultyId = 23, SubjectCode = "ORT103", SubjectName = "Orientation Examination", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 280, FacultyId = 23, SubjectCode = "ORT108", SubjectName = "Orientation", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Physical Training
             new Subject { SubjectId = 281, FacultyId = 24, SubjectCode = "COV111", SubjectName = "Chess 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 282, FacultyId = 24, SubjectCode = "COV121", SubjectName = "Chess 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 283, FacultyId = 24, SubjectCode = "COV131", SubjectName = "Chess 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 284, FacultyId = 24, SubjectCode = "GDQP", SubjectName = "Military training", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 285, FacultyId = 24, SubjectCode = "OTP101", SubjectName = "Orientation and General Training Program", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 286, FacultyId = 24, SubjectCode = "VOV-EX", SubjectName = "Vovinam", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 287, FacultyId = 24, SubjectCode = "VOV114", SubjectName = "Vovinam 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 288, FacultyId = 24, SubjectCode = "VOV124", SubjectName = "Vovinam 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 289, FacultyId = 24, SubjectCode = "VOV134", SubjectName = "Vovinam 3", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


             // Seed data for Soft Skill
             new Subject { SubjectId = 290, FacultyId = 26, SubjectCode = "HCM202", SubjectName = "Ho Chi Minh Ideology", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 291, FacultyId = 26, SubjectCode = "MLN111", SubjectName = "Philosophy of Marxism – Leninism", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 292, FacultyId = 26, SubjectCode = "MLN122", SubjectName = "Political economics of Marxism – Leninism", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 293, FacultyId = 26, SubjectCode = "MLN131", SubjectName = "Scientific socialism", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 294, FacultyId = 26, SubjectCode = "MMP201", SubjectName = "Media Psychology", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 295, FacultyId = 26, SubjectCode = "SSB201", SubjectName = "Advanced Business Communication", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 296, FacultyId = 26, SubjectCode = "SSG104", SubjectName = "Communication and In-Group Working Skills", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 297, FacultyId = 26, SubjectCode = "SSG104-EX", SubjectName = "Communication and In-Group Working Skills", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 298, FacultyId = 26, SubjectCode = "SSL101c", SubjectName = "Academic Skills for University Success", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 299, FacultyId = 26, SubjectCode = "VNR202", SubjectName = "History of Việt Nam Communist Party", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


             // Seed data for Software Engineering
             new Subject { SubjectId = 300, FacultyId = 27, SubjectCode = "CSI106", SubjectName = "Introduction to Computer Science", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 301, FacultyId = 27, SubjectCode = "DTA301", SubjectName = "Data Analysis", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 302, FacultyId = 27, SubjectCode = "HSF301", SubjectName = "Hibernate and Spring framework", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 303, FacultyId = 27, SubjectCode = "ISM302", SubjectName = "Enterprise Resource Planning", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 304, FacultyId = 27, SubjectCode = "ISP392", SubjectName = "Information System Programming Project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 305, FacultyId = 27, SubjectCode = "ITA203c", SubjectName = "Management information systems", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 306, FacultyId = 27, SubjectCode = "ITA301", SubjectName = "Information System Analysis and Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 307, FacultyId = 27, SubjectCode = "ITB302c", SubjectName = "Business Intelligence (BI)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 308, FacultyId = 27, SubjectCode = "ITE302c", SubjectName = "Ethics in IT", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 309, FacultyId = 27, SubjectCode = "ITE303c", SubjectName = "Ethics in IT", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 310, FacultyId = 27, SubjectCode = "KMS301", SubjectName = "Knowledge management system", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 311, FacultyId = 27, SubjectCode = "LAB211", SubjectName = "OOP with Java Lab", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 312, FacultyId = 27, SubjectCode = "LAB211-EX", SubjectName = "OOP with Java Lab", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 313, FacultyId = 27, SubjectCode = "LAB221c", SubjectName = "Desktop Java lab", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 314, FacultyId = 27, SubjectCode = "MMA301", SubjectName = "Multiplatform Mobile App Development", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 315, FacultyId = 27, SubjectCode = "PMG201c", SubjectName = "Project management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 316, FacultyId = 27, SubjectCode = "PRM392", SubjectName = "Mobile Programming", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 317, FacultyId = 27, SubjectCode = "SAP311", SubjectName = "SAP General 1", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 318, FacultyId = 27, SubjectCode = "SAP321", SubjectName = "SAP General 2", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 319, FacultyId = 27, SubjectCode = "SAP341", SubjectName = "SAP Application Development with ABAP", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 320, FacultyId = 27, SubjectCode = "SWD392", SubjectName = "SW Architecture and Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 321, FacultyId = 27, SubjectCode = "SWE201c", SubjectName = "Introduction to Software Engineering", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 322, FacultyId = 27, SubjectCode = "SWP391", SubjectName = "Application development project", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 323, FacultyId = 27, SubjectCode = "SWR302", SubjectName = "Software Requirement", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 324, FacultyId = 27, SubjectCode = "SWT301", SubjectName = "Software Testing", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 325, FacultyId = 27, SubjectCode = "WDU203c", SubjectName = "UI/UX Design", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

             // Seed data for Software Engineering
             new Subject { SubjectId = 326, FacultyId = 29, SubjectCode = "DBA103", SubjectName = "Traditional musical instrument - Dan Bau", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 327, FacultyId = 29, SubjectCode = "DNG103", SubjectName = "Traditional musical instrument - Dan Nguyet", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 328, FacultyId = 29, SubjectCode = "DNH103", SubjectName = "Traditional musical instrument - Dan Nhi", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 329, FacultyId = 29, SubjectCode = "DSA103", SubjectName = "Traditional musical instrument - Sao Truc", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 330, FacultyId = 29, SubjectCode = "DTB103", SubjectName = "Traditional musical instrument - Dan Ty Ba", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 331, FacultyId = 29, SubjectCode = "DTR103", SubjectName = "Traditional musical instrument - Dan Tranh", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 332, FacultyId = 29, SubjectCode = "TRG101", SubjectName = "Traditional musical instrument - Trong", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 333, FacultyId = 29, SubjectCode = "TRG103", SubjectName = "Traditional musical instrument - Trong", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 334, FacultyId = 29, SubjectCode = "ÐBA101", SubjectName = "Traditional musical instrument - Dan Bau", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new Subject { SubjectId = 335, FacultyId = 29, SubjectCode = "ÐNH101", SubjectName = "Traditional musical instrument - Dan Nhi", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }


        );

        modelBuilder.Entity<CampusUserSubject>().HasData(

            //Seed data for Heads of Department of Ha Noi campus
            //new CampusUserSubject { Id = 1, SubjectId = 1, CampusId = 1, UserId = 9 },
            //new CampusUserSubject { Id = 2, SubjectId = 2, CampusId = 1, UserId = 9 },
            //new CampusUserSubject { Id = 3, SubjectId = 3, CampusId = 1, UserId = 9 },
            //new CampusUserSubject { Id = 4, SubjectId = 4, CampusId = 1, UserId = 10 },
            //new CampusUserSubject { Id = 5, SubjectId = 5, CampusId = 1, UserId = 10 },

            //new CampusUserSubject { Id = 6, SubjectId = 6, CampusId = 1, UserId = 11 },
            //new CampusUserSubject { Id = 7, SubjectId = 7, CampusId = 1, UserId = 11},
            //new CampusUserSubject { Id = 8, SubjectId = 8, CampusId = 1, UserId = 1 },


            //Seed data for Lecturer of Ha Noi campus
            new CampusUserSubject { Id = 9, SubjectId = 1, CampusId = 1, UserId = 5 },
            new CampusUserSubject { Id = 10, SubjectId = 2, CampusId = 1, UserId = 5 },
            new CampusUserSubject { Id = 11, SubjectId = 3, CampusId = 1, UserId = 5 },
            new CampusUserSubject { Id = 12, SubjectId = 4, CampusId = 1, UserId = 6 },
            new CampusUserSubject { Id = 13, SubjectId = 5, CampusId = 1, UserId = 6 },

            new CampusUserSubject { Id = 14, SubjectId = 6, CampusId = 1, UserId = 7 },
            new CampusUserSubject { Id = 15, SubjectId = 7, CampusId = 1, UserId = 7 },
            new CampusUserSubject { Id = 16, SubjectId = 8, CampusId = 1, UserId = 7 }


        //// Seed data for Heads of Department of Da Nang campus
        //new CampusUserSubject { Id = 17, SubjectId = 1, CampusId = 2, UserId = 8},
        //new CampusUserSubject { Id = 18, SubjectId = 2, CampusId = 2, UserId = 8 },
        //new CampusUserSubject { Id = 19, SubjectId = 3, CampusId = 2, UserId = 8 },
        //new CampusUserSubject { Id = 20, SubjectId = 4, CampusId = 2, UserId = 8 },
        //new CampusUserSubject { Id = 21, SubjectId = 5, CampusId = 2, UserId = 8 }
        );
        modelBuilder.Entity<CampusUserFaculty>().HasData(
        //Seed data for Heads of Department of Ha Noi campus
        new CampusUserFaculty { Id = 1, CampusId = 1, UserId = 9, FacultyId = 1 },
        new CampusUserFaculty { Id = 2, CampusId = 1, UserId = 9, FacultyId = 2 },
        new CampusUserFaculty { Id = 3, CampusId = 1, UserId = 9, FacultyId = 3 },
        new CampusUserFaculty { Id = 4, CampusId = 1, UserId = 10, FacultyId = 4 },
        new CampusUserFaculty { Id = 5, CampusId = 1, UserId = 10, FacultyId = 5 }

        );


        // 7. Seed data for Exam table
        modelBuilder.Entity<Exam>().HasData(

            // Ha Noi's Examiners create exams
            // Seed data for software engineering major
            new Exam { ExamId = 1, ExamCode = "PRN211_Q1_10_123456", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 2, ExamCode = "PRN211_Q2_5_654321", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 1, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 3, ExamCode = "PRN221_Q1_10_789012", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 4, ExamCode = "PRN221_Q2_5_210987", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 2, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 20), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 5, ExamCode = "PRN231_Q1_10_345678", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 6, ExamCode = "PRN231_Q2_5_876543", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 3, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 7, ExamCode = "MAE101_Q1_10_234567", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 8, ExamCode = "MAE101_Q2_5_765432", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 4, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 9, ExamCode = "NWC203c_Q1_10_345678", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 2, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), CreateDate = DateTime.Now, UpdateDate = DateTime.Now,  },
            new Exam { ExamId = 10, ExamCode = "NWC203c_Q2_5_876543", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 5, CreaterId = 2, CampusId = 1, SemesterId = 1, ExamStatusId = 3, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 25), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },


            // Seed data for international business major
            new Exam { ExamId = 11, ExamCode = "ENM401_Q1_10_111222", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 12, ExamCode = "ENM401_Q2_5_222111", ExamDuration = "30'", TermDuration = "Block 10", ExamType = "Reading", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 13, ExamCode = "ENM401_Q3_7_222333", ExamDuration = "60'", TermDuration = "Block 10", ExamType = "Writing", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10..", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 14, ExamCode = "ENM401_Q4_9_333111", ExamDuration = "30'", TermDuration = "Block 10", ExamType = "Listening", SubjectId = 6, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 4, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 20), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "This exam covers the material from Block 10.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 15, ExamCode = "ECO121_Q1_10_333444", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 16, ExamCode = "ECO121_Q2_5_444333", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 7, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 5, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 21), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "OK.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 17, ExamCode = "ECO201_Q1_10_555666", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 18, ExamCode = "ECO201_Q2_5_666555", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 8, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 6, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = new DateTime(2024, 9, 22), StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 30), AssignedUserId = 3, AssignmentDate = new DateTime(2024, 8, 25), GeneralFeedback = "The exam can be used for testing.", IsReady = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 19, ExamCode = "ACC101_Q1_10_777888", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 20, ExamCode = "ACC101_Q2_5_888777", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 9, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },

            new Exam { ExamId = 21, ExamCode = "MKT101_Q1_10_999000", ExamDuration = "90'", TermDuration = "Block 10", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Exam { ExamId = 22, ExamCode = "MKT101_Q2_5_000999", ExamDuration = "60'", TermDuration = "Block 5", ExamType = "Multiple Choice", SubjectId = 10, CreaterId = 2, CampusId = 1, SemesterId = 2, ExamStatusId = 1, ExamDate = new DateTime(2024, 10, 21), EstimatedTimeTest = null, StartDate = DateTime.Now, EndDate = DateTime.Now, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }


        );
        // 10. Seed data for Menu table
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuLink = "/Admin/UserManagement", MenuName = "User Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 2, MenuLink = "/Admin/UserActivityLog", MenuName = "User Activity Log", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 3, MenuLink = "/Examiner/ExamList", MenuName = "Exam List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 4, MenuLink = "/HeadDepartment/ExamList", MenuName = "Assign Teacher To Review", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 5, MenuLink = "/Lecture/ExamList", MenuName = "Exam List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 6, MenuLink = "/HeadDepartment/Report", MenuName = "View Report", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 7, MenuLink = "/HeadDepartment/ExamStatus", MenuName = "Exam Status(UnderContrucst)", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 10, MenuLink = "/Examiner/HeadDeparmentManagement", MenuName = "Head Department Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 8, MenuLink = "/Admin/CampusManagement", MenuName = "Campus Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 11, MenuLink ="/Examiner/CreateExam", MenuName = "Create Exam", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 12, MenuLink = "/HeadDepartment/LecturerManagement", MenuName = "Lecturer Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 13, MenuLink = "/Examiner/OverallReport", MenuName = "Overall Report", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 14, MenuLink = "/Admin/SemesterManagement", MenuName = "Semester Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 9, MenuLink = "/Admin/SubjectManagement", MenuName = "Subject Management", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 15, MenuLink = "/Examiner/ReviewReport", MenuName = "Review Report", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new Menu { MenuId = 16, MenuLink = "/HeadDepartment/SubjectList", MenuName = "Subject List", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }

        );

        // 11. Seed data for MenuRole table
        modelBuilder.Entity<MenuRole>().HasData(
            new MenuRole { RoleId = 1, MenuId = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 8, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 9, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 1, MenuId = 14, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
             new MenuRole { RoleId = 2, MenuId = 15, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 10, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 6, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 7, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 11, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 2, MenuId = 13, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 12, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 3, MenuId = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            new MenuRole { RoleId = 4, MenuId = 16, CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
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



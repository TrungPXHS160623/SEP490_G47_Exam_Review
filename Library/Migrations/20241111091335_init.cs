using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    CampusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.CampusId);
                });

            migrationBuilder.CreateTable(
                name: "ExamStatuses",
                columns: table => new
                {
                    ExamStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusContent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStatuses", x => x.ExamStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProgram = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    SemesterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.SemesterId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRoles", x => new { x.RoleId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_MenuRoles_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRoles_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailFe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Campuses",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "CampusUserFaculty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampusUserFaculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampusUserFaculty_Campuses",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampusUserFaculty_Faculties",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampusUserFaculty_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampusUserSubject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampusUserSubject", x => x.id);
                    table.ForeignKey(
                        name: "FK_CampusUserSubject_Campuses",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_CampusUserSubject_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "SemesterId");
                    table.ForeignKey(
                        name: "FK_CampusUserSubject_Subjects",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_CampusUserSubject_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExamDuration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TermDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    CreaterId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    ExamStatusId = table.Column<int>(type: "int", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedTimeTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GeneralFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                    table.ForeignKey(
                        name: "FK_Exams_Campuses",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_Exams_ExamStatuses_ExamStatusId",
                        column: x => x.ExamStatusId,
                        principalTable: "ExamStatuses",
                        principalColumn: "ExamStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Semesters",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "SemesterId");
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_Exams_Users_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    LogContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHistory_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: true),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionSolutionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Exams",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId");
                });

            migrationBuilder.CreateTable(
                name: "ReportFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_ReportFiles_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4431), null, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4453) },
                    { 2, "Da Nang", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4458), null, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4459) },
                    { 3, "Can Tho", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4462), null, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4463) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4467), null, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4468) },
                    { 5, "Quy Nhon", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4611), null, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4612) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5015), "Unassigned", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5018) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5021), "Assigned", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5022) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5024), "Awaiting Lecturer Confirm", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5026) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5028), "Reviewing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5029) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5031), "Error", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5032) },
                    { 6, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5033), "OK", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5035) },
                    { 7, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5037), "Completed", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5037) },
                    { 8, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5039), "Rejected", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5040) }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "CreateDate", "Description", "FacultyName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5097), "The study and creation of systems that can perform tasks requiring human-like intelligence, such as learning and problem-solving.", "Artificial Intelligence", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5098) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5101), "A field focusing on core skills in business, law, operations, and communication for professional success.", "BLOC", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5120) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5136), "The study of managing and overseeing business operations, including planning, organization, and leadership", "Business Administration", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5138) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5140), "The study of the Chinese language, culture, and communication skills.", "Chinese", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5141) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5143), "The study of computation, algorithms, programming, and the design of software and hardware systems.", "Computer Science", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5144) },
                    { 6, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5147), "An introduction to core computing concepts, including basic programming, data processing, and system operations.", "Computing Fundamental", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5148) },
                    { 7, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5150), "The study of the English language, including grammar, literature, and communication skills.", "English", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5151) },
                    { 8, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5154), "A course designed to enhance language skills in reading, writing, speaking, and listening, preparing students for academic or professional English proficiency.", "English Preparation Course", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5155) },
                    { 9, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5157), "Additional sessions designed to provide supplementary learning in various subjects to reinforce or expand students' knowledge and skills.", "Extra Classes", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5158) },
                    { 10, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5161), "The study of managing money, investments, financial systems, and the principles of budgeting and financial decision-making.", "Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5161) },
                    { 11, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5164), "A program or course designed for students who have completed an undergraduate degree, focusing on advanced studies and specialized knowledge in a particular field.", "Graduate", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5165) },
                    { 12, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5167), "The art of creating visual content to communicate messages, combining elements like typography, images, and colors to design logos, advertisements, websites, and more.", "Graphic Design", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5168) },
                    { 13, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5170), "The practice of protecting and managing information systems to ensure their confidentiality, integrity, and availability, focusing on risk management and security measures.", "Information Assurance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5171) },
                    { 14, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5174), "A focused study of advanced topics in IT, such as network management, cybersecurity, software development, and database administration.", "Information Technology Specialization", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5175) },
                    { 15, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5177), "The study of the Japanese language, including its grammar, vocabulary, writing systems, and cultural aspects.", "Japanese", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5178) },
                    { 16, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5180), "The study of the Korean language, including its grammar, vocabulary, writing systems (Hangul), and cultural nuances.", "Korean", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5181) },
                    { 17, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5183), "A practical learning environment where students conduct experiments, apply theoretical knowledge, and gain hands-on experience in various scientific or technical fields.", "LAB", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5184) },
                    { 18, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5187), "A program or initiative that offers an immersive learning experience focused on British culture, language, and educational practices, often designed to enhance students' understanding of the UK.", "Little UK", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5188) },
                    { 19, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5190), "The study of planning, organizing, and overseeing resources and processes to achieve organizational goals efficiently and effectively.", "Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5191) },
                    { 20, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5193), "The study of numbers, quantities, shapes, and patterns, focusing on problem-solving, logic, and abstract reasoning.", "Mathematics", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5194) },
                    { 21, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5197), "The study of using various media formats—such as text, audio, video, and graphics—to communicate information effectively across different platforms and technologies.", "Multimedia Communications", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5197) },
                    { 22, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5201), "A practical learning process where employees acquire skills and knowledge by performing tasks and duties in a real work environment under the guidance of experienced colleagues or supervisors.", "On the job training", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5202) },
                    { 23, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5204), "The application of mathematical models, statistical analysis, and optimization techniques to solve complex decision-making problems and improve efficiency in business, logistics, and other systems.", "OR", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5205) },
                    { 24, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5207), "The practice of improving physical fitness and performance through exercises, workouts, and conditioning to enhance strength, endurance, and overall health.", "Physical Training", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5208) },
                    { 25, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5211), "A leading enterprise resource planning (ERP) software that helps organizations manage business processes by integrating key functions like finance, supply chain, human resources, and customer relationships.", "SAP", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5212) },
                    { 26, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5214), "Non-technical skills that relate to how individuals interact with others, such as communication, teamwork, problem-solving, adaptability, and leadership.", "Soft Skill", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5215) },
                    { 27, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5217), "The application of engineering principles to the design, development, testing, and maintenance of software systems, ensuring they are efficient, reliable, and scalable.", "Software Engineering", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5218) },
                    { 28, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5220), "A program or course focused on teaching the fundamentals of launching and managing a new business, including idea development, business planning, marketing, and financial management.", "Start Your Business", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5221) },
                    { 29, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5223), "The study and practice of playing musical instruments that are indigenous to specific cultures, often passed down through generations, such as the violin, guitar, sitar, or drums.", "Traditional Instrument", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5224) },
                    { 30, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5226), "The study of the Vietnamese language, including its grammar, vocabulary, pronunciation, and cultural context.", "Vietnamese", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5227) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7079), null, "/usermanagement", "User Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7080) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7083), null, "/Admin/History", "User Log", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7084) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7086), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7087) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7089), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7090) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7092), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7093) },
                    { 6, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7096), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7097) },
                    { 7, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7099), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7100) },
                    { 8, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7106), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7107) },
                    { 9, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7122), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7123) },
                    { 10, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7102), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7104) },
                    { 11, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7110), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7111) },
                    { 12, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7113), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7114) },
                    { 13, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7116), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7117) },
                    { 14, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7119), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7120) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7351), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp24", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7352) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7356), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su24", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7357) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7361), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa24", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7362) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5308), "Admin", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5310) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5313), "Examiner", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5314) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5399), "Lecturer", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5400) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5402), "Head of Department", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5403) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5406), "Curriculum Development", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5407) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7174), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7176) },
                    { 2, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7178), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7179) },
                    { 8, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7180), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7181) },
                    { 9, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7183), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7184) },
                    { 14, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7186), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7187) },
                    { 3, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7189), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7190) },
                    { 10, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7193), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7194) },
                    { 11, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7204), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7205) },
                    { 13, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7206), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7208) },
                    { 5, 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7212), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7213) },
                    { 4, 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7195), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7196) },
                    { 6, 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7198), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7199) },
                    { 7, 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7201), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7202) },
                    { 12, 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7209), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7210) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "FacultyId", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5700), 1, null, "ADY201m", "Data Science with Python & SQL", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5702) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5706), 1, null, "AID301c", "AI in Production", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5707) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5710), 1, null, "AIE301m", "AI for Trading", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5711) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5715), 1, null, "AIG202c", "Artificial Intelligence", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5716) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5719), 1, null, "AIH301m", "AI in Healthcare", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5720) },
                    { 6, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5723), 1, null, "AIL303m", "Machine Learning", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5723) },
                    { 7, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5726), 1, null, "AIM301m", "AI for Medicine", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5727) },
                    { 8, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5730), 1, null, "ASR301c", "AI for Scientific Research", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5731) },
                    { 9, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5734), 1, null, "BDI301c", "Big Data", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5735) },
                    { 10, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5738), 1, null, "BDI302c", "Big Data", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5739) },
                    { 11, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5741), 1, null, "CPV301", "Computer Vision", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5742) },
                    { 12, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5745), 1, null, "DAP391m", "AI-DS Project", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5746) },
                    { 13, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5749), 1, null, "DAT301m", "AI Development with TensorFlow", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5750) },
                    { 14, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5753), 1, null, "DBM302m", "Data Mining", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5754) },
                    { 15, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5757), 1, null, "DPL301m", "Deep Learing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5758) },
                    { 16, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5761), 1, null, "DPL302m", "Deep Learning", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5762) },
                    { 17, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5765), 1, null, "DSR301m", "Applied Data Science with R", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5766) },
                    { 18, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5769), 1, null, "DWP301c", "Web Development with Python", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5770) },
                    { 19, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5773), 1, null, "NLP301c", "Natural Language Processing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5774) },
                    { 20, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5776), 1, null, "PRP201c", "Python programming", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5777) },
                    { 21, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5780), 1, null, "REL301m", "Reinforcement Learning", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5781) },
                    { 22, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5784), 2, null, "BDP306b", "Final Project - Blockchain Development in Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5785) },
                    { 23, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5787), 3, null, "ACC101", "Principles of Accounting", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5788) },
                    { 24, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5791), 3, null, "ACC302", "Managerial Accounting", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5792) },
                    { 25, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5796), 3, null, "ACC305", "Financial Statement Analysis", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5797) },
                    { 26, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5799), 3, null, "ADS301m", "Google Ads và Seo", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5800) },
                    { 27, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5803), 3, null, "CAA201", "Communications and advertising", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5804) },
                    { 28, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5806), 3, null, "CIH201", "Contemporary issues in hotel and tourism management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5807) },
                    { 29, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5810), 3, null, "DMA301m", "Digital Marketing Analytics", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5811) },
                    { 30, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5814), 3, null, "DMS301m", "Digital Marketing Strategy", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5815) },
                    { 31, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5817), 3, null, "ECO102", "Business Environment", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5819) },
                    { 32, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5822), 3, null, "ECO111", "Microeconomics", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5823) },
                    { 33, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5826), 3, null, "ECO201", "International Economics", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5827) },
                    { 34, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5829), 3, null, "EXE101", "Experiential Entrepreneurship 1", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5830) },
                    { 35, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5834), 3, null, "EXE201", "Experiential Entrepreneurship 2", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5836) },
                    { 36, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5838), 3, null, "FIM302c", "Financial modelling", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5839) },
                    { 37, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5843), 3, null, "FIN201", "Monetary Economics and Global Economy", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5844) },
                    { 38, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5846), 3, null, "FIN202", "Principles of Corporate Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5847) },
                    { 39, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5850), 3, null, "FIN301", "Financial Markets and Institutions", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5851) },
                    { 40, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5853), 3, null, "FIN303", "Advanced Corporate Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5855) },
                    { 41, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5859), 3, null, "FIN306c", "Financial Reporting", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5860) },
                    { 42, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5862), 3, null, "FIN308", "International Corporate Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5863) },
                    { 43, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5866), 3, null, "FIN402", "Derivatives", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5867) },
                    { 44, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5870), 3, null, "FIN403", "Mergers and Acquisitions", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5871) },
                    { 45, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5874), 3, null, "HRM201c", "Human Resource Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5875) },
                    { 46, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5878), 3, null, "IBC201", "Cross Cultural Management and Negotiation", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5879) },
                    { 47, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5933), 3, null, "IBF301", "International Finance", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5934) },
                    { 48, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5937), 3, null, "IBI101", "Introduction to International business", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5938) },
                    { 49, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5940), 3, null, "IBS301m", "International Business Strategy", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5941) },
                    { 50, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5944), 3, null, "IEI301", "Import and Export", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5945) },
                    { 51, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5947), 3, null, "IIP301", "International payment", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5948) },
                    { 52, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5951), 3, null, "IPR102", "Intellectual Property Rights", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5952) },
                    { 53, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5955), 3, null, "LAW102", "Business Law and Ethics Fundamentals", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5956) },
                    { 54, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5959), 3, null, "LAW201c", "International Business Law", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5960) },
                    { 55, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5962), 3, null, "LOG311", "Customs Operations", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5963) },
                    { 56, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5967), 3, null, "MGT103", "Introduction to Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5968) },
                    { 57, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5972), 3, null, "MKT101", "Marketing Principles", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5973) },
                    { 58, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5978), 3, null, "MKT201", "Consumer Behavior", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5979) },
                    { 59, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5982), 3, null, "MKT202", "Services Marketing Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5984) },
                    { 60, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5986), 3, null, "MKT205c", "International Marketing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5987) },
                    { 61, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5990), 3, null, "MKT208c", "Social media marketing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5991) },
                    { 62, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5994), 3, null, "MKT209m", "Content Marketing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5995) },
                    { 63, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5998), 3, null, "MKT301", "Marketing Research", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5999) },
                    { 64, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6002), 3, null, "MKT304", "Integrated Marketing Communications", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6003) },
                    { 65, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6006), 3, null, "MKT309m ", "Omnichannel marketing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6007) },
                    { 66, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6012), 3, null, "OBE102c", "Organizational Behavior", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6013) },
                    { 67, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6018), 3, null, "RES201", "Food Preparation & Science", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6019) },
                    { 68, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6022), 3, null, "RES213", "Wines, Beers, Spirits 1", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6023) },
                    { 69, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6026), 3, null, "RES301", "Food and Beverage Cost Control", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6027) },
                    { 70, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6030), 3, null, "RMB301", "Business Research Methods", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6031) },
                    { 71, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6034), 3, null, "RMB302", "Research Methods & Quantitative Analysis", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6035) },
                    { 72, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6037), 3, null, "SAL301", "Professional Selling", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6038) },
                    { 73, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6041), 3, null, "SCM201", "Supply Chain Management", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6042) },
                    { 74, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6044), 3, null, "SCM301m", "Procurement and Global Sourcing", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6045) },
                    { 75, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6048), 3, null, "SYB302c", "Entrepreneurship", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6049) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5477), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5478) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5485), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lienkt@fe.edu.vn", "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5486) },
                    { 3, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5492), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hunglthe160235@fe.edu.vn", "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5493) },
                    { 4, "Đà Nẵng", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5502), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoanglm@fe.edu.vn", "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5503) },
                    { 5, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5509), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5510) },
                    { 6, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5516), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5517) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5523), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5525) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5530), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5531) },
                    { 9, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5537), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5538) },
                    { 10, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5543), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5544) },
                    { 11, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5550), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5551) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5557), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5558) },
                    { 13, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5564), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5565) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5571), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5572) },
                    { 15, "Hà Nội", 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5577), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5578) },
                    { 16, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5584), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5585) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserFaculty",
                columns: new[] { "Id", "CampusId", "CreateDate", "FacultyId", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6279), 1, null, 9 },
                    { 2, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6283), 2, null, 9 },
                    { 3, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6285), 3, null, 9 },
                    { 4, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6287), 4, null, 10 },
                    { 5, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6289), 5, null, 10 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 9, 1, null, 1, 5 },
                    { 10, 1, null, 2, 5 },
                    { 11, 1, null, 3, 5 },
                    { 12, 1, null, 4, 6 },
                    { 13, 1, null, 5, 6 },
                    { 14, 1, null, 6, 7 },
                    { 15, 1, null, 7, 7 },
                    { 16, 1, null, 8, 7 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "AssignedUserId", "AssignmentDate", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDate", "ExamDuration", "ExamStatusId", "ExamType", "GeneralFeedback", "IsReady", "SemesterId", "StartDate", "SubjectId", "TermDuration", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6341), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN211_Q1_10_123456", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 4, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6342) },
                    { 2, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6351), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN211_Q2_5_654321", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 4, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6352) },
                    { 3, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6368), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN221_Q1_10_789012", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 5, "Multiple Choice", "OK.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6369) },
                    { 4, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6377), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN221_Q2_5_210987", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 5, "Multiple Choice", "OK.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6378) },
                    { 5, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6386), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN231_Q1_10_345678", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 6, "Multiple Choice", "The exam can be used for testing.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6388) },
                    { 6, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6396), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRN231_Q2_5_876543", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 6, "Multiple Choice", "The exam can be used for testing.", true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6397) },
                    { 7, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6404), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6402), null, "MAE101_Q1_10_234567", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6401), 4, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6405) },
                    { 8, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6412), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6411), null, "MAE101_Q2_5_765432", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6410), 4, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6413) },
                    { 9, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6419), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 2, "Multiple Choice", null, null, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6420) },
                    { 10, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6428), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "NWC203c_Q2_5_876543", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 3, "Multiple Choice", null, true, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6429) },
                    { 11, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6437), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ENM401_Q1_10_111222", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 4, "Multiple Choice", "This exam covers the material from Block 10.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6438) },
                    { 12, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6445), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ENM401_Q2_5_222111", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "30'", 4, "Reading", "This exam covers the material from Block 10.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6446) },
                    { 13, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6454), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ENM401_Q3_7_222333", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 4, "Writing", "This exam covers the material from Block 10..", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6455) },
                    { 14, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6853), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ENM401_Q4_9_333111", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "30'", 4, "Listening", "This exam covers the material from Block 10.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6855) },
                    { 15, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6866), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "ECO121_Q1_10_333444", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 5, "Multiple Choice", "OK.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6866) },
                    { 16, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6874), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "ECO121_Q2_5_444333", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 5, "Multiple Choice", "OK.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6875) },
                    { 17, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6882), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ECO201_Q1_10_555666", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 6, "Multiple Choice", "The exam can be used for testing.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6884) },
                    { 18, 3, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6891), 2, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ECO201_Q2_5_666555", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 6, "Multiple Choice", "The exam can be used for testing.", true, 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6892) },
                    { 19, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6899), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6898), null, "ACC101_Q1_10_777888", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6897), 9, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6900) },
                    { 20, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6907), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6905), null, "ACC101_Q2_5_888777", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6904), 9, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6908) },
                    { 21, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6915), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6914), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6913), 10, "Block 10 (10 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6916) },
                    { 22, null, null, 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6984), 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6983), null, "MKT101_Q2_5_000999", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6982), 10, "Block 5 (5 weeks)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6985) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7270), 1, 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7272) },
                    { 2, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7275), 2, 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7276) },
                    { 3, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7280), 11, 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7281) },
                    { 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7284), 12, 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7285) },
                    { 5, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7288), 13, 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7289) },
                    { 6, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7291), 14, 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7293) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserFaculty_CampusId",
                table: "CampusUserFaculty",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserFaculty_FacultyId",
                table: "CampusUserFaculty",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserFaculty_UserId",
                table: "CampusUserFaculty",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_CampusId",
                table: "CampusUserSubject",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_SemesterId",
                table: "CampusUserSubject",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_SubjectId",
                table: "CampusUserSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_UserId",
                table: "CampusUserSubject",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CampusId",
                table: "Exams",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CreaterId",
                table: "Exams",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamStatusId",
                table: "Exams",
                column: "ExamStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SemesterId",
                table: "Exams",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_MenuId",
                table: "MenuRoles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_ReportId",
                table: "ReportFiles",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ExamId",
                table: "Reports",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacultyId",
                table: "Subjects",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_UserId",
                table: "UserHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampusId",
                table: "Users",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampusUserFaculty");

            migrationBuilder.DropTable(
                name: "CampusUserSubject");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "ReportFiles");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "ExamStatuses");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class innit : Migration
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
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
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
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_CampusUserFaculty_Faculties",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId");
                    table.ForeignKey(
                        name: "FK_CampusUserFaculty_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "CampusUserSubject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                    CreaterId = table.Column<int>(type: "int", nullable: true),
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
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestTimeInMinute = table.Column<int>(type: "int", nullable: true)
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
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    { 1, "Ha Noi", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6408), null, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6431) },
                    { 2, "Da Nang", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6436), null, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6437) },
                    { 3, "Can Tho", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6441), null, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6443) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6447), null, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6449) },
                    { 5, "Quy Nhon", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6454), null, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(6455) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7056), "Unassigned", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7061) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7065), "Assigned", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7066) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7068), "Awaiting Lecturer Confirm", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7070) },
                    { 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7072), "Planned", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7074) },
                    { 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7076), "Error", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7078) },
                    { 6, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7080), "OK", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7081) },
                    { 7, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7083), "Completed", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7085) },
                    { 8, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7088), "Rejected", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7089) }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "CreateDate", "Description", "FacultyName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7187), "The study and creation of systems that can perform tasks requiring human-like intelligence, such as learning and problem-solving.", "Artificial Intelligence", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7188) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7194), "A field focusing on core skills in business, law, operations, and communication for professional success.", "BLOC", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7196) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7199), "The study of managing and overseeing business operations, including planning, organization, and leadership", "Business Administration", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7200) },
                    { 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7204), "The study of the Chinese language, culture, and communication skills.", "Chinese", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7204) },
                    { 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7207), "The study of computation, algorithms, programming, and the design of software and hardware systems.", "Computer Science", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7209) },
                    { 6, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7212), "An introduction to core computing concepts, including basic programming, data processing, and system operations.", "Computing Fundamental", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7213) },
                    { 7, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7216), "The study of the English language, including grammar, literature, and communication skills.", "English", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7217) },
                    { 8, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7220), "A course designed to enhance language skills in reading, writing, speaking, and listening, preparing students for academic or professional English proficiency.", "English Preparation Course", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7222) },
                    { 9, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7225), "Additional sessions designed to provide supplementary learning in various subjects to reinforce or expand students' knowledge and skills.", "Extra Classes", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7226) },
                    { 10, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7229), "The study of managing money, investments, financial systems, and the principles of budgeting and financial decision-making.", "Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7230) },
                    { 11, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7233), "A program or course designed for students who have completed an undergraduate degree, focusing on advanced studies and specialized knowledge in a particular field.", "Graduate", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7234) },
                    { 12, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7238), "The art of creating visual content to communicate messages, combining elements like typography, images, and colors to design logos, advertisements, websites, and more.", "Graphic Design", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7239) },
                    { 13, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7242), "The practice of protecting and managing information systems to ensure their confidentiality, integrity, and availability, focusing on risk management and security measures.", "Information Assurance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7243) },
                    { 14, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7246), "A focused study of advanced topics in IT, such as network management, cybersecurity, software development, and database administration.", "Information Technology Specialization", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7247) },
                    { 15, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7250), "The study of the Japanese language, including its grammar, vocabulary, writing systems, and cultural aspects.", "Japanese", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7251) },
                    { 16, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7254), "The study of the Korean language, including its grammar, vocabulary, writing systems (Hangul), and cultural nuances.", "Korean", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7255) },
                    { 17, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7259), "A practical learning environment where students conduct experiments, apply theoretical knowledge, and gain hands-on experience in various scientific or technical fields.", "LAB", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7260) },
                    { 18, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7263), "A program or initiative that offers an immersive learning experience focused on British culture, language, and educational practices, often designed to enhance students' understanding of the UK.", "Little UK", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7264) },
                    { 19, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7267), "The study of planning, organizing, and overseeing resources and processes to achieve organizational goals efficiently and effectively.", "Management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7269) },
                    { 20, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7272), "The study of numbers, quantities, shapes, and patterns, focusing on problem-solving, logic, and abstract reasoning.", "Mathematics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7273) },
                    { 21, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7277), "The study of using various media formats—such as text, audio, video, and graphics—to communicate information effectively across different platforms and technologies.", "Multimedia Communications", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7278) },
                    { 22, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7281), "A practical learning process where employees acquire skills and knowledge by performing tasks and duties in a real work environment under the guidance of experienced colleagues or supervisors.", "On the job training", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7282) },
                    { 23, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7286), "The application of mathematical models, statistical analysis, and optimization techniques to solve complex decision-making problems and improve efficiency in business, logistics, and other systems.", "OR", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7287) },
                    { 24, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7290), "The practice of improving physical fitness and performance through exercises, workouts, and conditioning to enhance strength, endurance, and overall health.", "Physical Training", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7291) },
                    { 25, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7294), "A leading enterprise resource planning (ERP) software that helps organizations manage business processes by integrating key functions like finance, supply chain, human resources, and customer relationships.", "SAP", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7296) },
                    { 26, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7370), "Non-technical skills that relate to how individuals interact with others, such as communication, teamwork, problem-solving, adaptability, and leadership.", "Soft Skill", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7371) },
                    { 27, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7376), "The application of engineering principles to the design, development, testing, and maintenance of software systems, ensuring they are efficient, reliable, and scalable.", "Software Engineering", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7377) },
                    { 28, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7380), "A program or course focused on teaching the fundamentals of launching and managing a new business, including idea development, business planning, marketing, and financial management.", "Start Your Business", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7381) },
                    { 29, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7384), "The study and practice of playing musical instruments that are indigenous to specific cultures, often passed down through generations, such as the violin, guitar, sitar, or drums.", "Traditional Instrument", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7385) },
                    { 30, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7388), "The study of the Vietnamese language, including its grammar, vocabulary, pronunciation, and cultural context.", "Vietnamese", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7389) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5673), null, "/Admin/UserManagement", "User Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5683) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5687), null, "/Admin/UserActivityLog", "User Activity Log", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5688) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5691), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5693) },
                    { 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5696), null, "/HeadDepartment/ExamList", "Assign Teacher To Review", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5697) },
                    { 5, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5700), null, "/Lecture/ExamList", "Exam List", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5702) },
                    { 6, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5704), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5706) },
                    { 7, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5708), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5709) },
                    { 8, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5712), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5714) },
                    { 9, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5717), null, "/Examiner/HeadDeparmentManagement", "Head Department Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5718) },
                    { 10, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5721), null, "/Examiner/CreateExam", "Create Exam", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5723) },
                    { 11, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5726), null, "/HeadDepartment/LecturerManagement", "Lecturer Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5727) },
                    { 12, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5729), null, "/Examiner/OverallReport", "Overall Report", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5731) },
                    { 13, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5733), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5734) },
                    { 14, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5736), null, "/Examiner/ReviewReport", "Review Report", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5738) },
                    { 15, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5740), null, "/HeadDepartment/SubjectList", "Subject List", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5741) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6149), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp24", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6152) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6158), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su24", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6159) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6165), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa24", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6166) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7524), "Admin", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7527) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7531), "Examiner", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7533) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7553), "Lecturer", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7555) },
                    { 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7558), "Head of Department", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7560) },
                    { 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7566), "Curriculum Development", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7568) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5981), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5984) },
                    { 2, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5988), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5989) },
                    { 7, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5992), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5993) },
                    { 8, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5996), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(5997) },
                    { 13, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6000), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6002) },
                    { 3, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6005), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6006) },
                    { 9, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6012), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6013) },
                    { 10, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6023), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6025) },
                    { 12, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6027), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6028) },
                    { 14, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6008), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6010) },
                    { 5, 3, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6033), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6034) },
                    { 4, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6016), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6017) },
                    { 6, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6020), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6021) },
                    { 11, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6030), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6031) },
                    { 15, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6036), new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(6038) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "FacultyId", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8719), 1, null, "ADY201m", "Data Science with Python & SQL", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8724) },
                    { 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8730), 1, null, "AID301c", "AI in Production", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8732) },
                    { 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8736), 1, null, "AIE301m", "AI for Trading", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8737) },
                    { 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8741), 1, null, "AIG202c", "Artificial Intelligence", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8742) },
                    { 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8747), 1, null, "AIH301m", "AI in Healthcare", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8749) },
                    { 6, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8752), 1, null, "AIL303m", "Machine Learning", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8753) },
                    { 7, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8758), 1, null, "AIM301m", "AI for Medicine", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8760) },
                    { 8, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8764), 1, null, "ASR301c", "AI for Scientific Research", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8765) },
                    { 9, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8769), 1, null, "BDI301c", "Big Data", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8770) },
                    { 10, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8775), 1, null, "BDI302c", "Big Data", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8776) },
                    { 11, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8781), 1, null, "CPV301", "Computer Vision", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8782) },
                    { 12, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8786), 1, null, "DAP391m", "AI-DS Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8787) },
                    { 13, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8790), 1, null, "DAT301m", "AI Development with TensorFlow", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8791) },
                    { 14, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8794), 1, null, "DBM302m", "Data Mining", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8796) },
                    { 15, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8799), 1, null, "DPL301m", "Deep Learning", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8800) },
                    { 16, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8803), 1, null, "DPL302m", "Deep Learning", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8805) },
                    { 17, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8808), 1, null, "DSR301m", "Applied Data Science with R", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8809) },
                    { 18, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8812), 1, null, "DWP301c", "Web Development with Python", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8814) },
                    { 19, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8818), 1, null, "NLP301c", "Natural Language Processing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8819) },
                    { 20, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8823), 1, null, "PRP201c", "Python programming", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8824) },
                    { 21, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8829), 1, null, "REL301m", "Reinforcement Learning", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8830) },
                    { 22, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8833), 2, null, "BDP306b", "Final Project - Blockchain Development in Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8835) },
                    { 23, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8839), 3, null, "ACC101", "Principles of Accounting", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8840) },
                    { 24, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8844), 3, null, "ACC302", "Managerial Accounting", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8845) },
                    { 25, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8962), 3, null, "ACC305", "Financial Statement Analysis", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8965) },
                    { 26, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8969), 3, null, "ADS301m", "Google Ads và Seo", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8970) },
                    { 27, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8974), 3, null, "CAA201", "Communications and advertising", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8976) },
                    { 28, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8979), 3, null, "CIH201", "Contemporary issues in hotel and tourism management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8980) },
                    { 29, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8983), 3, null, "DMA301m", "Digital Marketing Analytics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8984) },
                    { 30, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8987), 3, null, "DMS301m", "Digital Marketing Strategy", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8989) },
                    { 31, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8994), 3, null, "ECO102", "Business Environment", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8995) },
                    { 32, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8999), 3, null, "ECO111", "Microeconomics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9000) },
                    { 33, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9003), 3, null, "ECO201", "International Economics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9005) },
                    { 34, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9008), 3, null, "EXE101", "Experiential Entrepreneurship 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9009) },
                    { 35, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9012), 3, null, "EXE201", "Experiential Entrepreneurship 2", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9014) },
                    { 36, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9017), 3, null, "FIM302c", "Financial modelling", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9018) },
                    { 37, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9022), 3, null, "FIN201", "Monetary Economics and Global Economy", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9023) },
                    { 38, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9026), 3, null, "FIN202", "Principles of Corporate Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9027) },
                    { 39, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9030), 3, null, "FIN301", "Financial Markets and Institutions", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9031) },
                    { 40, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9034), 3, null, "FIN303", "Advanced Corporate Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9035) },
                    { 41, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9039), 3, null, "FIN306c", "Financial Reporting", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9041) },
                    { 42, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9044), 3, null, "FIN308", "International Corporate Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9045) },
                    { 43, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9048), 3, null, "FIN402", "Derivatives", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9050) },
                    { 44, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9053), 3, null, "FIN403", "Mergers and Acquisitions", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9054) },
                    { 45, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9057), 3, null, "HRM201c", "Human Resource Management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9058) },
                    { 46, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9061), 3, null, "IBC201", "Cross Cultural Management and Negotiation", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9062) },
                    { 47, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9067), 3, null, "IBF301", "International Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9068) },
                    { 48, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9074), 3, null, "IBI101", "Introduction to International business", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9076) },
                    { 49, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9081), 3, null, "IBS301m", "International Business Strategy", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9083) },
                    { 50, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9088), 3, null, "IEI301", "Import and Export", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9090) },
                    { 51, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9096), 3, null, "IIP301", "International payment", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9098) },
                    { 52, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9102), 3, null, "IPR102", "Intellectual Property Rights", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9104) },
                    { 53, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9109), 3, null, "LAW102", "Business Law and Ethics Fundamentals", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9111) },
                    { 54, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9116), 3, null, "LAW201c", "International Business Law", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9118) },
                    { 55, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9123), 3, null, "LOG311", "Customs Operations", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9125) },
                    { 56, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9130), 3, null, "MGT103", "Introduction to Management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9133) },
                    { 57, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9138), 3, null, "MKT101", "Marketing Principles", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9140) },
                    { 58, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9145), 3, null, "MKT201", "Consumer Behavior", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9147) },
                    { 59, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9153), 3, null, "MKT202", "Services Marketing Management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9155) },
                    { 60, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9160), 3, null, "MKT205c", "International Marketing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9162) },
                    { 61, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9169), 3, null, "MKT208c", "Social media marketing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9171) },
                    { 62, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9176), 3, null, "MKT209m", "Content Marketing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9178) },
                    { 63, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9182), 3, null, "MKT301", "Marketing Research", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9184) },
                    { 64, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9190), 3, null, "MKT304", "Integrated Marketing Communications", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9192) },
                    { 65, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9197), 3, null, "MKT309m ", "Omnichannel marketing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9199) },
                    { 66, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9203), 3, null, "OBE102c", "Organizational Behavior", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9206) },
                    { 67, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9210), 3, null, "RES201", "Food Preparation & Science", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9213) },
                    { 68, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9218), 3, null, "RES213", "Wines, Beers, Spirits 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9220) },
                    { 69, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9225), 3, null, "RES301", "Food and Beverage Cost Control", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9227) },
                    { 70, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9232), 3, null, "RMB301", "Business Research Methods", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9234) },
                    { 71, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9242), 3, null, "RMB302", "Research Methods & Quantitative Analysis", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9246) },
                    { 72, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9252), 3, null, "SAL301", "Professional Selling", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9253) },
                    { 73, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9256), 3, null, "SCM201", "Supply Chain Management", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9257) },
                    { 74, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9342), 3, null, "SCM301m", "Procurement and Global Sourcing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9343) },
                    { 75, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9348), 3, null, "SYB302c", "Entrepreneurship", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9349) },
                    { 76, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9352), 6, null, "CSD201", "Data Structures and Algorithms", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9353) },
                    { 77, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9357), 6, null, "CSD201-EX", "Data Structures and Algorithms", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9358) },
                    { 78, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9362), 6, null, "CSD203", "Data Structures and Algorithm with Python", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9363) },
                    { 79, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9366), 6, null, "DBI202", "Introduction to Databases", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9368) },
                    { 80, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9371), 6, null, "DBI202-EX", "Introduction to Databases", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9372) },
                    { 81, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9377), 6, null, "FER202", "Front-End web development with React", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9379) },
                    { 82, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9382), 6, null, "JFE301", "Japanese IT Fundamentals", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9383) },
                    { 83, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9387), 6, null, "OSG202", "Operating Systems", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9388) },
                    { 84, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9392), 6, null, "PFP191", "Programming Fundamentals with Python", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9393) },
                    { 85, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9397), 6, null, "PRE201c", "Excel Skills for Business", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9398) },
                    { 86, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9401), 6, null, "PRF192", "Programming Fundamentals", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9403) },
                    { 87, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9406), 6, null, "PRF192-EX", "Programming Fundamentals", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9408) },
                    { 88, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9412), 6, null, "PRJ301", "Java Web Application Development", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9413) },
                    { 89, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9417), 6, null, "PRJ301-EX", "Java Web Application Development", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9418) },
                    { 90, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9422), 6, null, "PRJ302", "Java Web Application Development", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9423) },
                    { 91, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9427), 6, null, "PRN212", "Basis Cross-Platform Application Programming With .NET", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9428) },
                    { 92, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9432), 6, null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9433) },
                    { 93, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9436), 6, null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9437) },
                    { 94, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9440), 6, null, "PRN292c", "C# và .NET", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9441) },
                    { 95, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9445), 6, null, "PRO192", "Object-Oriented Programming", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9446) },
                    { 96, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9449), 6, null, "PRO192-EX", "Object-Oriented Programming", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9450) },
                    { 97, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9454), 6, null, "PRO192c", "Object Oriented Programming with Java", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9455) },
                    { 98, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9458), 6, null, "PRU212", "C# Programming and Unity", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9459) },
                    { 99, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9463), 6, null, "SDN302", "Server-Side development with NodeJS, Express, and MongoDB", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9465) },
                    { 100, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9468), 6, null, "WDP301", "Web Development Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9469) },
                    { 101, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9474), 6, null, "WED201c", "Web Design", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9475) },
                    { 102, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9479), 7, null, "CHN113", "Elementary Chinese 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9480) },
                    { 103, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9484), 7, null, "CHN113-EX", "Elementary Chinese 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9485) },
                    { 104, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9488), 7, null, "CHN123", "Elementary Chinese 2", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9489) },
                    { 105, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9493), 7, null, "CHN123-EX", "Elementary Chinese 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9494) },
                    { 106, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9497), 7, null, "CHN132c", "Elementary Chinese 3", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9498) },
                    { 107, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9502), 7, null, "CMC201c", "Creative Writing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9503) },
                    { 108, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9506), 7, null, "EAW212", "Academic English Writing 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9508) },
                    { 109, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9512), 7, null, "EAW222", "Academic English Writing 2", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9513) },
                    { 110, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9517), 7, null, "EBC301c", "Business English Communication", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9518) },
                    { 111, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9522), 7, null, "ECN101", "Integrated Chinese 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9524) },
                    { 112, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9528), 7, null, "ECN221", "Integrated Chinese 3", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9530) },
                    { 113, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9533), 7, null, "ECR202", "Critical Reading in English", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9535) },
                    { 114, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9539), 7, null, "ELI301", "Translation 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9540) },
                    { 115, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9544), 7, null, "ELI401", "Translation 2", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9545) },
                    { 116, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9548), 7, null, "ELR301", "Research Methods", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9549) },
                    { 117, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9552), 7, null, "ELS401c", "Academic Listening and Speaking", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9554) },
                    { 118, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9557), 7, null, "ENB301", "Business Writing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9559) },
                    { 119, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9562), 7, null, "ENG303", "Advanced English Grammar", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9563) },
                    { 120, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9567), 7, null, "ENM211c", "Business English Communication 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9568) },
                    { 121, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9572), 7, null, "ENM301", "Intermediate Business English", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9573) },
                    { 122, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9577), 7, null, "ENM302", "Business English– Level 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9578) },
                    { 123, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9687), 7, null, "ENM401", "Upper Intermediate Business English", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9691) },
                    { 124, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9699), 7, null, "ENP102", "English phonetics and phonology in use", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9702) },
                    { 125, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9710), 7, null, "ENW492c", "Writing Research Papers", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9713) },
                    { 126, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9720), 7, null, "EPE301c", "Professional Ethics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9723) },
                    { 127, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9731), 7, null, "ERW412", "English Read-Think-Write 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9734) },
                    { 128, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9741), 7, null, "LIT301", "British and American Literature", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9745) },
                    { 129, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9752), 7, null, "LTG202", "Introduction to linguistics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9755) },
                    { 130, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9763), 7, null, "SEM101", "Semantics", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9766) },
                    { 131, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9775), 7, null, "SSC302c", "Advanced Presentation skills", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9779) },
                    { 132, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9785), 8, null, "ENT104", "English 2 (Top Notch 1)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9786) },
                    { 133, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9790), 8, null, "ENT203", "English 3 (Top Notch 2)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9791) },
                    { 134, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9794), 8, null, "TRS401", "English 4 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9795) },
                    { 135, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9798), 8, null, "TRS501", "English 5 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9800) },
                    { 136, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9804), 8, null, "TRS601", "English 6 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9805) },
                    { 137, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9809), 8, null, "TRS601-CULI-TL", "English 6 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9812) },
                    { 138, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9817), 8, null, "TRS601-KBU-TL", "English 6 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9819) },
                    { 139, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9825), 8, null, "TRS601-TAR UMT-ML", "English 6 (University success)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9827) },
                    { 140, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9832), 9, null, "EXE101g", "Group Experiential Entrepreneurship 1", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9835) },
                    { 141, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9841), 9, null, "EXE201g", "Experiential Entrepreneurship 2", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9844) },
                    { 142, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9849), 11, null, "AIP490", "AI Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9851) },
                    { 143, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9856), 11, null, "AIP491", "AI Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9859) },
                    { 144, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9864), 11, null, "ELT492", "Graduation Thesis - English studies", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9866) },
                    { 145, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9871), 11, null, "GDP491", "Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9873) },
                    { 146, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9878), 11, null, "GDP492", "Capstone Project Graphic Design - Animation", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9881) },
                    { 147, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9886), 11, null, "GDP493", "Capstone Project Graphic Design - Interaction Design", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9889) },
                    { 148, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9894), 11, null, "GDP494", "Capstone Project Graphic Design - Communication Design", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9897) },
                    { 149, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9902), 11, null, "GRA497", "Capstone Project - Multimedia and Communication", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9904) },
                    { 150, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9909), 11, null, "GRF491", "Graduation Thesis - Finance", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9911) },
                    { 151, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9918), 11, null, "GRI491", "Graduation Thesis - International Business", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9920) },
                    { 152, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9925), 11, null, "GRM491", "Graduation Thesis - Marketing", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9927) },
                    { 153, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9979), 11, null, "GRP490", "Graduation thesis (Business plan)", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9982) },
                    { 154, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9989), 11, null, "IAP491", "IA Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(9993) },
                    { 155, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local), 11, null, "IOP490", "IoT Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(3) },
                    { 156, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(11), 11, null, "JGP491", "Graduation Project - Japanese Studies", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(14) },
                    { 157, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(21), 11, null, "SAP490", "SAP Interdisciplinary Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(25) },
                    { 158, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(33), 11, null, "SEP490", "SE Capstone Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(36) },
                    { 159, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(44), 12, null, "ADB201", "Book Design & Printing Technology", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(45) },
                    { 160, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(48), 12, null, "ADE301", "Visual Communication Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(49) },
                    { 161, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(55), 12, null, "ADH301", "Mobility Applications Design 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(56) },
                    { 162, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(59), 12, null, "ADI201", "Brand Identity Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(60) },
                    { 163, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(65), 12, null, "ADP301", "Packaging design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(67) },
                    { 164, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(72), 12, null, "ADT401", "Mobility Applications Design 2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(75) },
                    { 165, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(80), 12, null, "AET102", "Aesthetic", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(83) },
                    { 166, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(89), 12, null, "AFA201", "Human Anatomy for Artis", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(91) },
                    { 167, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(97), 12, null, "AGD301", "Information Graphic Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(100) },
                    { 168, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(106), 12, null, "AMR401", "3D Modeling & Rigging", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(108) },
                    { 169, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(114), 12, null, "ANA401", "3D Character Animation", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(116) },
                    { 170, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(126), 12, null, "ANB401", "Background Painting for Animation", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(127) },
                    { 171, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(131), 12, null, "ANC301", "Character Development", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(132) },
                    { 172, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(234), 12, null, "ANO301c", "Visual development for digital design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(235) },
                    { 173, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(239), 12, null, "ANS201", "Idea & Script Development", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(241) },
                    { 174, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(244), 12, null, "ANS301", "Storyboarding", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(245) },
                    { 175, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(249), 12, null, "ANT401", "Traditional Animation Principles", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(250) },
                    { 176, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(253), 12, null, "CAD201", "Water Color", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(254) },
                    { 178, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(258), 12, null, "DID301", "Data visualization & Infographic design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(259) },
                    { 179, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(262), 12, null, "DRD204", "Drawing - Speed drawing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(264) },
                    { 180, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(267), 12, null, "DRP101", "Drawing - Plaster Statue, Portrait", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(268) },
                    { 181, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(271), 12, null, "DRS102", "Drawing - Form, Still-life", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(272) },
                    { 182, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(277), 12, null, "DTG102", "Visual Design Tools", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(278) },
                    { 183, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(281), 12, null, "DTG302", "Visual Effects - Principles of Compositing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(282) },
                    { 184, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(285), 12, null, "DTG303", "Principles of Animation", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(286) },
                    { 185, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(289), 12, null, "GDF201", "Fundamental of Graphic Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(291) },
                    { 186, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(294), 12, null, "HOA102", "Art History", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(295) },
                    { 187, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(298), 12, null, "PFD201", "Photography for Designer", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(299) },
                    { 188, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(302), 12, null, "PST202", "Perspective", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(305) },
                    { 189, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(308), 12, null, "TPG203", "Basic typography & Layout", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(309) },
                    { 190, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(312), 12, null, "TPG302", "Typography & E-publication", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(313) },
                    { 191, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(316), 12, null, "VCM202", "Visual Communication", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(317) },
                    { 192, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(322), 12, null, "VNC104", "Vietnamese Culture", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(323) },
                    { 193, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(326), 12, null, "WDL202", "Web layout design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(327) },
                    { 194, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(331), 12, null, "WDU202c", "UI/UX Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(332) },
                    { 195, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(335), 12, null, "WIR201", "Interaction design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(336) },
                    { 196, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(340), 13, null, "CES202", "System Support and Trouble Shooting", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(341) },
                    { 197, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(344), 13, null, "CRY303c", "Applied Cryptography", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(345) },
                    { 198, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(348), 13, null, "DBS401", "Database Security", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(349) },
                    { 199, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(352), 13, null, "FRS301", "Digital Forensics", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(354) },
                    { 200, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(357), 13, null, "FRS401c", "Network Forensics", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(359) },
                    { 201, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(362), 13, null, "HOD401", "Ethical Hacking and Offensive Security", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(364) },
                    { 202, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(367), 13, null, "IAA202", "Risk Management in Information Systems", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(369) },
                    { 203, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(372), 13, null, "IAM302", "Malware Analysis and Reverse Engineering", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(373) },
                    { 204, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(376), 13, null, "IAO201c", "Introduction to Information Assurance", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(377) },
                    { 205, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(381), 13, null, "IAP301", "Policy Development in Information Assurance", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(382) },
                    { 206, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(385), 13, null, "IAR401c", "Incident Response", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(387) },
                    { 207, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(390), 13, null, "IAW301", "Web security", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(392) },
                    { 208, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(395), 13, null, "NWC204", "Computer Networking", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(396) },
                    { 209, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(399), 13, null, "OSP201", "Open Source Platform and Network Administration", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(400) },
                    { 210, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(403), 14, null, "CEA201", "Computer Organization and Architecture", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(404) },
                    { 211, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(407), 14, null, "DGT301", "Digital Signal Processing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(409) },
                    { 212, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(413), 14, null, "EDT202c", "Emerging Digital Technologies", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(414) },
                    { 213, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(417), 14, null, "ESP301m", "Microcontroller & embedded system programming", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(418) },
                    { 214, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(421), 14, null, "IOP391", "IoT application development project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(423) },
                    { 215, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(426), 14, null, "IOT102", "Internet of things", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(427) },
                    { 216, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(431), 14, null, "ISC301", "e-Commerce", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(432) },
                    { 217, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(436), 14, null, "PRC392c", "Cloud Computing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(437) },
                    { 218, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(440), 14, null, "SST301", "Sensor Technology", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(441) },
                    { 219, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(444), 15, null, "JIJ301", "Basic issues of Japanese lexicology & phonetics", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(446) },
                    { 220, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(449), 15, null, "JIS401", "Japanese in Software", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(450) },
                    { 221, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(453), 15, null, "JIT401", "Information Technology Japanese", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(459) },
                    { 222, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(505), 15, null, "JJL301", "Japanese Literature", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(507) },
                    { 223, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(511), 15, null, "JPD113", "Elementary Japanese 1-A1.1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(512) },
                    { 224, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(515), 15, null, "JPD113-EX", "Japanese 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(517) },
                    { 225, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(520), 15, null, "JPD123", "Elementary Japanese 1-A1.2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(522) },
                    { 226, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(526), 15, null, "JPD133", "Elementary Japanese 1-A1/A2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(527) },
                    { 227, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(530), 15, null, "JPD316", "Intermediate Japanese 1-B1/B2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(532) },
                    { 228, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(535), 15, null, "JPD326", "Japanese Intermediate 2-B2.1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(536) },
                    { 229, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(539), 15, null, "JSI201", "Information System", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(540) },
                    { 230, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(543), 16, null, "KLE301", "Korean Literature", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(544) },
                    { 231, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(547), 16, null, "KLI311", "Interpretation 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(550) },
                    { 232, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(553), 16, null, "KLT311", "Translation 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(554) },
                    { 233, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(558), 16, null, "KOR311", "Intermediate Korean Language 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(559) },
                    { 234, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(562), 16, null, "KOR321", "Intermediate Korean Language 2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(563) },
                    { 235, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(566), 16, null, "KOR411", "Intermediate Korean Language 3", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(567) },
                    { 236, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(571), 16, null, "KRC301", "Korean Culture", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(572) },
                    { 237, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(575), 16, null, "KRL312", "Intermediate Korean 3", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(577) },
                    { 238, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(580), 16, null, "KRL322", "Intermediate Korean 4", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(581) },
                    { 239, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(584), 16, null, "KRP301", "Korean Phonetics", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(585) },
                    { 240, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(588), 18, null, "LUK1", "LUK Global 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(590) },
                    { 241, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(593), 18, null, "LUK5", "LUK Global 5", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(595) },
                    { 242, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(598), 20, null, "MAD101", "Discrete mathematics", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(599) },
                    { 243, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(602), 20, null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(603) },
                    { 244, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(606), 20, null, "MAE101-EX", "Mathematics for Engineering", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(607) },
                    { 245, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(610), 20, null, "MAI391", "Mathematics for Machine Learning", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(612) },
                    { 246, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(615), 20, null, "MAS202", "Applied Statistics for Business", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(616) },
                    { 247, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(619), 20, null, "MAS202-EX", "Applied Statistics for Business", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(620) },
                    { 248, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(624), 20, null, "MAS291", "Statistics and Probability", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(625) },
                    { 249, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(629), 21, null, "BDI201", "Brand identity design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(630) },
                    { 250, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(633), 21, null, "BRA301", "Brand Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(634) },
                    { 251, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(637), 21, null, "CCM301", "Crisis Communications Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(639) },
                    { 252, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(642), 21, null, "CCO201", "Corporate Communication", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(643) },
                    { 253, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(648), 21, null, "CSP201m", "Content Strategy for Professionals", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(649) },
                    { 254, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(653), 21, null, "DTG111", "Visual Design Tools 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(654) },
                    { 255, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(657), 21, null, "EVN201", "Event planning", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(658) },
                    { 256, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(662), 21, null, "IFT201c", "Innovation and Future thinking", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(663) },
                    { 257, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(667), 21, null, "IMC301c", "Intergrated Marketing Communication in Digital World", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(668) },
                    { 258, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(672), 21, null, "MCO201m", "Transmedia Storytelling", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(673) },
                    { 259, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(676), 21, null, "MED201", "New Media Technology", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(678) },
                    { 260, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(681), 21, null, "MEP301", "Multimedia Production Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(682) },
                    { 261, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(685), 21, null, "MSM201c", "Meta social media marketing Management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(687) },
                    { 262, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(691), 21, null, "PRE301", "Public Relations principles and strategies", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(692) },
                    { 263, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(696), 21, null, "RMC301m", "Research methods in Communication", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(697) },
                    { 264, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(700), 21, null, "SDP201", "Sound Production", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(701) },
                    { 265, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(704), 21, null, "SEO201c", "Search Engine Optimization", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(705) },
                    { 266, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(709), 21, null, "VDE301", "Digital Video Editing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(710) },
                    { 267, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(713), 21, null, "VDP301", "Search Engine Optimization", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(714) },
                    { 268, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(717), 21, null, "SEO201c", "Video Production", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(718) },
                    { 269, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(865), 21, null, "WMC201", "Media Writing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(867) },
                    { 271, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(876), 22, null, "OJB202", "On-the-job training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(879) },
                    { 272, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(882), 22, null, "OJE202", "On-the-job training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(884) },
                    { 273, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(887), 22, null, "OJP202", "On-the-job training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(888) },
                    { 274, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(892), 22, null, "OJS201", "On-the-job training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(893) },
                    { 275, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(896), 22, null, "OJT202", "On-the-job training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(897) },
                    { 276, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(900), 23, null, "GDQP", "Military training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(901) },
                    { 277, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(905), 23, null, "ORT101", "Orientation", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(906) },
                    { 278, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(909), 23, null, "ORT102", "Orientation IT", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(910) },
                    { 279, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(913), 23, null, "ORT103", "Orientation Examination", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(914) },
                    { 280, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(918), 23, null, "ORT108", "Orientation", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(919) },
                    { 281, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(922), 24, null, "COV111", "Chess 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(927) },
                    { 282, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(931), 24, null, "COV121", "Chess 2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(932) },
                    { 283, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(935), 24, null, "COV131", "Chess 3", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(936) },
                    { 284, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(939), 24, null, "GDQP", "Military training", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(941) },
                    { 285, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(944), 24, null, "OTP101", "Orientation and General Training Program", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(945) },
                    { 286, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(948), 24, null, "VOV-EX", "Vovinam", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(949) },
                    { 287, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(952), 24, null, "VOV114", "Vovinam 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(954) },
                    { 288, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(957), 24, null, "VOV124", "Vovinam 2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(958) },
                    { 289, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(961), 24, null, "VOV134", "Vovinam 3", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(962) },
                    { 290, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(965), 26, null, "HCM202", "Ho Chi Minh Ideology", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(967) },
                    { 291, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(970), 26, null, "MLN111", "Philosophy of Marxism – Leninism", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(972) },
                    { 292, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(974), 26, null, "MLN122", "Political economics of Marxism – Leninism", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(976) },
                    { 293, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(979), 26, null, "MLN131", "Scientific socialism", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(981) },
                    { 294, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(984), 26, null, "MMP201", "Media Psychology", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(986) },
                    { 295, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(989), 26, null, "SSB201", "Advanced Business Communication", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(991) },
                    { 296, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(994), 26, null, "SSG104", "Communication and In-Group Working Skills", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(995) },
                    { 297, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(999), 26, null, "SSG104-EX", "Communication and In-Group Working Skills", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1000) },
                    { 298, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1004), 26, null, "SSL101c", "Academic Skills for University Success", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1006) },
                    { 299, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1009), 26, null, "VNR202", "History of Việt Nam Communist Party", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1010) },
                    { 300, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1013), 27, null, "CSI106", "Introduction to Computer Science", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1014) },
                    { 301, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1017), 27, null, "DTA301", "Data Analysis", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1019) },
                    { 302, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1023), 27, null, "HSF301", "Hibernate and Spring framework", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1025) },
                    { 303, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1028), 27, null, "ISM302", "Enterprise Resource Planning", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1029) },
                    { 304, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1033), 27, null, "ISP392", "Information System Programming Project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1035) },
                    { 305, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1038), 27, null, "ITA203c", "Management information systems", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1039) },
                    { 306, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1043), 27, null, "ITA301", "Information System Analysis and Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1045) },
                    { 307, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1048), 27, null, "ITB302c", "Business Intelligence (BI)", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1049) },
                    { 308, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1053), 27, null, "ITE302c", "Ethics in IT", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1054) },
                    { 309, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1058), 27, null, "ITE303c", "Ethics in IT", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1059) },
                    { 310, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1062), 27, null, "KMS301", "Knowledge management system", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1064) },
                    { 311, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1068), 27, null, "LAB211", "OOP with Java Lab", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1070) },
                    { 312, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1074), 27, null, "LAB211-EX", "OOP with Java Lab", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1075) },
                    { 313, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1079), 27, null, "LAB221c", "Desktop Java lab", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1080) },
                    { 314, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1084), 27, null, "MMA301", "Multiplatform Mobile App Development", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1085) },
                    { 315, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1089), 27, null, "PMG201c", "Project management", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1090) },
                    { 316, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1093), 27, null, "PRM392", "Mobile Programming", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1095) },
                    { 317, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1098), 27, null, "SAP311", "SAP General 1", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1099) },
                    { 318, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1201), 27, null, "SAP321", "SAP General 2", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1204) },
                    { 319, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1208), 27, null, "SAP341", "SAP Application Development with ABAP", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1209) },
                    { 320, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1212), 27, null, "SWD392", "SW Architecture and Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1215) },
                    { 321, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1218), 27, null, "SWE201c", "Introduction to Software Engineering", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1220) },
                    { 322, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1223), 27, null, "SWP391", "Application development project", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1225) },
                    { 323, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1228), 27, null, "SWR302", "Software Requirement", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1230) },
                    { 324, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1233), 27, null, "SWT301", "Software Testing", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1234) },
                    { 325, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1238), 27, null, "WDU203c", "UI/UX Design", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1239) },
                    { 326, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1245), 29, null, "DBA103", "Traditional musical instrument - Dan Bau", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1246) },
                    { 327, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1249), 29, null, "DNG103", "Traditional musical instrument - Dan Nguyet", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1250) },
                    { 328, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1254), 29, null, "DNH103", "Traditional musical instrument - Dan Nhi", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1256) },
                    { 329, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1259), 29, null, "DSA103", "Traditional musical instrument - Sao Truc", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1260) },
                    { 330, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1263), 29, null, "DTB103", "Traditional musical instrument - Dan Ty Ba", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1265) },
                    { 331, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1268), 29, null, "DTR103", "Traditional musical instrument - Dan Tranh", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1269) },
                    { 332, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1273), 29, null, "TRG101", "Traditional musical instrument - Trong", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1274) },
                    { 333, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1277), 29, null, "TRG103", "Traditional musical instrument - Trong", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1278) },
                    { 334, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1281), 29, null, "ÐBA101", "Traditional musical instrument - Dan Bau", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1282) },
                    { 335, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1286), 29, null, "ÐNH101", "Traditional musical instrument - Dan Nhi", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(1287) },
                    { 336, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(872), 21, null, "BCJ201c", "Branding: The Creative Journey", new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(873) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7669), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adminHoaLac", true, true, "adminHoaLac@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7671) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7682), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lienkt@fe.edu.vn", "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7684) },
                    { 3, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7691), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hunglthe160235@fe.edu.vn", "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7693) },
                    { 4, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7701), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoanglm@fe.edu.vn", "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7702) },
                    { 5, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7710), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7712) },
                    { 6, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7720), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7722) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7729), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7731) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7739), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7741) },
                    { 9, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7747), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7749) },
                    { 10, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7756), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7758) },
                    { 11, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7764), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7765) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7950), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7951) },
                    { 13, "TP Hồ Chí Minh", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7959), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7961) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7969), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7970) },
                    { 15, "Hà Nội", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7978), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7979) },
                    { 16, "TP Hồ Chí Minh", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7987), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7988) },
                    { 17, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7996), new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adminDaNang", true, true, "adminDaNang@fpt.edu.vn", "0905123456", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(7998) },
                    { 18, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8006), new DateTime(1992, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangthu@fe.edu.vn", "Hoàng Thư", false, true, "hoangthu@fpt.edu.vn", "0905223344", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8008) },
                    { 19, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8015), new DateTime(1988, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "longnguyen@fe.edu.vn", "Nguyễn Long", true, true, "longnguyen@fpt.edu.vn", "0905111222", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8017) },
                    { 20, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8024), new DateTime(1990, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "huongmai@fe.edu.vn", "Mai Hương", false, true, "huongmai@fpt.edu.vn", "0905667788", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8025) },
                    { 21, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8034), new DateTime(1989, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhkhang@fe.edu.vn", "Minh Khang", true, true, "minhkhang@fpt.edu.vn", "0905553344", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8035) },
                    { 22, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8042), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thanhnam@fe.edu.vn", "Thanh Nam", true, true, "thanhnam@fpt.edu.vn", "0905887766", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8044) },
                    { 23, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8051), new DateTime(1987, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "honganh@fe.edu.vn", "Hồng Ánh", false, true, "honganh@fpt.edu.vn", "0905332211", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8052) },
                    { 24, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8060), new DateTime(1993, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Đức Trường", true, true, "ductruong@fpt.edu.vn", "0905665544", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8061) },
                    { 25, "Đà Nẵng", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8067), new DateTime(1994, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quỳnh Nga", false, true, "quynhnga@fpt.edu.vn", "0905778899", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8069) },
                    { 26, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8075), new DateTime(1978, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adminCanTho", true, true, "adminCanTho@fpt.edu.vn", "0909273648", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8076) },
                    { 27, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8083), new DateTime(1990, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "lethanh@fe.edu.vn", "Lê Thanh", true, true, "lethanh@fpt.edu.vn", "0909111223", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8084) },
                    { 28, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8091), new DateTime(1993, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "baongoc@fe.edu.vn", "Bảo Ngọc", false, true, "baongoc@fpt.edu.vn", "0909123456", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8092) },
                    { 29, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8100), new DateTime(1988, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuananh@fe.edu.vn", "Tuấn Anh", true, true, "tuananh@fpt.edu.vn", "0909789900", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8102) },
                    { 30, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8128), new DateTime(1992, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "nguyetmai@fe.edu.vn", "Nguyệt Mai", false, true, "nguyetmai@fpt.edu.vn", "0909345678", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8129) },
                    { 31, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8137), new DateTime(1984, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "hungpham@fe.edu.vn", "Phạm Hùng", true, true, "hungpham@fpt.edu.vn", "0909988776", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8139) },
                    { 32, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8263), new DateTime(1985, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "thuannguyen@fe.edu.vn", "Nguyễn Thuận", true, true, "thuannguyen@fpt.edu.vn", "0909553321", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8264) },
                    { 33, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8274), new DateTime(1994, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quang Minh", true, true, "quangminh@fpt.edu.vn", "0909988771", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8275) },
                    { 34, "Cần Thơ", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8282), new DateTime(1995, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trang Vy", false, true, "trangvy@fpt.edu.vn", "0909112334", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8284) },
                    { 35, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8293), new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adminHoChiMinh", true, true, "adminHoChiMinh@fpt.edu.vn", "0903344789", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8294) },
                    { 36, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8302), new DateTime(1990, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Dũng", true, true, "hoangdung@fpt.edu.vn", "0909123456", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8303) },
                    { 37, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8311), new DateTime(1992, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thúy Trinh", false, true, "thuytrinh@fpt.edu.vn", "0909786543", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8312) },
                    { 38, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8320), new DateTime(1985, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Danh Diệu", true, true, "danhdieu@fpt.edu.vn", "0909112345", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8321) },
                    { 39, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8329), new DateTime(1987, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", true, true, "thanhnguyen@fpt.edu.vn", "0909567890", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8330) },
                    { 40, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8338), new DateTime(1980, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ngọc Hùng", true, true, "ngochung@fpt.edu.vn", "0902345678", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8339) },
                    { 41, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8348), new DateTime(1983, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Văn Nguyễn", true, true, "vannguyen@fpt.edu.vn", "0909876543", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8349) },
                    { 42, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8357), new DateTime(1992, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Khoa Chính", true, true, "khoachinh@fpt.edu.vn", "0909223345", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8358) },
                    { 43, "Hồ Chí Minh", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8366), new DateTime(1994, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Dương", true, true, "hoangduong@fpt.edu.vn", "0909334455", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8367) },
                    { 44, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8374), new DateTime(1980, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "adminQuyNhon", true, true, "adminQuyNhon@fpt.edu.vn", "0908112233", 1, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8376) },
                    { 45, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8383), new DateTime(1991, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Khánh Phan", true, true, "khangphan@fpt.edu.vn", "0909222333", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8385) },
                    { 46, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8392), new DateTime(1993, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lan", false, true, "hoanglan@fpt.edu.vn", "0909887766", 4, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8393) },
                    { 47, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8401), new DateTime(1986, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ngọc Chiêu", true, true, "ngochieu@fpt.edu.vn", "0909445566", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8402) },
                    { 48, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8409), new DateTime(1988, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tuấn Anh", true, true, "tuananh@fpt.edu.vn", "0908776543", 3, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8411) },
                    { 49, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8419), new DateTime(1982, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hồng Giang", true, true, "honggiang@fpt.edu.vn", "0901234567", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8421) },
                    { 50, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8428), new DateTime(1984, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ngọc Lan", false, true, "ngoclan@fpt.edu.vn", "0907654321", 2, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8430) },
                    { 51, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8437), new DateTime(1992, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Liên", true, true, "hoanglien@fpt.edu.vn", "0909922333", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8439) },
                    { 52, "Quy Nhơn", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8446), new DateTime(1994, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Trang", false, true, "minhtrang@fpt.edu.vn", "0909233445", 5, new DateTime(2024, 12, 3, 18, 57, 46, 433, DateTimeKind.Local).AddTicks(8448) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserFaculty",
                columns: new[] { "Id", "CampusId", "CreateDate", "FacultyId", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2088), 1, null, 9 },
                    { 2, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2096), 2, null, 9 },
                    { 3, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2098), 3, null, 9 },
                    { 4, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2102), 4, null, 10 },
                    { 5, 1, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2104), 5, null, 10 },
                    { 6, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2106), 1, null, 22 },
                    { 7, 2, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2109), 2, null, 23 },
                    { 8, 3, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2111), 3, null, 31 },
                    { 9, 3, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2114), 4, null, 32 },
                    { 10, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2117), 1, null, 40 },
                    { 11, 4, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2119), 5, null, 41 },
                    { 12, 5, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2122), 2, null, 49 },
                    { 13, 5, new DateTime(2024, 12, 3, 18, 57, 46, 434, DateTimeKind.Local).AddTicks(2124), 4, null, 50 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 1, 2, 5 },
                    { 3, 1, 3, 5 },
                    { 4, 1, 4, 6 },
                    { 5, 1, 5, 6 },
                    { 6, 2, 1, 20 },
                    { 7, 2, 2, 20 },
                    { 8, 2, 3, 21 },
                    { 9, 2, 4, 21 },
                    { 10, 2, 5, 21 },
                    { 11, 3, 6, 29 },
                    { 12, 3, 7, 29 },
                    { 13, 3, 8, 29 },
                    { 14, 3, 9, 30 },
                    { 15, 3, 10, 30 },
                    { 16, 4, 6, 38 },
                    { 17, 4, 7, 38 },
                    { 18, 4, 8, 38 },
                    { 19, 4, 9, 39 },
                    { 20, 4, 10, 39 },
                    { 21, 5, 11, 47 },
                    { 22, 5, 12, 47 },
                    { 23, 5, 13, 48 },
                    { 24, 5, 14, 48 },
                    { 25, 5, 15, 48 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "AssignedUserId", "AssignmentDate", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDate", "ExamDuration", "ExamStatusId", "ExamType", "GeneralFeedback", "IsReady", "SemesterId", "StartDate", "SubjectId", "TermDuration", "TestTimeInMinute", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADY201m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10", 0, null },
                    { 2, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AID301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10", 0, null },
                    { 3, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIE301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10", 0, null },
                    { 4, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIG202c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Block 10", 0, null },
                    { 5, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIH301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10", 0, null },
                    { 6, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIL303m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10", 0, null },
                    { 7, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIM301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10", 0, null },
                    { 8, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASR301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10", 0, null },
                    { 9, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Block 10", 0, null },
                    { 10, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI302c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Block 10", 0, null },
                    { 11, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CPV301_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Block 10", 0, null },
                    { 12, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAP391m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Block 10", 0, null },
                    { 13, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAT301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Block 10", 0, null },
                    { 14, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DBM302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Block 10", 0, null },
                    { 15, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Block 10", 0, null },
                    { 16, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Block 10", 0, null },
                    { 17, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DSR301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Block 10", 0, null },
                    { 18, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DWP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Block 10", 0, null },
                    { 19, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NLP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "Block 10", 0, null },
                    { 20, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRP201c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Block 10", 0, null },
                    { 21, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADY201m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10", 0, null },
                    { 22, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AID301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10", 0, null },
                    { 23, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIE301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10", 0, null },
                    { 24, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIG202c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Block 10", 0, null },
                    { 25, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIH301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10", 0, null },
                    { 26, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIL303m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10", 0, null },
                    { 27, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIM301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10", 0, null },
                    { 28, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASR301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10", 0, null },
                    { 29, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Block 10", 0, null },
                    { 30, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI302c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Block 10", 0, null },
                    { 31, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CPV301_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Block 10", 0, null },
                    { 32, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAP391m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Block 10", 0, null },
                    { 33, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAT301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Block 10", 0, null },
                    { 34, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DBM302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Block 10", 0, null },
                    { 35, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Block 10", 0, null },
                    { 36, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Block 10", 0, null },
                    { 37, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DSR301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Block 10", 0, null },
                    { 38, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DWP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Block 10", 0, null },
                    { 39, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NLP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "Block 10", 0, null },
                    { 40, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRP201c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Block 10", 0, null },
                    { 41, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADY201m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10", 0, null },
                    { 42, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AID301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10", 0, null },
                    { 43, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIE301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10", 0, null },
                    { 44, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIG202c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Block 10", 0, null },
                    { 45, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIH301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10", 0, null },
                    { 46, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIL303m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10", 0, null },
                    { 47, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIM301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10", 0, null },
                    { 48, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASR301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10", 0, null },
                    { 49, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Block 10", 0, null },
                    { 50, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI302c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Block 10", 0, null },
                    { 51, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CPV301_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Block 10", 0, null },
                    { 52, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAP391m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Block 10", 0, null },
                    { 53, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAT301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Block 10", 0, null },
                    { 54, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DBM302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Block 10", 0, null },
                    { 55, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Block 10", 0, null },
                    { 56, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Block 10", 0, null },
                    { 57, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DSR301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Block 10", 0, null },
                    { 58, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DWP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Block 10", 0, null },
                    { 59, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NLP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "Block 10", 0, null },
                    { 60, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRP201c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Block 10", 0, null },
                    { 61, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADY201m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10", 0, null },
                    { 62, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AID301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10", 0, null },
                    { 63, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIE301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10", 0, null },
                    { 64, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIG202c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Block 10", 0, null },
                    { 65, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIH301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10", 0, null },
                    { 66, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIL303m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10", 0, null },
                    { 67, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIM301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10", 0, null },
                    { 68, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASR301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10", 0, null },
                    { 69, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Block 10", 0, null },
                    { 70, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI302c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Block 10", 0, null },
                    { 71, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CPV301_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Block 10", 0, null },
                    { 72, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAP391m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Block 10", 0, null },
                    { 73, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAT301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Block 10", 0, null },
                    { 74, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DBM302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Block 10", 0, null },
                    { 75, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Block 10", 0, null },
                    { 76, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Block 10", 0, null },
                    { 77, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DSR301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Block 10", 0, null },
                    { 78, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DWP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Block 10", 0, null },
                    { 79, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NLP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "Block 10", 0, null },
                    { 80, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRP201c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Block 10", 0, null },
                    { 81, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADY201m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Block 10", 0, null },
                    { 82, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AID301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Block 10", 0, null },
                    { 83, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIE301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Block 10", 0, null },
                    { 84, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIG202c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Block 10", 0, null },
                    { 85, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIH301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Block 10", 0, null },
                    { 86, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIL303m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Block 10", 0, null },
                    { 87, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AIM301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Block 10", 0, null },
                    { 88, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASR301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Block 10", 0, null },
                    { 89, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Block 10", 0, null },
                    { 90, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "BDI302c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Block 10", 0, null },
                    { 91, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CPV301_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Block 10", 0, null },
                    { 92, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAP391m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Block 10", 0, null },
                    { 93, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DAT301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Block 10", 0, null },
                    { 94, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DBM302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Block 10", 0, null },
                    { 95, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Block 10", 0, null },
                    { 96, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DPL302m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "Block 10", 0, null },
                    { 97, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DSR301m_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Block 10", 0, null },
                    { 98, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DWP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Block 10", 0, null },
                    { 99, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NLP301c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "Block 10", 0, null },
                    { 100, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRP201c_Fa24_FE_123456", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60'", 1, "Multiple Choice", null, false, 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Block 10", 0, null }
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

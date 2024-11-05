using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeanId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                    table.ForeignKey(
                        name: "FK_Faculties_Users_DeanId",
                        column: x => x.DeanId,
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
                name: "CampusUserSubject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    IsLecturer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                        name: "FK_CampusUserSubject_Semesters",
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
                    { 1, "Ha Noi", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(512), null, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(534) },
                    { 2, "Da Nang", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(539), null, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(540) },
                    { 3, "Can Tho", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(544), null, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(546) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(549), null, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(550) },
                    { 5, "Quy Nhon", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(554), null, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(555) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1080), "Unassigned", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1084) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1088), "Assigned", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1090) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1093), "Reviewing", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1094) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1097), "Erroneous", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1098) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1101), "Faultless", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1102) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1105), "Completed", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1107) }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "CreateDate", "DeanId", "Description", "FacultyName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1189), null, "Specializes in information technology, software development, and systems engineering.", "IT", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1191) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1195), null, "Focuses on business administration, economics, and financial management.", "BA", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1197) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1200), null, "Offers programs in communication technology and media studies.", "CTT", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1202) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1205), null, "Dedicated to English language studies and cross-cultural communication.", "ENG", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1206) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1209), null, "Specializes in Japanese language, culture, and international relations.", "JPN", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1210) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1215), null, "Focuses on Korean language, culture, and regional studies.", "KOR", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1216) },
                    { 7, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1219), null, "Provides training in Chinese language, culture, and business practices.", "CHN", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1221) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3061), null, "/usermanagement", "User Management", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3064) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3067), null, "/Admin/History", "User Log", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3069) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3072), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3074) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3077), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3078) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3081), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3083) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3085), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3086) },
                    { 7, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3090), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3091) },
                    { 8, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3098), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3100) },
                    { 9, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3120), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3121) },
                    { 10, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3094), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3096) },
                    { 11, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3103), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3104) },
                    { 12, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3107), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3108) },
                    { 13, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3111), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3114) },
                    { 14, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3116), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3118) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3434), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa21", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3435) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3442), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp21", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3443) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3447), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su21", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3449) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3453), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa21", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3454) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3458), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp22", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3460) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3464), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su22", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3465) },
                    { 7, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3469), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa22", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3470) },
                    { 8, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3474), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp23", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3476) },
                    { 9, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3480), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su23", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3481) },
                    { 10, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3485), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa23", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3486) },
                    { 11, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3490), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp24", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3491) },
                    { 12, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3495), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su24", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3496) },
                    { 13, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3500), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa24", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3502) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1295), "Admin", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1297) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1300), "Examiner", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1302) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1305), "Lecturer", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1306) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1309), "Head of Department", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1311) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1315), "Curriculum Development", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1317) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3201), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3203) },
                    { 2, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3207), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3208) },
                    { 8, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3211), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3212) },
                    { 9, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3215), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3216) },
                    { 14, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3219), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3221) },
                    { 3, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3223), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3224) },
                    { 10, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3227), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3229) },
                    { 11, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3243), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3244) },
                    { 13, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3247), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3248) },
                    { 5, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3253), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3254) },
                    { 4, 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3231), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3232) },
                    { 6, 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3234), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3236) },
                    { 7, 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3238), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3239) },
                    { 12, 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3250), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3251) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "FacultyId", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1982), 1, null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1984) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1989), 1, null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1990) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1995), 1, null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1997) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2001), 1, null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2002) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2006), 1, null, "NWC203c", "Computer Networking", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2007) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2011), 2, null, "ENM401", "Business English", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2012) },
                    { 7, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2154), 2, null, "ECO121", "Basic Macro Economics", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2155) },
                    { 8, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2159), 2, null, "ECO201", "International Economics", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2161) },
                    { 9, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2164), 2, null, "ACC101", "Principles of Accounting", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2166) },
                    { 10, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2169), 2, null, "MKT101", "Marketing Principles", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2171) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1509), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1511) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1521), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lienkt@fe.edu.vn", "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1522) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1530), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoanglm@fe.edu.vn", "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1531) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1541), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhnq@fe.edu.vn", "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1542) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1550), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhnh@fe.edu.vn", "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1551) },
                    { 6, "Huế", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1560), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phongtl@fe.edu.vn", "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1561) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1579), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1580) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1588), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1590) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1598), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1599) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1607), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1608) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1616), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1617) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1643), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1645) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1654), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1656) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1664), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1666) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1731), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1732) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1741), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1742) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1750), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1751) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1759), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1761) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1770), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1771) },
                    { 20, "Huế", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1778), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1780) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1788), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1789) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1817), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1818) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1826), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1827) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1835), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1836) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1844), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1845) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1853), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1854) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1625), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1626) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1568), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hunglthe160235@fe.edu.vn", "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1570) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1797), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1798) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1635), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1636) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1806), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(1809) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 29 },
                    { 2, 1, 1, 2, 29 },
                    { 3, 1, 1, 3, 29 },
                    { 4, 1, 1, 4, 31 },
                    { 5, 1, 1, 5, 31 },
                    { 6, 1, 2, 6, 13 },
                    { 7, 1, 2, 7, 13 },
                    { 8, 1, 2, 8, 13 },
                    { 9, 1, 2, 9, 13 },
                    { 10, 1, 2, 10, 13 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "IsLecturer", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 11, 1, true, 3, 1, 7 },
                    { 12, 1, true, 3, 2, 7 },
                    { 13, 1, true, 3, 3, 7 },
                    { 14, 1, true, 3, 4, 7 },
                    { 15, 1, true, 3, 5, 7 },
                    { 16, 1, true, 4, 6, 27 },
                    { 17, 1, true, 4, 7, 27 },
                    { 18, 1, true, 4, 8, 27 },
                    { 19, 1, true, 4, 9, 27 },
                    { 20, 1, true, 4, 10, 27 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 21, 3, 5, 1, 16 },
                    { 22, 3, 5, 2, 16 },
                    { 23, 3, 5, 3, 16 },
                    { 24, 3, 5, 4, 16 },
                    { 25, 3, null, 5, 16 },
                    { 26, 3, 6, 6, 17 },
                    { 27, 3, 6, 7, 17 },
                    { 28, 3, 6, 8, 17 },
                    { 29, 3, 6, 9, 17 },
                    { 30, 3, 6, 10, 17 },
                    { 31, 4, 7, 1, 18 },
                    { 32, 4, 7, 2, 18 },
                    { 33, 4, 7, 3, 18 },
                    { 34, 4, 7, 4, 18 },
                    { 35, 4, 7, 5, 18 },
                    { 36, 4, 8, 6, 19 },
                    { 37, 4, 8, 7, 19 },
                    { 38, 4, 8, 8, 19 },
                    { 39, 4, 8, 9, 19 },
                    { 40, 4, 8, 10, 19 },
                    { 41, 5, 9, 1, 20 },
                    { 42, 5, 9, 2, 20 },
                    { 43, 5, 9, 3, 20 },
                    { 44, 5, 9, 4, 20 },
                    { 45, 5, 9, 5, 20 },
                    { 46, 5, 10, 6, 21 },
                    { 47, 5, 10, 7, 21 },
                    { 48, 5, 10, 8, 21 },
                    { 49, 5, 10, 9, 21 },
                    { 50, 5, 10, 10, 21 },
                    { 51, 2, 1, 1, 14 },
                    { 52, 2, 1, 2, 14 },
                    { 53, 2, 1, 3, 14 },
                    { 54, 2, 1, 4, 14 },
                    { 55, 2, 1, 5, 14 },
                    { 56, 2, 2, 6, 15 },
                    { 57, 2, 2, 7, 15 },
                    { 58, 2, 2, 8, 15 },
                    { 59, 2, 2, 9, 15 },
                    { 60, 2, 2, 10, 15 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "AssignedUserId", "AssignmentDate", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDate", "ExamDuration", "ExamStatusId", "ExamType", "GeneralFeedback", "IsReady", "SemesterId", "StartDate", "SubjectId", "TermDuration", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2633), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2636), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2632), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2627), "PRN211_Q1_10_123456", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 5, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2630), 1, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2637) },
                    { 2, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2650), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2652), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2648), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2645), "PRN211_Q2_5_654321", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 5, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2647), 1, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2654) },
                    { 3, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2666), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2668), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2664), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2661), "PRN221_Q1_10_789012", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2662), 2, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2669) },
                    { 4, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2679), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2678), null, "PRN221_Q2_5_210987", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2675), 2, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2680) },
                    { 5, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2692), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2690), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2689), null, "PRN231_Q1_10_345678", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2687), 3, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2694) },
                    { 6, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2703), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2705), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2701), null, "PRN231_Q2_5_876543", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2700), 3, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2707) },
                    { 7, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2718), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2720), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2716), null, "MAE101_Q1_10_234567", new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2715), 4, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2721) },
                    { 8, 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2731), 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2733), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2729), null, "MAE101_Q2_5_765432", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", "This exam covers the material from Block 10.", true, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2728), 4, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2734) },
                    { 9, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2743), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2742), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2741), 5, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2745) },
                    { 10, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2754), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2752), null, "NWC203c_Q2_5_876543", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2751), 5, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2755) },
                    { 11, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2766), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2764), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2761), "ENM401_Q1_10_111222", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2763), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2767) },
                    { 12, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2778), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2777), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2773), "ENM401_Q2_5_222111", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Reading", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2775), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2780) },
                    { 13, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2790), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2789), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2786), "ENM401_Q3_7_222333", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Writing", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2788), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2792) },
                    { 14, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2803), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2800), new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2798), "ENM401_Q4_9_333111", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Listening", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2799), 6, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2804) },
                    { 15, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2812), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2811), null, "ECO121_Q1_10_333444", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2810), 7, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2813) },
                    { 16, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2821), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2820), null, "ECO121_Q2_5_444333", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2819), 7, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2823) },
                    { 17, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2831), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2830), null, "ECO201_Q1_10_555666", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2829), 8, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2833) },
                    { 18, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2842), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2840), null, "ECO201_Q2_5_666555", new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2839), 8, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2843) },
                    { 19, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2852), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2851), null, "ACC101_Q1_10_777888", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2849), 9, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2853) },
                    { 20, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2862), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2861), null, "ACC101_Q2_5_888777", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2859), 9, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2864) },
                    { 21, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2873), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2872), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2870), 10, "Block 10 (10 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2875) },
                    { 22, null, null, 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2884), 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2882), null, "MKT101_Q2_5_000999", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2881), 10, "Block 5 (5 weeks)", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(2885) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3332), 1, 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3334) },
                    { 2, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3338), 2, 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3340) },
                    { 3, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3345), 3, 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3346) },
                    { 4, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3351), 4, 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3352) },
                    { 5, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3356), 5, 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3358) },
                    { 6, new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3361), 6, 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", new DateTime(2024, 11, 5, 16, 47, 17, 34, DateTimeKind.Local).AddTicks(3363) }
                });

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
                name: "IX_Faculties_DeanId",
                table: "Faculties",
                column: "DeanId");

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
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

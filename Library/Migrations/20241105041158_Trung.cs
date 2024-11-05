using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class Trung : Migration
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
                name: "Faculty",
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
                    table.PrimaryKey("PK_Faculty", x => x.FacultyId);
                    table.ForeignKey(
                        name: "FK_Faculty_Users_DeanId",
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
                    SubjectCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "FacultyId");
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
                name: "InstructorAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignStatusId = table.Column<int>(type: "int", nullable: true),
                    GeneralFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
                    ExamTestDuration = table.Column<TimeSpan>(type: "time", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_InstructorAssignments_ExamStatuses_AssignStatusId",
                        column: x => x.AssignStatusId,
                        principalTable: "ExamStatuses",
                        principalColumn: "ExamStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorAssignments_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId");
                    table.ForeignKey(
                        name: "FK_InstructorAssignments_Users_AssignedTo",
                        column: x => x.AssignedUserId,
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
                name: "ReportFile",
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
                    table.PrimaryKey("PK_ReportFile", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_ReportFile_Reports_ReportId",
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
                    { 1, "Ha Noi", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5556), null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5568) },
                    { 2, "Da Nang", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5569), null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5570) },
                    { 3, "Can Tho", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5594), null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5595) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5596), null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5597) },
                    { 5, "Quy Nhon", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5598), null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5599) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5748), "Unassigned", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5749) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5751), "Assigned", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5751) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5753), "Reviewing", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5753) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5754), "Erroneous", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5755) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5756), "Faultless", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5757) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5758), "Completed", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5758) }
                });

            migrationBuilder.InsertData(
                table: "Faculty",
                columns: new[] { "FacultyId", "CreateDate", "DeanId", "Description", "FacultyName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5788), null, "Specializes in information technology, software development, and systems engineering.", "IT", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5789) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5791), null, "Focuses on business administration, economics, and financial management.", "BA", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5791) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5793), null, "Offers programs in communication technology and media studies.", "CTT", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5793) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5794), null, "Dedicated to English language studies and cross-cultural communication.", "ENG", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5795) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5796), null, "Specializes in Japanese language, culture, and international relations.", "JPN", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5797) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5798), null, "Focuses on Korean language, culture, and regional studies.", "KOR", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5799) },
                    { 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5800), null, "Provides training in Chinese language, culture, and business practices.", "CHN", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5800) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6558), null, "/usermanagement", "User Management", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6559) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6561), null, "/Admin/History", "User Log", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6561) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6563), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6563) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6564), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6565) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6567), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6568) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6569), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6569) },
                    { 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6570), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6571) },
                    { 8, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6573), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6574) },
                    { 9, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6581), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6582) },
                    { 10, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6572), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6572) },
                    { 11, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6575), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6575) },
                    { 12, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6576), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6577) },
                    { 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6578), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6579) },
                    { 14, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6580), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6580) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6717), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6718) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6720), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6720) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6722), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6722) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6724), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6724) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6726), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6726) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6728), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6728) },
                    { 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6730), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6730) },
                    { 8, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6732), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6732) },
                    { 9, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6733), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6734) },
                    { 10, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6735), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6736) },
                    { 11, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6737), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6738) },
                    { 12, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6739), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6740) },
                    { 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6741), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6742) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "FacultyId", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6092), null, null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6093) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6094), null, null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6095) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6096), null, null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6097) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6098), null, null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6098) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6100), null, null, "NWC203c", "Computer Networking", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6101) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6102), null, null, "ENM401", "Business English", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6103) },
                    { 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6104), null, null, "ECO121", "Basic Macro Economics", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6104) },
                    { 8, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6106), null, null, "ECO201", "International Economics", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6107) },
                    { 9, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6108), null, null, "ACC101", "Principles of Accounting", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6108) },
                    { 10, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6109), null, null, "MKT101", "Marketing Principles", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6110) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5827), "Admin", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5828) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5829), "Examiner", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5830) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5831), "Lecturer", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5831) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5833), "Head of Department", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5833) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5834), "Curriculum Development", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5835) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6609), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6610) },
                    { 2, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6611), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6612) },
                    { 8, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6613), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6614) },
                    { 9, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6614), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6615) },
                    { 14, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6616), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6616) },
                    { 3, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6617), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6618) },
                    { 10, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6619), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6619) },
                    { 11, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6624), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6625) },
                    { 13, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6626), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6626) },
                    { 5, 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6628), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6629) },
                    { 4, 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6620), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6621) },
                    { 6, 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6622), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6622) },
                    { 7, 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6623), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6623) },
                    { 12, 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6627), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6627) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5867), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5868) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5872), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5872) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5876), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5877) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5881), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5881) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5887), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5888) },
                    { 6, "Huế", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5891), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5892) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5916), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5916) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5919), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5920) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5923), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5924) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5927), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5928) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5933), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5934) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5947), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5948) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5952), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5953) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5956), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5956) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5960), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5960) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5964), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5965) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5969), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5970) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5973), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5974) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6011), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6011) },
                    { 20, "Huế", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6015), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6016) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6019), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6019) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6031), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6031) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6035), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6035) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6038), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6039) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6042), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6043) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6047), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6047) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5937), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5939) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5911), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5912) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6024), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6024) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5943), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(5943) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6027), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6028) }
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
                    { 1, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6308), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6307), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6305), "PRN211_Q1_10_123456", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 5, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6306), 1, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6309) },
                    { 2, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6319), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6319), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6314), "PRN211_Q2_5_654321", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 5, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6318), 1, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6321) },
                    { 3, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6326), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6325), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6324), "PRN221_Q1_10_789012", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6324), 2, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6326) },
                    { 4, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6349), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6349), null, "PRN221_Q2_5_210987", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6348), 2, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6350) },
                    { 5, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6355), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6355), null, "PRN231_Q1_10_345678", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6354), 3, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6356) },
                    { 6, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6361), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6361), null, "PRN231_Q2_5_876543", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6360), 3, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6362) },
                    { 7, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6366), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6365), null, "MAE101_Q1_10_234567", new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6365), 4, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6366) },
                    { 8, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6370), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6369), null, "MAE101_Q2_5_765432", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6369), 4, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6370) },
                    { 9, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6374), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6374), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6373), 5, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6375) },
                    { 10, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6378), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6378), null, "NWC203c_Q2_5_876543", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6377), 5, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6379) },
                    { 11, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6384), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6383), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6382), "ENM401_Q1_10_111222", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6383), 6, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6384) },
                    { 12, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6388), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6388), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6387), "ENM401_Q2_5_222111", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Reading", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6387), 6, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6389) },
                    { 13, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6395), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6392), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6391), "ENM401_Q3_7_222333", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Writing", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6392), 6, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6395) },
                    { 14, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6399), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6399), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6397), "ENM401_Q4_9_333111", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Listening", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6398), 6, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6400) },
                    { 15, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6404), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6403), null, "ECO121_Q1_10_333444", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6403), 7, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6404) },
                    { 16, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6408), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6408), null, "ECO121_Q2_5_444333", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6407), 7, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6409) },
                    { 17, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6412), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6412), null, "ECO201_Q1_10_555666", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6411), 8, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6412) },
                    { 18, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6416), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6416), null, "ECO201_Q2_5_666555", new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6415), 8, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6417) },
                    { 19, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6421), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6420), null, "ACC101_Q1_10_777888", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6419), 9, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6422) },
                    { 20, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6426), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6425), null, "ACC101_Q2_5_888777", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6424), 9, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6426) },
                    { 21, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6430), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6430), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6429), 10, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6431) },
                    { 22, null, null, 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6434), 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6434), null, "MKT101_Q2_5_000999", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", null, null, 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6433), 10, null, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6435) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "ExamTestDuration", "GeneralFeedback", "IsReady", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6494), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6496), 1, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6497) },
                    { 2, 3, 12, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6499), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6500), 2, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6500) },
                    { 3, 3, 12, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6501), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6502), 3, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6502) },
                    { 4, 3, 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6503), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6504), 11, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6505) },
                    { 5, 3, 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6506), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6507), 12, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6507) },
                    { 6, 3, 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6508), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6509), 13, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6510) },
                    { 7, 3, 13, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6510), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6511), 14, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6512) },
                    { 8, 4, 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6513), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6513), 1, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6514) },
                    { 9, 4, 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6515), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6516), 2, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6516) },
                    { 10, 4, 7, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6518), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6519), 3, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6519) },
                    { 11, 4, 27, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6520), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6521), 11, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6522) },
                    { 12, 4, 27, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6522), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6523), 12, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6524) },
                    { 13, 4, 27, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6525), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6526), 13, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6526) },
                    { 14, 4, 27, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6527), new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6528), 14, null, null, false, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6528) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6658), 1, 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6659) },
                    { 2, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6662), 2, 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6663) },
                    { 3, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6664), 3, 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6665) },
                    { 4, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6666), 4, 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6667) },
                    { 5, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6668), 5, 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6669) },
                    { 6, new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6670), 6, 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", new DateTime(2024, 11, 5, 11, 11, 57, 714, DateTimeKind.Local).AddTicks(6671) }
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
                table: "Faculty",
                column: "DeanId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAssignments_AssignedTo",
                table: "InstructorAssignments",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAssignments_AssignStatusId",
                table: "InstructorAssignments",
                column: "AssignStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAssignments_ExamId",
                table: "InstructorAssignments",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_MenuId",
                table: "MenuRoles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_ReportId",
                table: "ReportFile",
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
                name: "InstructorAssignments");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "ReportFile");

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
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

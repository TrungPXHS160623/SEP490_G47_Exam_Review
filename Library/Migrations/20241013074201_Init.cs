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
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
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
                name: "CampusUserSubject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
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
                    ExamType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    CreaterId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    ExamStatusId = table.Column<int>(type: "int", nullable: true),
                    EstimatedTimeTest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "InstructorAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignStatusId = table.Column<int>(type: "int", nullable: true),
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
                    AssignmentId = table.Column<int>(type: "int", nullable: true),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionSolutionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_InstructorAssignments",
                        column: x => x.AssignmentId,
                        principalTable: "InstructorAssignments",
                        principalColumn: "AssignmentId");
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(766), null, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(785) },
                    { 2, "Da Nang", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(789), null, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(790) },
                    { 3, "Can Tho", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(792), null, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(794) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(796), null, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(797) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(800), null, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(801) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1118), "Not Assign", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1120) },
                    { 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1123), "Waiting To Assign", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1124) },
                    { 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1201), "Assigned", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1202) },
                    { 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1205), "Reviewing", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1206) },
                    { 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1208), "Exam With Errors", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1209) },
                    { 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1212), "Faultless Exam", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1213) },
                    { 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1215), "Complete", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1216) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2438), null, "/usermanagement", "User Management", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2439) },
                    { 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2442), null, "/Admin/History", "User Log", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2443) },
                    { 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2445), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2446) },
                    { 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2448), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2449) },
                    { 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2451), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2452) },
                    { 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2455), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2456) },
                    { 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2458), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2459) },
                    { 8, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2464), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2465) },
                    { 9, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2476), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2477) },
                    { 10, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2461), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2462) },
                    { 11, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2467), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2468) },
                    { 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2470), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2471) },
                    { 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2473), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2474) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1556), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1557) },
                    { 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1560), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1561) },
                    { 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1564), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1565) },
                    { 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1567), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1568) },
                    { 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1630), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1632) },
                    { 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1634), null, "ENM401", "Business English", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1635) },
                    { 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1638), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1639) },
                    { 8, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1641), null, "ECO201", "International Economics", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1642) },
                    { 9, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1644), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1645) },
                    { 10, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1647), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1648) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1267), "Admin", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1268) },
                    { 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1272), "Examiner", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1273) },
                    { 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1276), "Lecturer", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1277) },
                    { 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1279), "Head of Department", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1280) },
                    { 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1283), "Curriculum Development", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1284) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2524), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2525) },
                    { 2, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2528), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2529) },
                    { 8, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2532), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2533) },
                    { 9, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2534), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2535) },
                    { 3, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2537), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2538) },
                    { 10, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2541), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2542) },
                    { 11, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2553), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2554) },
                    { 13, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2556), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2556) },
                    { 5, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2561), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2562) },
                    { 4, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2544), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2545) },
                    { 6, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2547), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2548) },
                    { 7, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2549), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2551) },
                    { 12, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2558), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2559) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1328), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1330) },
                    { 2, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1334), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1335) },
                    { 3, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1339), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1340) },
                    { 4, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1344), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1345) },
                    { 5, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1349), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1351) },
                    { 6, 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1355), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1356) },
                    { 7, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1363), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1365) },
                    { 8, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1370), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1371) },
                    { 9, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1374), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1375) },
                    { 10, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1378), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1379) },
                    { 11, 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1383), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1384) },
                    { 12, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1397), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1399) },
                    { 13, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1404), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1405) },
                    { 14, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1408), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1410) },
                    { 15, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1414), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1415) },
                    { 16, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1420), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1422) },
                    { 17, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1427), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1428) },
                    { 18, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1431), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1432) },
                    { 19, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1436), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1437) },
                    { 20, 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1441), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1442) },
                    { 21, 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1446), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1447) },
                    { 22, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1456), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1457) },
                    { 23, 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1460), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1461) },
                    { 24, 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1464), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1465) },
                    { 25, 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1469), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1469) },
                    { 26, 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1473), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1474) },
                    { 27, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1389), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1390) },
                    { 28, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1477), true, "hunglthe160235@fpt.edu.vn", 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1478) },
                    { 29, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1481), true, "tuanlmhe161245@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1482) },
                    { 30, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1485), true, "trungpxhs160623@fpt.edu.vn", 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1486) },
                    { 31, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1489), true, "tungtkHS163077@fpt.edu.vn", 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(1490) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 29 },
                    { 2, 1, 2, 29 },
                    { 3, 1, 3, 29 },
                    { 4, 1, 4, 31 },
                    { 5, 1, 5, 31 },
                    { 6, 1, 6, 13 },
                    { 7, 1, 7, 13 },
                    { 8, 1, 8, 13 },
                    { 9, 1, 9, 13 },
                    { 10, 1, 10, 13 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "IsLecturer", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 11, 1, true, 1, 7 },
                    { 12, 1, true, 2, 7 },
                    { 13, 1, true, 3, 7 },
                    { 14, 1, true, 4, 7 },
                    { 15, 1, true, 5, 7 },
                    { 16, 1, true, 6, 27 },
                    { 17, 1, true, 7, 27 },
                    { 18, 1, true, 8, 27 },
                    { 19, 1, true, 9, 27 },
                    { 20, 1, true, 10, 27 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 21, 3, 1, 16 },
                    { 22, 3, 2, 16 },
                    { 23, 3, 3, 16 },
                    { 24, 3, 4, 16 },
                    { 25, 3, 5, 16 },
                    { 26, 3, 6, 17 },
                    { 27, 3, 7, 17 },
                    { 28, 3, 8, 17 },
                    { 29, 3, 9, 17 },
                    { 30, 3, 10, 17 },
                    { 31, 4, 1, 18 },
                    { 32, 4, 2, 18 },
                    { 33, 4, 3, 18 },
                    { 34, 4, 4, 18 },
                    { 35, 4, 5, 18 },
                    { 36, 4, 6, 19 },
                    { 37, 4, 7, 19 },
                    { 38, 4, 8, 19 },
                    { 39, 4, 9, 19 },
                    { 40, 4, 10, 19 },
                    { 41, 5, 1, 20 },
                    { 42, 5, 2, 20 },
                    { 43, 5, 3, 20 },
                    { 44, 5, 4, 20 },
                    { 45, 5, 5, 20 },
                    { 46, 5, 6, 21 },
                    { 47, 5, 7, 21 },
                    { 48, 5, 8, 21 },
                    { 49, 5, 9, 21 },
                    { 50, 5, 10, 21 },
                    { 51, 2, 1, 14 },
                    { 52, 2, 2, 14 },
                    { 53, 2, 3, 14 },
                    { 54, 2, 4, 14 },
                    { 55, 2, 5, 14 },
                    { 56, 2, 6, 15 },
                    { 57, 2, 7, 15 },
                    { 58, 2, 8, 15 },
                    { 59, 2, 9, 15 },
                    { 60, 2, 10, 15 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2013), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2012), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2008), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2011), 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2015) },
                    { 2, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2023), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2022), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2020), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2021), 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2024) },
                    { 3, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2033), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2030), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2028), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2029), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2034) },
                    { 4, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2039), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2038), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2037), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2040) },
                    { 5, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2047), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2046), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2045), 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2048) },
                    { 6, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2055), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2053), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2052), 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2056) },
                    { 7, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2061), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2060), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2059), 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2062) },
                    { 8, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2068), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2067), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2066), 4, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2069) },
                    { 9, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2077), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2075), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2074), 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2078) },
                    { 10, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2083), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2082), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2081), 5, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2084) },
                    { 11, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2090), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2090), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2088), "ENM401_Q1_10_111222", "Block 10 (10 weeks)", 7, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2089), 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2091) },
                    { 12, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2098), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2097), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2095), "ENM401_Q2_5_222111", "Block 10 (10 weeks)", 7, "Reading", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2096), 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2098) },
                    { 13, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2106), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2105), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2103), "ENM401_Q3_7_222333", "Block 10 (10 weeks)", 7, "Writing", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2104), 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2107) },
                    { 14, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2113), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2111), "ENM401_Q4_9_333111", "Block 10 (10 weeks)", 7, "Listening", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2112), 6, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2115) },
                    { 15, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2121), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2120), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2119), 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2122) },
                    { 16, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2128), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2127), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2126), 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2129) },
                    { 17, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2136), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2135), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2134), 8, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2137) },
                    { 18, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2143), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2142), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2141), 8, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2144) },
                    { 19, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2149), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2148), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2147), 9, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2150) },
                    { 20, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2156), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2155), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2154), 9, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2157) },
                    { 21, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2245), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2244), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2243), 10, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2246) },
                    { 22, 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2252), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2251), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2250), 10, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2253) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2318), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2322), 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2324) },
                    { 2, 3, 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2328), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2330), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2331) },
                    { 3, 3, 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2333), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2334), 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2335) },
                    { 4, 3, 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2337), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2339), 11, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2340) },
                    { 5, 3, 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2342), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2343), 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2344) },
                    { 6, 3, 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2346), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2347), 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2348) },
                    { 7, 3, 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2350), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2351), 14, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2352) },
                    { 8, 4, 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2354), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2356), 1, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2356) },
                    { 9, 4, 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2359), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2360), 2, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2361) },
                    { 10, 4, 7, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2364), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2365), 3, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2366) },
                    { 11, 4, 27, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2368), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2370), 11, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2371) },
                    { 12, 4, 27, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2373), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2374), 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2375) },
                    { 13, 4, 27, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2377), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2378), 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2379) },
                    { 14, 4, 27, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2381), new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2383), 14, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2384) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2613), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2614) },
                    { 2, 9, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2618), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2619) },
                    { 3, 11, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2622), 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2623) },
                    { 4, 12, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2626), 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2628) },
                    { 5, 13, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2690), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2691) },
                    { 6, 14, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2695), 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 13, 14, 41, 59, 555, DateTimeKind.Local).AddTicks(2696) }
                });

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
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

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
                name: "IX_Reports_AssignmentId",
                table: "Reports",
                column: "AssignmentId");

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
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "InstructorAssignments");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "ExamStatuses");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

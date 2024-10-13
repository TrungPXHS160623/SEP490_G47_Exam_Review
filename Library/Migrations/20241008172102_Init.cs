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
                    AssignemtId = table.Column<int>(type: "int", nullable: true),
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
                        column: x => x.AssignemtId,
                        principalTable: "InstructorAssignments",
                        principalColumn: "AssignmentId");
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(524), null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(546) },
                    { 2, "Da Nang", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(550), null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(551) },
                    { 3, "Can Tho", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(554), null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(555) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(558), null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(559) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(562), null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(563) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(850), "Not Assign", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(852) },
                    { 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(855), "Waiting to Assign", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(856) },
                    { 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(859), "Assigned", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(860) },
                    { 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(932), "Reviewing", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(934) },
                    { 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(936), "Finish Review", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(937) },
                    { 6, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(941), "Complete", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(942) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2003), null, "/usermanagement", "User Management", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2004) },
                    { 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2007), null, "/Admin/History", "User Log", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2008) },
                    { 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2010), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2011) },
                    { 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2013), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2014) },
                    { 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2016), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2017) },
                    { 6, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2019), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2020) },
                    { 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2022), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2023) },
                    { 8, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2028), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2029) },
                    { 9, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2037), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2038) },
                    { 10, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2025), null, "/Examiner/usermanagement", "View Report", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2026) },
                    { 11, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2032), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2034) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2158), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2159) },
                    { 2, null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2163), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2164) },
                    { 3, null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2166), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2167) },
                    { 4, null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2169), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2170) },
                    { 5, null, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2173), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2174) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1310), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1311) },
                    { 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1314), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1315) },
                    { 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1318), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1319) },
                    { 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1321), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1322) },
                    { 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1324), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1325) },
                    { 6, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1327), null, "ECO111", "Microeconomics", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1328) },
                    { 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1331), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1332) },
                    { 8, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1334), null, "ECO201", "International Economics", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1335) },
                    { 9, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1337), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1338) },
                    { 10, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1340), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1341) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(992), "Admin", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(997) },
                    { 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1000), "Examiner", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1001) },
                    { 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1004), "Lecturer", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1005) },
                    { 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1008), "Head of Department", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1010) },
                    { 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1012), "Curriculum Development", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1013) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2085), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2087) },
                    { 2, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2089), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2090) },
                    { 8, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2092), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2093) },
                    { 9, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2096), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2096) },
                    { 3, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2098), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2099) },
                    { 10, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2101), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2102) },
                    { 11, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2111), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2112) },
                    { 5, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2114), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2115) },
                    { 4, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2103), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2104) },
                    { 6, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2106), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2107) },
                    { 7, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2109), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(2110) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1060), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1062) },
                    { 2, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1066), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1068) },
                    { 3, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1073), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1074) },
                    { 4, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1077), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1078) },
                    { 5, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1081), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1083) },
                    { 6, 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1087), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1087) },
                    { 7, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1091), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1093) },
                    { 8, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1096), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1097) },
                    { 9, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1101), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1102) },
                    { 10, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1105), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1106) },
                    { 11, 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1110), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1111) },
                    { 12, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1120), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1121) },
                    { 13, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1125), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1126) },
                    { 14, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1129), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1130) },
                    { 15, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1134), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1135) },
                    { 16, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1138), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1138) },
                    { 17, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1142), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1143) },
                    { 18, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1146), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1147) },
                    { 19, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1151), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1152) },
                    { 20, 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1155), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1156) },
                    { 21, 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1159), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1160) },
                    { 22, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1165), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1166) },
                    { 23, 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1222), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1223) },
                    { 24, 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1228), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1231) },
                    { 25, 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1235), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1236) },
                    { 26, 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1239), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1240) },
                    { 27, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1115), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1116) },
                    { 28, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1243), true, "hunglthe160235@fpt.edu.vn", 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1244) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 12 },
                    { 2, 1, 2, 12 },
                    { 3, 1, 3, 12 },
                    { 4, 1, 4, 12 },
                    { 5, 1, 5, 12 },
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
                    { 1, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1618), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1617), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1613), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1616), 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1619) },
                    { 2, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1627), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1626), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1624), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1625), 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1628) },
                    { 3, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1634), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1633), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1631), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1632), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1635) },
                    { 4, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1641), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1640), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1638), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1641) },
                    { 5, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1648), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1647), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1646), 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1649) },
                    { 6, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1654), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1653), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1653), 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1655) },
                    { 7, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1661), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1660), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1659), 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1663) },
                    { 8, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1668), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1667), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1666), 4, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1669) },
                    { 9, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1674), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1673), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1673), 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1675) },
                    { 10, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1682), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1681), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1679), 5, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1682) },
                    { 11, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1689), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1688), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1686), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1687), 6, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1690) },
                    { 12, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1695), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1695), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1693), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1694), 6, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1696) },
                    { 13, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1701), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1701), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1700), 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1702) },
                    { 14, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1708), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1707), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1706), 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1709) },
                    { 15, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1715), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1714), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1713), 8, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1716) },
                    { 16, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1721), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1720), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1719), 8, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1722) },
                    { 17, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1728), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1727), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1726), 9, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1728) },
                    { 18, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1734), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1733), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1732), 9, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1735) },
                    { 19, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1740), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1739), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1738), 10, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1741) },
                    { 20, 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1746), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1745), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1744), 10, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1748) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1808), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1810), 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1811) },
                    { 2, 3, 12, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1814), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1816), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1817) },
                    { 3, 3, 12, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1918), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1919), 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1920) },
                    { 4, 3, 13, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1923), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1924), 11, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1925) },
                    { 5, 3, 13, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1927), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1928), 12, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1929) },
                    { 6, 3, 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1931), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1933), 1, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1933) },
                    { 7, 3, 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1936), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1937), 2, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1938) },
                    { 8, 3, 7, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1940), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1941), 3, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1942) },
                    { 9, 3, 27, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1946), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1947), 11, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1948) },
                    { 10, 3, 27, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1951), new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1953), 12, new DateTime(2024, 10, 9, 0, 21, 1, 3, DateTimeKind.Local).AddTicks(1954) }
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
                name: "IX_Reports_AssignemtId",
                table: "Reports",
                column: "AssignemtId");

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

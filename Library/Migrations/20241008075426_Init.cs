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
                columns: new[] { "CampusId", "CampusName", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9058), new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9079) },
                    { 2, "Da Nang", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9089), new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9092) },
                    { 3, "Can Tho", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9100), new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9104) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9109), new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9114) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9117), new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9118) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9880), "Not Assign", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9893) },
                    { 2, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9897), "Waiting to Assign", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9898) },
                    { 3, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9902), "Assigned", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9903) },
                    { 4, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9906), "Reviewing", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9907) },
                    { 5, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9910), "Finish Review", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9911) },
                    { 6, new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9914), "Complete", new DateTime(2024, 10, 8, 14, 54, 23, 514, DateTimeKind.Local).AddTicks(9916) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1216), null, "/usermanagement", "User Management", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1218) },
                    { 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1234), null, "/Admin/History", "History", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1236) },
                    { 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1240), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1241) },
                    { 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1243), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1244) },
                    { 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1247), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1248) },
                    { 6, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1250), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1251) },
                    { 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1254), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1255) },
                    { 8, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1258), null, "/Examiner/usermanagement", "View Report", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1259) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1413), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1414) },
                    { 2, null, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1419), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1420) },
                    { 3, null, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1423), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1424) },
                    { 4, null, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1428), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1429) },
                    { 5, null, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1432), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1433) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(394), "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(397) },
                    { 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(401), "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(403) },
                    { 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(407), "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(408) },
                    { 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(411), "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(413) },
                    { 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(415), "NWC203c", "Computer Networking", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(417) },
                    { 6, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(420), "ECO111", "Microeconomics", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(422) },
                    { 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(425), "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(426) },
                    { 8, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(429), "ECO201", "International Economics", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(431) },
                    { 9, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(434), "ACC101", "Principles of Accounting", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(436) },
                    { 10, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(439), "MKT101", "Marketing Principles", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(441) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(5), "Admin", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(7) },
                    { 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(12), "Examiner", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(13) },
                    { 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(18), "Lecturer", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(19) },
                    { 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(45), "Head of Department", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(47) },
                    { 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(51), "Curriculum Development", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(52) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1322), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1324) },
                    { 2, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1327), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1328) },
                    { 3, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1330), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1331) },
                    { 8, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1333), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1334) },
                    { 5, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1346), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1348) },
                    { 4, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1336), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1338) },
                    { 6, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1340), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1341) },
                    { 7, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1343), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1344) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(129), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(135) },
                    { 2, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(141), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(142) },
                    { 3, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(147), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(148) },
                    { 4, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(152), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(153) },
                    { 5, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(157), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(158) },
                    { 6, 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(161), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(162) },
                    { 7, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(167), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(168) },
                    { 8, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(173), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(175) },
                    { 9, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(178), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(180) },
                    { 10, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(185), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(186) },
                    { 11, 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(190), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(191) },
                    { 12, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(201), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(202) },
                    { 13, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(206), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(207) },
                    { 14, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(211), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(213) },
                    { 15, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(217), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(218) },
                    { 16, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(224), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(225) },
                    { 17, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(229), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(231) },
                    { 18, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(235), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(236) },
                    { 19, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(240), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(242) },
                    { 20, 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(246), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(247) },
                    { 21, 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(252), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(253) },
                    { 22, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(257), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(258) },
                    { 23, 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(262), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(263) },
                    { 24, 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(268), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(269) },
                    { 25, 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(273), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(275) },
                    { 26, 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(280), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(282) },
                    { 27, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(195), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(197) }
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
                    { 1, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(800), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(798), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(792), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(796), 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(802) },
                    { 2, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(811), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(810), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(806), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(808), 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(813) },
                    { 3, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(821), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(820), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(817), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(818), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(823) },
                    { 4, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(830), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(829), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(827), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(831) },
                    { 5, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(838), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(837), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(835), 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(839) },
                    { 6, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(846), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(845), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(844), 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(848) },
                    { 7, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(856), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(853), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(852), 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(857) },
                    { 8, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(864), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(862), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(861), 4, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(865) },
                    { 9, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(871), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(870), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(869), 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(873) },
                    { 10, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(879), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(878), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(877), 5, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(880) },
                    { 11, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(888), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(887), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(885), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(886), 6, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(890) },
                    { 12, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(897), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(896), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(894), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(895), 6, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(899) },
                    { 13, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(906), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(905), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(904), 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(907) },
                    { 14, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(913), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(912), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(911), 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(915) },
                    { 15, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(922), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(921), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(919), 8, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(923) },
                    { 16, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(930), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(929), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(928), 8, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(931) },
                    { 17, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(938), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(937), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(935), 9, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(939) },
                    { 18, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(946), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(945), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(944), 9, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(947) },
                    { 19, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(955), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(954), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(951), 10, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(956) },
                    { 20, 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(964), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(962), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(961), 10, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(965) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1082), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1085), 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1087) },
                    { 2, 3, 12, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1091), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1093), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1094) },
                    { 3, 3, 12, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1097), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1098), 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1099) },
                    { 4, 3, 13, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1103), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1105), 11, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1106) },
                    { 5, 3, 13, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1108), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1110), 12, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1112) },
                    { 6, 3, 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1114), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1116), 1, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1117) },
                    { 7, 3, 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1120), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1122), 2, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1123) },
                    { 8, 3, 7, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1126), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1127), 3, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1128) },
                    { 9, 3, 27, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1131), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1132), 11, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1133) },
                    { 10, 3, 27, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1136), new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1138), 12, new DateTime(2024, 10, 8, 14, 54, 23, 515, DateTimeKind.Local).AddTicks(1139) }
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

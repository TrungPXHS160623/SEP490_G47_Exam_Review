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
                    IsLecturer = table.Column<bool>(type: "bit", nullable: true)
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
                    { 1, "Ha Noi", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7546), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7564) },
                    { 2, "Da Nang", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7567), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7568) },
                    { 3, "Can Tho", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7570), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7571) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7572), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7573) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7575), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7575) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7736), "Not Assign", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7738) },
                    { 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7740), "Waiting to Assign", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7741) },
                    { 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7742), "Assigned", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7743) },
                    { 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7745), "Reviewing", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7745) },
                    { 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7746), "Finish Review", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7747) },
                    { 6, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7748), "Complete", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7749) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8423), null, "/usermanagement", "User Management", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8424) },
                    { 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8426), null, "/Admin/History", "History", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8426) },
                    { 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8428), null, "/TestDepartment/ExamList", "Exam List", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8428) },
                    { 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8430), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8430) },
                    { 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8432), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8432) },
                    { 6, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8433), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8434) },
                    { 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8435), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8436) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8546), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8547) },
                    { 2, null, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8549), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8549) },
                    { 3, null, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8551), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8552) },
                    { 4, null, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8553), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8554) },
                    { 5, null, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8555), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8556) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7985), "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7986) },
                    { 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7988), "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7989) },
                    { 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7990), "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7991) },
                    { 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7992), "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7993) },
                    { 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7994), "NWC203c", "Computer Networking", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7995) },
                    { 6, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7997), "ECO111", "Microeconomics", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7999) },
                    { 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8023), "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8024) },
                    { 8, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8025), "ECO201", "International Economics", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8026) },
                    { 9, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8027), "ACC101", "Principles of Accounting", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8028) },
                    { 10, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8029), "MKT101", "Marketing Principles", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8030) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7777), "Admin", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7778) },
                    { 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7780), "Examiner", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7781) },
                    { 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7782), "Lecturer", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7783) },
                    { 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7784), "Head of Department", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7785) },
                    { 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7786), "Program Developer", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7787) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8507), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8508) },
                    { 2, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8510), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8511) },
                    { 3, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8512), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8512) },
                    { 5, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8518), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8519) },
                    { 4, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8513), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8514) },
                    { 6, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8515), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8516) },
                    { 7, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8516), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8517) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7816), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7816) },
                    { 2, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7820), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7820) },
                    { 3, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7869), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7870) },
                    { 4, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7871), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7872) },
                    { 5, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7874), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7875) },
                    { 6, 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7878), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7878) },
                    { 7, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7880), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7881) },
                    { 8, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7883), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7884) },
                    { 9, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7886), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7887) },
                    { 10, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7889), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7891) },
                    { 11, 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7894), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7894) },
                    { 12, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7902), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7906) },
                    { 13, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7908), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7909) },
                    { 14, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7911), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7911) },
                    { 15, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7913), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7914) },
                    { 16, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7916), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7916) },
                    { 17, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7918), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7919) },
                    { 18, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7921), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7921) },
                    { 19, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7923), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7924) },
                    { 20, 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7926), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7927) },
                    { 21, 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7929), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7929) },
                    { 22, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7931), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7932) },
                    { 23, 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7934), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7934) },
                    { 24, 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7936), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7937) },
                    { 25, 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7939), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7939) },
                    { 26, 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7941), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7942) },
                    { 27, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7898), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(7898) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "IsLecturer", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 12 },
                    { 2, 1, null, 2, 12 },
                    { 3, 1, null, 3, 12 },
                    { 4, 1, null, 4, 12 },
                    { 5, 1, null, 5, 12 },
                    { 6, 1, null, 6, 13 },
                    { 7, 1, null, 7, 13 },
                    { 8, 1, null, 8, 13 },
                    { 9, 1, null, 9, 13 },
                    { 10, 1, null, 10, 13 },
                    { 11, 1, true, 1, 7 },
                    { 12, 1, true, 2, 7 },
                    { 13, 1, true, 3, 7 },
                    { 14, 1, true, 4, 7 },
                    { 15, 1, true, 5, 7 },
                    { 16, 1, true, 6, 27 },
                    { 17, 1, true, 7, 27 },
                    { 18, 1, true, 8, 27 },
                    { 19, 1, true, 9, 27 },
                    { 20, 1, true, 10, 27 },
                    { 21, 3, null, 1, 16 },
                    { 22, 3, null, 2, 16 },
                    { 23, 3, null, 3, 16 },
                    { 24, 3, null, 4, 16 },
                    { 25, 3, null, 5, 16 },
                    { 26, 3, null, 6, 17 },
                    { 27, 3, null, 7, 17 },
                    { 28, 3, null, 8, 17 },
                    { 29, 3, null, 9, 17 },
                    { 30, 3, null, 10, 17 },
                    { 31, 4, null, 1, 18 },
                    { 32, 4, null, 2, 18 },
                    { 33, 4, null, 3, 18 },
                    { 34, 4, null, 4, 18 },
                    { 35, 4, null, 5, 18 },
                    { 36, 4, null, 6, 19 },
                    { 37, 4, null, 7, 19 },
                    { 38, 4, null, 8, 19 },
                    { 39, 4, null, 9, 19 },
                    { 40, 4, null, 10, 19 },
                    { 41, 5, null, 1, 20 },
                    { 42, 5, null, 2, 20 },
                    { 43, 5, null, 3, 20 },
                    { 44, 5, null, 4, 20 },
                    { 45, 5, null, 5, 20 },
                    { 46, 5, null, 6, 21 },
                    { 47, 5, null, 7, 21 },
                    { 48, 5, null, 8, 21 },
                    { 49, 5, null, 9, 21 },
                    { 50, 5, null, 10, 21 },
                    { 51, 2, null, 1, 14 },
                    { 52, 2, null, 2, 14 },
                    { 53, 2, null, 3, 14 },
                    { 54, 2, null, 4, 14 },
                    { 55, 2, null, 5, 14 },
                    { 56, 2, null, 6, 15 },
                    { 57, 2, null, 7, 15 },
                    { 58, 2, null, 8, 15 },
                    { 59, 2, null, 9, 15 },
                    { 60, 2, null, 10, 15 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8173), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8172), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8169), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8170), 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8174) },
                    { 2, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8180), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8179), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8177), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8178), 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8180) },
                    { 3, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8248), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8248), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8247), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8247), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8249) },
                    { 4, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8253), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8252), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8251), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8253) },
                    { 5, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8257), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8257), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8256), 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8258) },
                    { 6, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8261), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8261), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8260), 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8262) },
                    { 7, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8265), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8264), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8264), 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8266) },
                    { 8, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8269), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8268), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8267), 4, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8269) },
                    { 9, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8272), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8272), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8271), 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8273) },
                    { 10, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8277), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8276), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8276), 5, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8278) },
                    { 11, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8281), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8281), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8280), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8280), 6, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8282) },
                    { 12, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8286), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8285), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8284), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8284), 6, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8287) },
                    { 13, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8290), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8289), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8289), 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8290) },
                    { 14, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8293), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8293), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8292), 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8294) },
                    { 15, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8298), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8298), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8297), 8, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8299) },
                    { 16, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8303), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8302), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8302), 8, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8304) },
                    { 17, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8307), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8307), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8306), 9, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8308) },
                    { 18, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8311), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8310), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8310), 9, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8311) },
                    { 19, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8315), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8315), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8314), 10, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8316) },
                    { 20, 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8321), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8320), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8320), 10, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8321) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8365), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8367), 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8368) },
                    { 2, 3, 12, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8370), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8371), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8371) },
                    { 3, 3, 12, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8373), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8374), 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8374) },
                    { 4, 3, 13, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8376), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8376), 11, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8377) },
                    { 5, 3, 13, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8378), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8379), 12, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8380) },
                    { 6, 3, 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8381), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8383), 1, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8384) },
                    { 7, 3, 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8385), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8386), 2, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8387) },
                    { 8, 3, 7, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8388), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8389), 3, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8390) },
                    { 9, 3, 27, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8391), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8392), 11, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8392) },
                    { 10, 3, 27, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8393), new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8394), 12, new DateTime(2024, 10, 8, 13, 51, 55, 593, DateTimeKind.Local).AddTicks(8395) }
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

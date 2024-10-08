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
                    { 1, "Ha Noi", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9438), null, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9456) },
                    { 2, "Da Nang", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9460), null, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9461) },
                    { 3, "Can Tho", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9464), null, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9465) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9468), null, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9469) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9472), null, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9473) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9732), "Not Assign", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9734) },
                    { 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9737), "Waiting to Assign", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9738) },
                    { 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9741), "Assigned", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9742) },
                    { 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9744), "Reviewing", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9745) },
                    { 5, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9747), "Finish Review", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9748) },
                    { 6, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9751), "Complete", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9752) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(968), null, "/usermanagement", "User Management", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(970) },
                    { 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(973), null, "/Admin/History", "History", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(974) },
                    { 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(977), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(978) },
                    { 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(980), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(980) },
                    { 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(982), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(983) },
                    { 6, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(985), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(986) },
                    { 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(989), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(990) },
                    { 8, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(995), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(996) },
                    { 9, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(998), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(999) },
                    { 10, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(992), null, "/Examiner/usermanagement", "View Report", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(993) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1192), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1194) },
                    { 2, null, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1198), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1198) },
                    { 3, null, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1201), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1202) },
                    { 4, null, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1205), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1206) },
                    { 5, null, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1208), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1216) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(121), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(122) },
                    { 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(125), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(126) },
                    { 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(129), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(130) },
                    { 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(132), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(133) },
                    { 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(136), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(137) },
                    { 6, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(261), null, "ECO111", "Microeconomics", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(263) },
                    { 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(265), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(266) },
                    { 8, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(269), null, "ECO201", "International Economics", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(270) },
                    { 9, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(273), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(273) },
                    { 10, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(276), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(277) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9800), "Admin", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9802) },
                    { 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9805), "Examiner", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9806) },
                    { 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9808), "Lecturer", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9809) },
                    { 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9811), "Head of Department", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9812) },
                    { 5, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9815), "Curriculum Development", new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9815) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1112), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1114) },
                    { 2, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1117), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1118) },
                    { 8, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1119), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1121) },
                    { 9, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1122), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1123) },
                    { 3, 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1125), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1126) },
                    { 10, 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1128), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1129) },
                    { 5, 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1138), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1139) },
                    { 4, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1130), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1131) },
                    { 6, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1133), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1134) },
                    { 7, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1136), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(1137) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9869), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9871) },
                    { 2, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9875), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9876) },
                    { 3, 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9932), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9933) },
                    { 4, 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9937), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9938) },
                    { 5, 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9942), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9943) },
                    { 6, 5, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9946), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9948) },
                    { 7, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9952), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9954) },
                    { 8, 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9959), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9960) },
                    { 9, 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9964), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9965) },
                    { 10, 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9968), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9969) },
                    { 11, 5, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9972), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9973) },
                    { 12, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9981), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9982) },
                    { 13, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9985), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9986) },
                    { 14, 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9989), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9990) },
                    { 15, 2, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9994), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9995) },
                    { 16, 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9999), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local) },
                    { 17, 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(4), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(4) },
                    { 18, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(8), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(9) },
                    { 19, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(12), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(13) },
                    { 20, 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(16), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(17) },
                    { 21, 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(21), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(22) },
                    { 22, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(25), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(26) },
                    { 23, 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(29), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(30) },
                    { 24, 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(34), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(34) },
                    { 25, 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(38), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(39) },
                    { 26, 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(42), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(43) },
                    { 27, 1, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9977), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 15, 27, 31, 263, DateTimeKind.Local).AddTicks(9978) }
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
                    { 1, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(535), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(534), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(531), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(532), 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(536) },
                    { 2, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(544), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(543), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(541), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(542), 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(545) },
                    { 3, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(633), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(632), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(630), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(631), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(634) },
                    { 4, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(640), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(639), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(638), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(641) },
                    { 5, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(648), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(648), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(647), 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(649) },
                    { 6, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(655), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(654), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(653), 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(656) },
                    { 7, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(663), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(662), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(661), 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(664) },
                    { 8, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(669), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(668), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(667), 4, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(670) },
                    { 9, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(677), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(674), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(673), 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(678) },
                    { 10, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(684), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(683), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(682), 5, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(685) },
                    { 11, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(691), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(690), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(688), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(689), 6, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(692) },
                    { 12, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(699), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(698), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(696), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(697), 6, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(700) },
                    { 13, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(706), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(705), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(704), 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(706) },
                    { 14, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(713), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(712), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(711), 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(714) },
                    { 15, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(719), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(718), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(717), 8, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(720) },
                    { 16, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(725), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(724), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(724), 8, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(727) },
                    { 17, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(733), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(732), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(731), 9, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(734) },
                    { 18, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(739), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(738), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(737), 9, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(740) },
                    { 19, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(745), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(744), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(743), 10, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(747) },
                    { 20, 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(752), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(751), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(750), 10, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(753) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(847), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(849), 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(851) },
                    { 2, 3, 12, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(854), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(856), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(857) },
                    { 3, 3, 12, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(860), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(862), 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(863) },
                    { 4, 3, 13, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(865), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(866), 11, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(867) },
                    { 5, 3, 13, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(870), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(871), 12, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(872) },
                    { 6, 3, 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(874), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(875), 1, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(876) },
                    { 7, 3, 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(878), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(880), 2, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(881) },
                    { 8, 3, 7, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(883), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(884), 3, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(885) },
                    { 9, 3, 27, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(887), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(889), 11, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(890) },
                    { 10, 3, 27, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(892), new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(894), 12, new DateTime(2024, 10, 8, 15, 27, 31, 264, DateTimeKind.Local).AddTicks(895) }
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

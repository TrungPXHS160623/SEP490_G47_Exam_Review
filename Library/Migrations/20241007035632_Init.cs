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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorAssignments", x => x.AssignmentId);
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
                    { 1, "Ha Noi", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2077), new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2098) },
                    { 2, "Da Nang", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2105), new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2107) },
                    { 3, "Can Tho", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2112), new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2113) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2117), new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2118) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2122), new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2123) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2715), "Not Assign", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2719) },
                    { 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2723), "Waiting to Assign", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2725) },
                    { 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2728), "Assigned", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2730) },
                    { 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2733), "Reviewing", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2735) },
                    { 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2737), "Finish Review", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2738) },
                    { 6, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2741), "Complete", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2742) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8914), null, "/usermanagement", "User management", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8933) },
                    { 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8938), null, "/Admin/History", "History", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8941) },
                    { 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8944), null, "/TestDepartment/ExamList", "Exam List", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8946) },
                    { 4, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8949), null, "/HeadDepartment/ExamList", "Head Department List", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8954) },
                    { 5, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8957), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(8958) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9216), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9219) },
                    { 2, null, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9224), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9226) },
                    { 3, null, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9229), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9230) },
                    { 4, null, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9234), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9235) },
                    { 5, null, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9239), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9240) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3519), "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3524) },
                    { 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3532), "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3536) },
                    { 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3544), "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3548) },
                    { 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3556), "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3560) },
                    { 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3568), "NWC203c", "Computer Networking", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3574) },
                    { 6, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3581), "ECO111", "Microeconomics", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3585) },
                    { 7, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3593), "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3597) },
                    { 8, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3605), "ECO201", "International Economics", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3610) },
                    { 9, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3617), "ACC101", "Principles of Accounting", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3621) },
                    { 10, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3629), "MKT101", "Marketing Principles", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3633) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2827), "Admin", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2830) },
                    { 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2834), "Examiner", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2836) },
                    { 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2839), "Lecturer", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2840) },
                    { 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2844), "Head of Department", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2872) },
                    { 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2902), "Program Developer", new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(2904) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9066), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9068) },
                    { 2, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9072), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9074) },
                    { 3, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9077), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9078) },
                    { 4, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9081), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9082) },
                    { 5, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9085), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9087) },
                    { 3, 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9089), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9091) },
                    { 4, 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9093), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9095) },
                    { 5, 4, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9097), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(9098) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3042), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3047) },
                    { 2, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3058), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3063) },
                    { 3, 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3073), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3078) },
                    { 4, 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3087), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3092) },
                    { 5, 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3102), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3106) },
                    { 6, 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3115), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3119) },
                    { 7, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3129), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3132) },
                    { 8, 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3142), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3146) },
                    { 9, 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3183), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3187) },
                    { 10, 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3198), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3202) },
                    { 11, 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3213), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3217) },
                    { 12, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3246), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3250) },
                    { 13, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3260), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3264) },
                    { 14, 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3275), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3276) },
                    { 15, 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3281), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3282) },
                    { 16, 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3286), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3288) },
                    { 17, 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3293), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3294) },
                    { 18, 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3300), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3301) },
                    { 19, 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3305), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3307) },
                    { 20, 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3310), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3312) },
                    { 21, 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3320), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3321) },
                    { 22, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3325), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3326) },
                    { 23, 2, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3331), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3332) },
                    { 24, 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3336), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3337) },
                    { 25, 4, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3341), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3343) },
                    { 26, 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3347), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3349) },
                    { 27, 1, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3227), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 7, 10, 56, 28, 750, DateTimeKind.Local).AddTicks(3231) }
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
                    { 10, 1, 10, 13 },
                    { 11, 2, 1, 14 },
                    { 12, 2, 2, 14 },
                    { 13, 2, 3, 14 },
                    { 14, 2, 4, 14 },
                    { 15, 2, 5, 14 },
                    { 16, 2, 6, 15 },
                    { 17, 2, 7, 15 },
                    { 18, 2, 8, 15 },
                    { 19, 2, 9, 15 },
                    { 20, 2, 10, 15 },
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
                    { 50, 5, 10, 21 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6755), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6751), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6631), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6747), 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6756) },
                    { 2, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6827), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6823), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6816), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6818), 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6828) },
                    { 3, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6844), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6843), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6835), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6841), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6845) },
                    { 4, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6859), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6858), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6855), "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6857), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6861) },
                    { 5, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6875), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6873), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6870), "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6872), 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6877) },
                    { 6, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6890), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6888), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6886), "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6887), 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6894) },
                    { 7, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6906), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6905), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6902), "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6904), 4, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6909) },
                    { 8, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6926), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6923), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6920), "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6921), 4, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6929) },
                    { 9, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6938), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6937), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6935), "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6936), 5, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6942) },
                    { 10, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6957), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6954), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6950), "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6951), 5, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6960) },
                    { 11, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6972), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6969), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6965), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6966), 6, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6976) },
                    { 12, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6985), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6982), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6980), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6981), 6, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6988) },
                    { 13, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7002), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6999), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6996), "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(6997), 7, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7005) },
                    { 14, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7016), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7014), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7011), "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7013), 7, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7019) },
                    { 15, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7162), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7161), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7154), "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7160), 8, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7164) },
                    { 16, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7177), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7176), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7171), "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7174), 8, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7178) },
                    { 17, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7192), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7191), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7185), "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7189), 9, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7193) },
                    { 18, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7208), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7207), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7205), "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7206), 9, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7211) },
                    { 19, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7223), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7221), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7219), "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7220), 10, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7224) },
                    { 20, 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7237), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7236), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7234), "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7235), 10, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7238) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 12, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7764), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7775), 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7777) },
                    { 2, 12, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7782), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7787), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7791) },
                    { 3, 12, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7794), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7795), 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7796) },
                    { 4, 13, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7801), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7804), 11, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7808) },
                    { 5, 13, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7810), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7811), 12, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7813) },
                    { 6, 7, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7818), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7822), 1, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7823) },
                    { 7, 7, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7826), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7827), 2, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7828) },
                    { 8, 7, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7834), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7837), 3, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7839) },
                    { 9, 27, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7841), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7842), 11, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7844) },
                    { 10, 27, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7848), new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7851), 12, new DateTime(2024, 10, 7, 10, 56, 28, 752, DateTimeKind.Local).AddTicks(7854) }
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

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
                    { 1, "Ha Noi", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4235), null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4248) },
                    { 2, "Da Nang", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4251), null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4251) },
                    { 3, "Can Tho", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4253), null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4254) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4255), null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4256) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4257), null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4258) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4399), "Not Assign", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4400) },
                    { 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4402), "Waiting to Assign", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4402) },
                    { 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4404), "Assigned", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4404) },
                    { 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4406), "Reviewing", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4406) },
                    { 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4408), "Finish Review", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4408) },
                    { 6, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4409), "Complete", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4410) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4982), null, "/usermanagement", "User Management", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4984) },
                    { 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4985), null, "/Admin/History", "History (Not Available)", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4986) },
                    { 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4987), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4988) },
                    { 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4990), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4990) },
                    { 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4991), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4992) },
                    { 6, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4993), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4993) },
                    { 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4995), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4995) },
                    { 8, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5026), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5026) },
                    { 9, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5028), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5028) },
                    { 10, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4996), null, "/Examiner/usermanagement", "View Report", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4997) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5096), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5097) },
                    { 2, null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5100), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5101) },
                    { 3, null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5102), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5103) },
                    { 4, null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5106), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5106) },
                    { 5, null, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5108), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5108) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4615), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4616) },
                    { 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4618), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4618) },
                    { 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4646), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4646) },
                    { 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4648), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4649) },
                    { 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4650), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4650) },
                    { 6, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4652), null, "ECO111", "Microeconomics", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4653) },
                    { 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4654), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4655) },
                    { 8, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4656), null, "ECO201", "International Economics", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4657) },
                    { 9, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4658), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4659) },
                    { 10, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4660), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4660) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4434), "Admin", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4435) },
                    { 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4436), "Examiner", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4437) },
                    { 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4438), "Lecturer", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4439) },
                    { 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4440), "Head of Department", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4441) },
                    { 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4442), "Curriculum Development", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4442) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5056), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5057) },
                    { 2, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5059), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5059) },
                    { 8, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5060), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5061) },
                    { 9, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5062), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5062) },
                    { 3, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5063), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5064) },
                    { 10, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5065), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5066) },
                    { 5, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5072), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5072) },
                    { 4, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5067), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5067) },
                    { 6, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5068), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5069) },
                    { 7, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5070), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(5071) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4466), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4467) },
                    { 2, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4512), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4512) },
                    { 3, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4514), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4515) },
                    { 4, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4517), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4518) },
                    { 5, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4520), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4521) },
                    { 6, 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4523), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4523) },
                    { 7, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4525), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4526) },
                    { 8, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4528), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4528) },
                    { 9, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4530), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4531) },
                    { 10, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4533), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4534) },
                    { 11, 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4536), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4537) },
                    { 12, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4541), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4541) },
                    { 13, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4543), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4544) },
                    { 14, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4546), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4547) },
                    { 15, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4549), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4549) },
                    { 16, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4552), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4552) },
                    { 17, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4554), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4555) },
                    { 18, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4557), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4557) },
                    { 19, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4559), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4560) },
                    { 20, 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4561), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4562) },
                    { 21, 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4564), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4564) },
                    { 22, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4566), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4567) },
                    { 23, 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4569), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4570) },
                    { 24, 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4572), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4572) },
                    { 25, 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4574), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4574) },
                    { 26, 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4576), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4577) },
                    { 27, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4538), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4539) },
                    { 28, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4579), true, "hunglthe160235@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4579) }
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
                    { 1, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4795), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4794), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4792), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4793), 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4795) },
                    { 2, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4820), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4819), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4818), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4819), 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4821) },
                    { 3, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4824), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4824), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4823), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4823), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4825) },
                    { 4, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4828), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4828), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4827), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4829) },
                    { 5, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4833), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4832), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4832), 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4833) },
                    { 6, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4836), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4836), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4835), 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4837) },
                    { 7, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4840), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4839), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4839), 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4841) },
                    { 8, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4844), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4843), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4843), 4, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4844) },
                    { 9, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4849), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4848), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4848), 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4849) },
                    { 10, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4853), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4852), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4852), 5, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4854) },
                    { 11, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4857), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4857), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4855), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4856), 6, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4857) },
                    { 12, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4861), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4860), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4859), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4860), 6, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4861) },
                    { 13, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4864), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4864), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4863), 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4865) },
                    { 14, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4868), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4867), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4867), 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4868) },
                    { 15, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4872), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4871), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4870), 8, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4872) },
                    { 16, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4875), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4875), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4874), 8, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4876) },
                    { 17, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4879), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4878), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4877), 9, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4879) },
                    { 18, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4882), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4881), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4881), 9, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4882) },
                    { 19, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4885), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4885), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4884), 10, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4886) },
                    { 20, 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4890), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4888), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4888), 10, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4891) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4927), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4928), 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4929) },
                    { 2, 3, 12, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4931), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4932), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4933) },
                    { 3, 3, 12, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4935), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4936), 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4937) },
                    { 4, 3, 13, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4938), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4939), 11, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4940) },
                    { 5, 3, 13, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4941), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4942), 12, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4942) },
                    { 6, 3, 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4943), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4944), 1, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4945) },
                    { 7, 3, 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4946), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4947), 2, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4947) },
                    { 8, 3, 7, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4948), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4949), 3, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4950) },
                    { 9, 3, 27, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4951), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4953), 11, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4953) },
                    { 10, 3, 27, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4955), new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4955), 12, new DateTime(2024, 10, 8, 21, 8, 25, 476, DateTimeKind.Local).AddTicks(4956) }
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

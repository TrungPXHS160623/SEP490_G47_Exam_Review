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
                    IsLecture = table.Column<bool>(type: "bit", nullable: true)
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
                    { 1, "Ha Noi", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6278), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6292) },
                    { 2, "Da Nang", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6294), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6295) },
                    { 3, "Can Tho", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6297), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6297) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6298), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6299) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6301), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6301) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6415), "Not Assign", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6417) },
                    { 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6419), "Waiting to Assign", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6420) },
                    { 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6421), "Assigned", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6422) },
                    { 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6423), "Reviewing", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6424) },
                    { 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6426), "Finish Review", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6426) },
                    { 6, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6427), "Complete", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6428) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7033), null, "/usermanagement", "User management", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7034) },
                    { 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7036), null, "/Admin/History", "History", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7036) },
                    { 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7038), null, "/TestDepartment/ExamList", "Exam List", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7038) },
                    { 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7044), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7045) },
                    { 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7039), null, "/HeadDepartment/ExamList", "Head Department List", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7040) },
                    { 6, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7041), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7041) },
                    { 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7043), null, "/Examiner/UserManagement", "User management", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7043) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignemtId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7102), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7103) },
                    { 2, null, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7105), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7106) },
                    { 3, null, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7107), 3, "Update the definition to clarify that asynchronous programming allows multiple tasks to run concurrently without blocking.", "In PRN221, question 3 incorrectly defines the concept of asynchronous programming.", 8f, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7108) },
                    { 4, null, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7109), 4, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ECO111, question 4 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7110) },
                    { 5, null, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7111), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ECO111, question 5 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7112) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6667), "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6668) },
                    { 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6670), "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6670) },
                    { 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6672), "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6672) },
                    { 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6673), "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6674) },
                    { 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6675), "NWC203c", "Computer Networking", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6676) },
                    { 6, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6677), "ECO111", "Microeconomics", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6678) },
                    { 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6679), "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6679) },
                    { 8, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6681), "ECO201", "International Economics", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6681) },
                    { 9, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6682), "ACC101", "Principles of Accounting", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6683) },
                    { 10, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6684), "MKT101", "Marketing Principles", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6685) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6480), "Admin", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6482) },
                    { 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6484), "Examiner", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6485) },
                    { 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6487), "Lecturer", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6487) },
                    { 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6489), "Head of Department", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6489) },
                    { 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6490), "Program Developer", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6491) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7068), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7069) },
                    { 2, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7071), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7071) },
                    { 6, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7073), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7073) },
                    { 3, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7074), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7075) },
                    { 7, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7076), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7076) },
                    { 4, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7077), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7078) },
                    { 5, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7079), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(7080) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6546), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6547) },
                    { 2, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6549), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6550) },
                    { 3, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6552), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6552) },
                    { 4, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6554), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6555) },
                    { 5, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6557), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6558) },
                    { 6, 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6560), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6560) },
                    { 7, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6562), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6563) },
                    { 8, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6564), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6565) },
                    { 9, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6568), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6568) },
                    { 10, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6570), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6571) },
                    { 11, 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6572), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6573) },
                    { 12, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6577), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6578) },
                    { 13, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6580), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6580) },
                    { 14, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6582), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6583) },
                    { 15, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6585), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6585) },
                    { 16, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6587), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6588) },
                    { 17, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6590), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6591) },
                    { 18, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6592), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6593) },
                    { 19, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6595), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6595) },
                    { 20, 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6597), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6598) },
                    { 21, 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6599), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6600) },
                    { 22, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6602), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6602) },
                    { 23, 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6604), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6604) },
                    { 24, 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6606), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6606) },
                    { 25, 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6608), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6609) },
                    { 26, 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6610), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6612) },
                    { 27, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6575), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6575) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "IsLecture", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 12 },
                    { 2, 1, false, 2, 12 },
                    { 3, 1, false, 3, 12 },
                    { 4, 1, false, 4, 12 },
                    { 5, 1, false, 5, 12 },
                    { 6, 1, false, 6, 13 },
                    { 7, 1, false, 7, 13 },
                    { 8, 1, false, 8, 13 },
                    { 9, 1, false, 9, 13 },
                    { 10, 1, false, 10, 13 },
                    { 11, 2, null, 1, 14 },
                    { 12, 2, null, 2, 14 },
                    { 13, 2, null, 3, 14 },
                    { 14, 2, null, 4, 14 },
                    { 15, 2, null, 5, 14 },
                    { 16, 2, null, 6, 15 },
                    { 17, 2, null, 7, 15 },
                    { 18, 2, null, 8, 15 },
                    { 19, 2, null, 9, 15 },
                    { 20, 2, null, 10, 15 },
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
                    { 51, 1, true, 1, 7 },
                    { 52, 1, true, 2, 7 },
                    { 53, 1, true, 3, 7 },
                    { 54, 1, true, 4, 7 },
                    { 55, 1, true, 5, 7 },
                    { 56, 1, true, 6, 27 },
                    { 57, 1, true, 7, 27 },
                    { 58, 1, true, 8, 27 },
                    { 59, 1, true, 9, 27 },
                    { 60, 1, true, 10, 27 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6837), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6836), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6833), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6835), 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6837) },
                    { 2, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6842), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6841), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6840), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6841), 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6843) },
                    { 3, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6846), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6846), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6845), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6845), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6847) },
                    { 4, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6850), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6850), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6849), "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6849), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6851) },
                    { 5, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6854), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6854), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6853), "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6853), 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6855) },
                    { 6, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6858), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6857), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6856), "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6857), 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6859) },
                    { 7, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6862), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6862), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6861), "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6861), 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6863) },
                    { 8, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6867), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6867), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6865), "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6866), 4, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6868) },
                    { 9, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6871), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6870), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6869), "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6870), 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6871) },
                    { 10, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6875), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6874), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6873), "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6874), 5, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6875) },
                    { 11, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6878), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6878), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6877), "ECO111_Q1_10_111222", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6877), 6, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6879) },
                    { 12, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6883), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6882), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6881), "ECO111_Q2_5_222111", "Block 5 (5 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6882), 6, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6883) },
                    { 13, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6887), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6886), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6885), "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6886), 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6888) },
                    { 14, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6891), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6890), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6889), "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6890), 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6891) },
                    { 15, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6895), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6894), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6893), "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6894), 8, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6895) },
                    { 16, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6898), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6898), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6897), "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6897), 8, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6900) },
                    { 17, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6903), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6902), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6901), "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6902), 9, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6903) },
                    { 18, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6907), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6906), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6905), "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6906), 9, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6907) },
                    { 19, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6910), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6910), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6909), "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6909), 10, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6911) },
                    { 20, 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6914), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6914), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6913), "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6913), 10, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6915) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6947), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6949), 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6949) },
                    { 2, 3, 12, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6952), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6952), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6953) },
                    { 3, 3, 12, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6954), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6955), 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6956) },
                    { 4, 3, 13, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6957), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6958), 11, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6958) },
                    { 5, 3, 13, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6959), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6960), 12, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6961) },
                    { 6, 3, 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6962), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6962), 1, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6963) },
                    { 7, 3, 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6964), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6965), 2, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6965) },
                    { 8, 3, 7, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6966), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6967), 3, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6967) },
                    { 9, 3, 27, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6968), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6969), 11, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6970) },
                    { 10, 3, 27, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6971), new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6971), 12, new DateTime(2024, 10, 8, 11, 22, 29, 45, DateTimeKind.Local).AddTicks(6973) }
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

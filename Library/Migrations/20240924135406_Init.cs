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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
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
                        name: "FK_Users_Campuses_CampusId",
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
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_Users_HeadOfDepartmentId",
                        column: x => x.HeadOfDepartmentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExamDuration = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ExamType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreaterId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Exams_ExamStatuses_ExamStatusId",
                        column: x => x.ExamStatusId,
                        principalTable: "ExamStatuses",
                        principalColumn: "ExamStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Users_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ExamAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AssignedBy = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_ExamAssignments_Departments_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_ExamAssignments_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId");
                    table.ForeignKey(
                        name: "FK_ExamAssignments_Users_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionSolutionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reports_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hanoi", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5580), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5593) },
                    { 2, "Ho Chi Minh", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5594), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5595) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5762), "Not started", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5764) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5765), "In progress", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5766) },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5767), "Completed", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5768) },
                    { 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5769), "Cancelled", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5770) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6040), "Dashboard", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6041) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6042), "Exam Management", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6042) },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6044), "User Management", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6044) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5793), "Admin", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5794) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5795), "Examiner", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5796) },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5797), "Lecturer", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5799) },
                    { 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5800), "Head of Department", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5800) },
                    { 5, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5802), "Program Developer", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5802) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6063), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6064) },
                    { 3, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6073), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6074) },
                    { 2, 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6066), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6066) },
                    { 2, 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6068), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6068) },
                    { 2, 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6069), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6070) },
                    { 2, 5, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6071), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6072) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5831), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5832) },
                    { 2, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5835), true, "examiner@fpt.edu.vn", 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5835) },
                    { 3, 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5837), true, "lecturer@fpt.edu.vn", 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5838) },
                    { 4, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5840), true, "head@fpt.edu.vn", 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5841) },
                    { 5, 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5843), true, "developer@fpt.edu.vn", 5, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5844) },
                    { 6, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5847), true, "trunghp@fpt.edu.vn", 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5847) }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreateDate", "DepartmentName", "HeadOfDepartmentId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5870), "Information Technology", 4, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5871) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5873), "Data Science", 6, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5873) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "DepartmentId", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5895), 1, "C# Programming", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5895) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5897), 1, "Computer Science", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5897) },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5899), 2, "Machine Learning", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5900) }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5923), 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5923), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5921), "EXAM001", "10w", 1, "Essay", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5922), 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5924) },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5959), 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5959), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5957), "EXAM002", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5958), 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5960) },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5964), 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5963), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5962), "EXAM003", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5963), 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5964) }
                });

            migrationBuilder.InsertData(
                table: "ExamAssignments",
                columns: new[] { "AssignmentId", "AssignedBy", "AssignedTo", "AssignmentDate", "CreateDate", "ExamId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5986), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5987), 1, "Pending", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5988) },
                    { 2, 2, 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5990), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5990), 2, "Assigned", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5991) },
                    { 3, 2, 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5992), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5993), 3, "Assigned", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(5993) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignedTo", "AssignmentDate", "CreateDate", "ExamId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6013), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6014), 1, "Pending", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6015) },
                    { 2, 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6016), new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6017), 2, "Assigned", new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6018) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReviewId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6094), 1, 1, "Solution explanation 1", "Report 1", 90f, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6095), 3 },
                    { 2, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6097), 2, 2, "Solution explanation 2", "Report 2", 85f, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6098), 3 },
                    { 3, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6099), 3, 3, "Solution explanation 3", "Report 3", 75f, new DateTime(2024, 9, 24, 20, 54, 5, 914, DateTimeKind.Local).AddTicks(6100), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadOfDepartmentId",
                table: "Departments",
                column: "HeadOfDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssignments_AssignedBy",
                table: "ExamAssignments",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssignments_AssignedTo",
                table: "ExamAssignments",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssignments_ExamId",
                table: "ExamAssignments",
                column: "ExamId");

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
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAssignments_ExamId",
                table: "InstructorAssignments",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_MenuId",
                table: "MenuRoles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ExamId",
                table: "Reports",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DepartmentId",
                table: "Subjects",
                column: "DepartmentId");

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
                name: "ExamAssignments");

            migrationBuilder.DropTable(
                name: "InstructorAssignments");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "ExamStatuses");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    CampusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    ExamStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusContent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStatuses", x => x.ExamStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    DepartmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: false),
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
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    ExamCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExamDuration = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ExamType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreaterId = table.Column<int>(type: "int", nullable: false),
                    ExamStatusID = table.Column<int>(type: "int", nullable: false),
                    EstimatedTimeTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                    table.ForeignKey(
                        name: "FK_Exams_ExamStatuses_ExamStatusID",
                        column: x => x.ExamStatusID,
                        principalTable: "ExamStatuses",
                        principalColumn: "ExamStatusID",
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
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAssignments_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Restrict);
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
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstructorAssignments_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionSolutionDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
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
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hanoi", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8763), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8774) },
                    { 2, "Ho Chi Minh", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8776), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8776) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusID", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8943), "Not started", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8944) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8946), "In progress", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8947) },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8948), "Completed", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8949) },
                    { 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8950), "Cancelled", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8954) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9240), "Dashboard", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9240) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9242), "Exam Management", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9243) },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9244), "User Management", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9244) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8981), "Admin", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8981) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8983), "Examiner", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8984) },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8985), "Lecturer", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8985) },
                    { 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8986), "Head of Department", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8987) },
                    { 5, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8988), "Program Developer", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(8988) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9268), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9269) },
                    { 3, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9276), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9277) },
                    { 2, 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9270), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9271) },
                    { 2, 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9272), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9273) },
                    { 2, 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9273), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9274) },
                    { 2, 5, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9275), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9275) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9015), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9016) },
                    { 2, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9018), true, "examiner@fpt.edu.vn", 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9019) },
                    { 3, 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9020), true, "lecturer@fpt.edu.vn", 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9021) },
                    { 4, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9022), true, "head@fpt.edu.vn", 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9022) },
                    { 5, 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9024), true, "developer@fpt.edu.vn", 5, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9024) },
                    { 6, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9026), true, "trunghp@fpt.edu.vn", 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9027) }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreateDate", "DepartmentName", "HeadOfDepartmentId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9053), "Information Technology", 4, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9054) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9055), "Data Science", 6, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9056) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "DepartmentId", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9076), 1, "C# Programming", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9078) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9079), 1, "Computer Science", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9080) },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9081), 2, "Machine Learning", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9081) }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusID", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9116), 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9116), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9114), "EXAM001", "10w", 1, "Essay", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9115), 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9117) },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9120), 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9120), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9118), "EXAM002", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9119), 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9121) },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9123), 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9123), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9122), "EXAM003", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9123), 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9124) }
                });

            migrationBuilder.InsertData(
                table: "ExamAssignments",
                columns: new[] { "AssignmentId", "AssignedBy", "AssignedTo", "AssignmentDate", "CreateDate", "ExamId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9144), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9145), 1, "Pending", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9146) },
                    { 2, 2, 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9148), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9148), 2, "Assigned", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9149) },
                    { 3, 2, 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9150), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9150), 3, "Assigned", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9151) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignedTo", "AssignmentDate", "CreateDate", "ExamId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9190), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9192), 1, "Pending", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9193) },
                    { 2, 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9195), new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9195), 2, "Assigned", new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9196) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReviewId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9301), 1, 1, "Solution explanation 1", "Report 1", 90f, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9302), 3 },
                    { 2, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9304), 2, 2, "Solution explanation 2", "Report 2", 85f, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9305), 3 },
                    { 3, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9306), 3, 3, "Solution explanation 3", "Report 3", 75f, new DateTime(2024, 9, 24, 15, 12, 0, 166, DateTimeKind.Local).AddTicks(9307), 3 }
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
                name: "IX_Exams_ExamStatusID",
                table: "Exams",
                column: "ExamStatusID");

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

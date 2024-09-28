﻿using System;
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
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HeadOfDepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Users",
                        column: x => x.HeadOfDepartmentId,
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
                    CampusId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Exams_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, "Hanoi", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9536), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9549) },
                    { 2, "Ho Chi Minh", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9552), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9552) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9653), "Not started", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9655) },
                    { 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9656), "In progress", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9657) },
                    { 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9658), "Completed", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9658) },
                    { 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9660), "Cancelled", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9660) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9878), null, "/usermanagement", "User management", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9879) },
                    { 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9881), null, "/Admin/History", "History", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9881) },
                    { 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9882), null, "/TestDepartment/ExamList", "Exam List", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9883) },
                    { 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9884), null, "/HeadDepartment/ExamList", "Head Department List", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9885) },
                    { 5, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9886), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9886) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9685), "Admin", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9685) },
                    { 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9687), "Examiner", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9687) },
                    { 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9689), "Lecturer", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9689) },
                    { 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9691), "Head of Department", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9691) },
                    { 5, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9693), "Program Developer", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9693) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9909), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9910) },
                    { 2, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9911), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9912) },
                    { 3, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9913), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9913) },
                    { 4, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9915), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9915) },
                    { 5, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9916), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9917) },
                    { 3, 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9917), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9918) },
                    { 4, 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9919), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9919) },
                    { 5, 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9920), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9921) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9723), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9723) },
                    { 2, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9728), true, "examiner@fpt.edu.vn", 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9729) },
                    { 3, 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9757), true, "lecturer@fpt.edu.vn", 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9757) },
                    { 4, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9760), true, "head@fpt.edu.vn", 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9761) },
                    { 5, 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9763), true, "developer@fpt.edu.vn", 5, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9763) },
                    { 6, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9765), true, "trunghp@fpt.edu.vn", 4, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9766) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "HeadOfDepartmentId", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9793), 1, "PRN231", "C# Programming", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9794) },
                    { 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9795), 1, "CSI123", "Computer Science", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9795) },
                    { 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9797), 2, "MLN123", "Machine Learning", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9797) }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9825), 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9825), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9823), "EXAM001", "10w", 1, "Essay", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9824), 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9827) },
                    { 2, 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9831), 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9830), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9829), "EXAM002", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9830), 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9831) },
                    { 3, 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9836), 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9835), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9833), "EXAM003", "10w", 1, "Multiple Choice", new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9835), 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9836) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9855), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9856), 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9858) },
                    { 2, 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9859), new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9859), 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9860) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReviewId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9946), 1, 1, "Solution explanation 1", "Report 1", 90f, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9946), 3 },
                    { 2, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9948), 2, 2, "Solution explanation 2", "Report 2", 85f, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9949), 3 },
                    { 3, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9950), 3, 3, "Solution explanation 3", "Report 3", 75f, new DateTime(2024, 9, 26, 10, 8, 30, 958, DateTimeKind.Local).AddTicks(9951), 3 }
                });

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
                column: "HeadOfDepartmentId");

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
                name: "InstructorAssignments");

            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "Menus");

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
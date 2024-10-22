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
                name: "Semester",
                columns: table => new
                {
                    SemesterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.SemesterId);
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailFe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SemesterId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_CampusUserSubject_Semesters",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "SemesterId");
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
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    ExamStatusId = table.Column<int>(type: "int", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_Exams_Semesters",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "SemesterId");
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
                    ExamTestDuration = table.Column<TimeSpan>(type: "time", nullable: true),
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
                    AssignmentId = table.Column<int>(type: "int", nullable: true),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionSolutionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_InstructorAssignments",
                        column: x => x.AssignmentId,
                        principalTable: "InstructorAssignments",
                        principalColumn: "AssignmentId");
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2373), null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2386) },
                    { 2, "Da Nang", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2388), null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2389) },
                    { 3, "Can Tho", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2393), null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2394) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2395), null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2396) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2397), null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2398) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2638), "Unassigned", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2640) },
                    { 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2641), "Pending", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2642) },
                    { 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2643), "Assigned", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2644) },
                    { 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2646), "Reviewed", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2646) },
                    { 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2648), "Erroneous", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2648) },
                    { 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2650), "Faultless", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2650) },
                    { 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2651), "Completed", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2652) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3746), null, "/usermanagement", "User Management", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3747) },
                    { 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3748), null, "/Admin/History", "User Log", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3750) },
                    { 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3752), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3752) },
                    { 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3753), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3754) },
                    { 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3756), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3757) },
                    { 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3759), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3759) },
                    { 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3760), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3761) },
                    { 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3764), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3765) },
                    { 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3775), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3776) },
                    { 10, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3762), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3763) },
                    { 11, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3767), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3768) },
                    { 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3770), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3771) },
                    { 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3772), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3773) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3982), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3983) },
                    { 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3985), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3986) },
                    { 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3987), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3988) },
                    { 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3990), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3990) },
                    { 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3992), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3992) },
                    { 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3994), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3995) },
                    { 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3996), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3997) },
                    { 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3999), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3999) },
                    { 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4001), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4002) },
                    { 10, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4004), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4004) },
                    { 11, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4006), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4006) },
                    { 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4008), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4009) },
                    { 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4010), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(4011) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3050), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3051) },
                    { 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3053), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3054) },
                    { 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3055), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3056) },
                    { 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3058), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3059) },
                    { 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3061), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3062) },
                    { 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3065), null, "ENM401", "Business English", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3065) },
                    { 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3068), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3069) },
                    { 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3071), null, "ECO201", "International Economics", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3071) },
                    { 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3073), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3074) },
                    { 10, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3075), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3076) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2704), "Admin", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2705) },
                    { 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2707), "Examiner", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2707) },
                    { 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2709), "Lecturer", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2710) },
                    { 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2711), "Head of Department", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2712) },
                    { 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2713), "Curriculum Development", new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2713) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3807), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3808) },
                    { 2, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3810), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3811) },
                    { 8, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3812), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3813) },
                    { 9, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3814), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3816) },
                    { 3, 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3817), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3817) },
                    { 10, 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3818), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3819) },
                    { 11, 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3826), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3827) },
                    { 13, 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3828), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3829) },
                    { 5, 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3832), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3833) },
                    { 4, 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3820), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3820) },
                    { 6, 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3821), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3822) },
                    { 7, 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3824), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3825) },
                    { 12, 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3830), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3831) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2749), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2750) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2830), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2831) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2835), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2835) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2838), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2840) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2843), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2843) },
                    { 6, "Huế", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2846), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2847) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2854), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2855) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2858), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2858) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2862), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2863) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2866), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2866) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2869), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2870) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2880), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2881) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2884), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2885) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2888), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2889) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2893), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2893) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2896), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2897) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2901), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2903) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2906), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2907) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2911), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2912) },
                    { 20, "Huế", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2915), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2916) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2919), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2919) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2932), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2932) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2937), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2938) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2993), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2994) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2997), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2998) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3001), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3001) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2873), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2873) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2850), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2850) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2923), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2924) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2876), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2877) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2927), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(2928) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 29 },
                    { 2, 1, 1, 2, 29 },
                    { 3, 1, 1, 3, 29 },
                    { 4, 1, 1, 4, 31 },
                    { 5, 1, 1, 5, 31 },
                    { 6, 1, 2, 6, 13 },
                    { 7, 1, 2, 7, 13 },
                    { 8, 1, 2, 8, 13 },
                    { 9, 1, 2, 9, 13 },
                    { 10, 1, 2, 10, 13 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "IsLecturer", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 11, 1, true, 3, 1, 7 },
                    { 12, 1, true, 3, 2, 7 },
                    { 13, 1, true, 3, 3, 7 },
                    { 14, 1, true, 3, 4, 7 },
                    { 15, 1, true, 3, 5, 7 },
                    { 16, 1, true, 4, 6, 27 },
                    { 17, 1, true, 4, 7, 27 },
                    { 18, 1, true, 4, 8, 27 },
                    { 19, 1, true, 4, 9, 27 },
                    { 20, 1, true, 4, 10, 27 }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SemesterId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 21, 3, 5, 1, 16 },
                    { 22, 3, 5, 2, 16 },
                    { 23, 3, 5, 3, 16 },
                    { 24, 3, 5, 4, 16 },
                    { 25, 3, null, 5, 16 },
                    { 26, 3, 6, 6, 17 },
                    { 27, 3, 6, 7, 17 },
                    { 28, 3, 6, 8, 17 },
                    { 29, 3, 6, 9, 17 },
                    { 30, 3, 6, 10, 17 },
                    { 31, 4, 7, 1, 18 },
                    { 32, 4, 7, 2, 18 },
                    { 33, 4, 7, 3, 18 },
                    { 34, 4, 7, 4, 18 },
                    { 35, 4, 7, 5, 18 },
                    { 36, 4, 8, 6, 19 },
                    { 37, 4, 8, 7, 19 },
                    { 38, 4, 8, 8, 19 },
                    { 39, 4, 8, 9, 19 },
                    { 40, 4, 8, 10, 19 },
                    { 41, 5, 9, 1, 20 },
                    { 42, 5, 9, 2, 20 },
                    { 43, 5, 9, 3, 20 },
                    { 44, 5, 9, 4, 20 },
                    { 45, 5, 9, 5, 20 },
                    { 46, 5, 10, 6, 21 },
                    { 47, 5, 10, 7, 21 },
                    { 48, 5, 10, 8, 21 },
                    { 49, 5, 10, 9, 21 },
                    { 50, 5, 10, 10, 21 },
                    { 51, 2, 1, 1, 14 },
                    { 52, 2, 1, 2, 14 },
                    { 53, 2, 1, 3, 14 },
                    { 54, 2, 1, 4, 14 },
                    { 55, 2, 1, 5, 14 },
                    { 56, 2, 2, 6, 15 },
                    { 57, 2, 2, 7, 15 },
                    { 58, 2, 2, 8, 15 },
                    { 59, 2, 2, 9, 15 },
                    { 60, 2, 2, 10, 15 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDate", "ExamDuration", "ExamStatusId", "ExamType", "SemesterId", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3451), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3450), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3447), "PRN211_Q1_10_123456", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 5, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3449), 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3453) },
                    { 2, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3459), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3458), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3457), "PRN211_Q2_5_654321", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 5, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3458), 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3460) },
                    { 3, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3464), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3464), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3462), "PRN221_Q1_10_789012", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3463), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3465) },
                    { 4, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3469), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3468), null, "PRN221_Q2_5_210987", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3468), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3470) },
                    { 5, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3474), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3473), null, "PRN231_Q1_10_345678", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3472), 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3474) },
                    { 6, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3478), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3478), null, "PRN231_Q2_5_876543", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3477), 3, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3479) },
                    { 7, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3483), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3482), null, "MAE101_Q1_10_234567", new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3481), 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3483) },
                    { 8, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3487), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3487), null, "MAE101_Q2_5_765432", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3486), 4, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3488) },
                    { 9, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3496), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3496), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3495), 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3497) },
                    { 10, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3501), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3501), null, "NWC203c_Q2_5_876543", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3500), 5, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3502) },
                    { 11, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3506), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3506), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3504), "ENM401_Q1_10_111222", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3505), 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3507) },
                    { 12, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3511), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3511), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3509), "ENM401_Q2_5_222111", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Reading", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3510), 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3512) },
                    { 13, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3517), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3516), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3515), "ENM401_Q3_7_222333", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Writing", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3516), 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3518) },
                    { 14, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3523), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3523), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3521), "ENM401_Q4_9_333111", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Listening", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3521), 6, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3524) },
                    { 15, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3528), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3527), null, "ECO121_Q1_10_333444", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3526), 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3529) },
                    { 16, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3534), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3533), null, "ECO121_Q2_5_444333", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3532), 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3535) },
                    { 17, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3540), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3538), null, "ECO201_Q1_10_555666", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3537), 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3540) },
                    { 18, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3545), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3545), null, "ECO201_Q2_5_666555", new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3544), 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3546) },
                    { 19, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3551), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3550), null, "ACC101_Q1_10_777888", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3549), 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3551) },
                    { 20, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3560), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3559), null, "ACC101_Q2_5_888777", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3559), 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3561) },
                    { 21, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3613), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3612), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3611), 10, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3613) },
                    { 22, 1, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3619), 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3618), null, "MKT101_Q2_5_000999", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3618), 10, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3620) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "ExamTestDuration", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3665), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3667), 1, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3667) },
                    { 2, 3, 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3670), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3671), 2, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3672) },
                    { 3, 3, 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3673), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3674), 3, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3675) },
                    { 4, 3, 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3676), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3677), 11, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3678) },
                    { 5, 3, 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3679), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3681), 12, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3681) },
                    { 6, 3, 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3683), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3684), 13, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3684) },
                    { 7, 3, 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3686), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3686), 14, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3688) },
                    { 8, 4, 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3689), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3690), 1, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3691) },
                    { 9, 4, 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3692), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3693), 2, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3694) },
                    { 10, 4, 7, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3695), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3696), 3, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3697) },
                    { 11, 4, 27, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3698), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3699), 11, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3699) },
                    { 12, 4, 27, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3701), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3701), 12, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3702) },
                    { 13, 4, 27, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3704), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3705), 13, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3706) },
                    { 14, 4, 27, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3707), new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3708), 14, null, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3709) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "FileData", "FileName", "FileSize", "FileType", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3861), null, null, null, null, 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3863) },
                    { 2, 9, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3918), null, null, null, null, 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3919) },
                    { 3, 11, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3922), null, null, null, null, 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3923) },
                    { 4, 12, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3927), null, null, null, null, 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3928) },
                    { 5, 13, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3931), null, null, null, null, 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3932) },
                    { 6, 14, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3936), null, null, null, null, 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 22, 9, 49, 31, 559, DateTimeKind.Local).AddTicks(3937) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_CampusId",
                table: "CampusUserSubject",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_CampusUserSubject_SemesterId",
                table: "CampusUserSubject",
                column: "SemesterId");

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
                name: "IX_Exams_SemesterId",
                table: "Exams",
                column: "SemesterId");

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
                name: "IX_Reports_AssignmentId",
                table: "Reports",
                column: "AssignmentId");

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
                name: "Semester");

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

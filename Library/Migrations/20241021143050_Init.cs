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
                name: "SemesterCampusUserSubject",
                columns: table => new
                {
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    CampusUserSubjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Semester__ACCC9C54F51488F5", x => new { x.SemesterId, x.CampusUserSubjectId });
                    table.ForeignKey(
                        name: "FK__SemesterC__Campu__71D1E811",
                        column: x => x.CampusUserSubjectId,
                        principalTable: "CampusUserSubject",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__SemesterC__Updat__70DDC3D8",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "SemesterId");
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
                    AssignmentId = table.Column<int>(type: "int", nullable: true),
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
                        column: x => x.AssignmentId,
                        principalTable: "InstructorAssignments",
                        principalColumn: "AssignmentId");
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5427), null, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5441) },
                    { 2, "Da Nang", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5443), null, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5444) },
                    { 3, "Can Tho", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5446), null, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5446) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5448), null, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5448) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5450), null, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5450) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5620), "Not Assign", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5622) },
                    { 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5623), "Waiting To Assign", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5624) },
                    { 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5626), "Assigned", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5626) },
                    { 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5628), "Reviewing", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5628) },
                    { 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5630), "Exam With Errors", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5630) },
                    { 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5632), "Faultless Exam", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5632) },
                    { 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5633), "Complete", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5634) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6382), null, "/usermanagement", "User Management", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6384) },
                    { 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6385), null, "/Admin/History", "User Log", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6386) },
                    { 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6387), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6387) },
                    { 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6389), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6389) },
                    { 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6390), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6391) },
                    { 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6392), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6393) },
                    { 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6394), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6394) },
                    { 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6398), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6398) },
                    { 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6404), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6405) },
                    { 10, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6395), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6396) },
                    { 11, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6399), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6400) },
                    { 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6401), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6401) },
                    { 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6403), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6403) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6537), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6538) },
                    { 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6540), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6540) },
                    { 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6542), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6543) },
                    { 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6544), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6545) },
                    { 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6546), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6547) },
                    { 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6548), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6549) },
                    { 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6550), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6551) },
                    { 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6552), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6553) },
                    { 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6555), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6556) },
                    { 10, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6558), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6558) },
                    { 11, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6560), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6560) },
                    { 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6562), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6562) },
                    { 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6564), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6564) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5884), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5885) },
                    { 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5900), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5901) },
                    { 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5902), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5903) },
                    { 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5904), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5905) },
                    { 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5906), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5907) },
                    { 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5908), null, "ENM401", "Business English", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5908) },
                    { 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5910), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5911) },
                    { 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5912), null, "ECO201", "International Economics", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5913) },
                    { 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5914), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5914) },
                    { 10, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5916), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5916) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5662), "Admin", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5663) },
                    { 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5665), "Examiner", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5665) },
                    { 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5667), "Lecturer", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5667) },
                    { 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5668), "Head of Department", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5669) },
                    { 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5670), "Curriculum Development", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5671) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6435), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6435) },
                    { 2, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6437), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6437) },
                    { 8, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6438), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6439) },
                    { 9, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6440), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6440) },
                    { 3, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6442), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6442) },
                    { 10, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6443), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6444) },
                    { 11, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6450), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6451) },
                    { 13, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6452), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6453) },
                    { 5, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6455), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6456) },
                    { 4, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6444), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6446) },
                    { 6, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6447), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6448) },
                    { 7, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6449), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6449) },
                    { 12, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6454), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6454) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5703), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5703) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5707), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5707) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5710), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5711) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5714), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5714) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5718), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5718) },
                    { 6, "Huế", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5721), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5721) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5728), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5728) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5756), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5757) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5760), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5761) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5764), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5764) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5767), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5768) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5777), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5778) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5782), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5782) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5785), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5786) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5789), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5790) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5792), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5793) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5796), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5796) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5799), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5800) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5803), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5804) },
                    { 20, "Huế", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5807), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5807) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5810), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5811) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5820), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5820) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5824), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5824) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5827), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5827) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5830), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5831) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5835), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5836) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5771), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5771) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5724), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5725) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5813), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5814) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5774), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5775) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5817), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(5817) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 29 },
                    { 2, 1, 2, 29 },
                    { 3, 1, 3, 29 },
                    { 4, 1, 4, 31 },
                    { 5, 1, 5, 31 },
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
                    { 1, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6130), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6130), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6127), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6129), 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6131) },
                    { 2, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6136), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6136), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6134), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6135), 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6137) },
                    { 3, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6141), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6140), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6139), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6140), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6141) },
                    { 4, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6146), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6146), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6145), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6147) },
                    { 5, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6150), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6150), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6149), 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6151) },
                    { 6, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6154), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6153), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6153), 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6155) },
                    { 7, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6158), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6158), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6156), 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6159) },
                    { 8, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6162), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6161), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6161), 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6162) },
                    { 9, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6166), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6165), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6164), 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6166) },
                    { 10, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6170), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6169), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6169), 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6170) },
                    { 11, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6174), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6173), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6172), "ENM401_Q1_10_111222", "Block 10 (10 weeks)", 7, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6173), 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6174) },
                    { 12, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6178), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6178), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6176), "ENM401_Q2_5_222111", "Block 10 (10 weeks)", 7, "Reading", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6177), 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6179) },
                    { 13, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6182), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6182), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6181), "ENM401_Q3_7_222333", "Block 10 (10 weeks)", 7, "Writing", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6181), 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6183) },
                    { 14, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6187), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6186), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6184), "ENM401_Q4_9_333111", "Block 10 (10 weeks)", 7, "Listening", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6185), 6, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6187) },
                    { 15, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6190), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6190), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6189), 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6191) },
                    { 16, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6194), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6193), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6193), 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6195) },
                    { 17, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6199), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6199), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6198), 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6200) },
                    { 18, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6203), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6202), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6202), 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6203) },
                    { 19, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6207), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6206), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6205), 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6208) },
                    { 20, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6212), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6211), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6211), 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6212) },
                    { 21, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6215), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6215), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6214), 10, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6216) },
                    { 22, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6219), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6219), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6218), 10, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6220) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6262), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6263), 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6264) },
                    { 2, 3, 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6265), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6266), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6267) },
                    { 3, 3, 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6268), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6269), 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6269) },
                    { 4, 3, 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6270), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6271), 11, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6272) },
                    { 5, 3, 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6273), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6274), 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6274) },
                    { 6, 3, 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6275), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6276), 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6277) },
                    { 7, 3, 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6278), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6279), 14, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6280) },
                    { 8, 4, 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6281), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6282), 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6283) },
                    { 9, 4, 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6284), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6285), 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6286) },
                    { 10, 4, 7, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6288), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6288), 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6289) },
                    { 11, 4, 27, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6335), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6336), 11, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6338) },
                    { 12, 4, 27, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6339), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6340), 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6340) },
                    { 13, 4, 27, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6342), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6343), 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6343) },
                    { 14, 4, 27, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6344), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6345), 14, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6346) }
                });

            migrationBuilder.InsertData(
                table: "SemesterCampusUserSubject",
                columns: new[] { "CampusUserSubjectId", "SemesterId", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6616), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6617) },
                    { 2, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6619), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6619) },
                    { 3, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6620), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6621) },
                    { 4, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6622), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6622) },
                    { 5, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6623), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6623) },
                    { 11, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6624), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6625) },
                    { 12, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6626), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6626) },
                    { 21, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6627), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6627) },
                    { 31, 1, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6628), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6628) },
                    { 6, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6629), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6630) },
                    { 7, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6631), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6631) },
                    { 8, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6632), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6632) },
                    { 9, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6633), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6634) },
                    { 10, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6635), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6636) },
                    { 16, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6637), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6637) },
                    { 17, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6638), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6639) },
                    { 26, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6639), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6640) },
                    { 36, 2, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6641), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6641) },
                    { 4, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6642), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6643) },
                    { 5, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6643), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6644) },
                    { 14, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6645), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6645) },
                    { 15, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6646), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6646) },
                    { 24, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6647), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6648) },
                    { 25, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6649), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6649) },
                    { 34, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6650), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6650) },
                    { 35, 3, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6651), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6651) },
                    { 1, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6652), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6653) },
                    { 2, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6654), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6654) },
                    { 3, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6655), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6655) },
                    { 4, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6656), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6657) },
                    { 5, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6658), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6658) },
                    { 11, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6659), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6659) },
                    { 12, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6660), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6661) },
                    { 21, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6661), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6662) },
                    { 31, 4, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6663), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6663) },
                    { 1, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6664), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6664) },
                    { 2, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6665), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6666) },
                    { 3, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6666), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6667) },
                    { 4, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6668), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6668) },
                    { 5, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6669), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6669) },
                    { 41, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6670), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6671) },
                    { 42, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6672), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6672) },
                    { 51, 5, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6673), new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6673) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6488), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6489) },
                    { 2, 9, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6491), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6492) },
                    { 3, 11, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6494), 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6494) },
                    { 4, 12, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6496), 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6496) },
                    { 5, 13, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6499), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6499) },
                    { 6, 14, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6501), 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 21, 21, 30, 49, 301, DateTimeKind.Local).AddTicks(6501) }
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
                name: "IX_Reports_AssignmentId",
                table: "Reports",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCampusUserSubject_CampusUserSubjectId",
                table: "SemesterCampusUserSubject",
                column: "CampusUserSubjectId");

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
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SemesterCampusUserSubject");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "InstructorAssignments");

            migrationBuilder.DropTable(
                name: "CampusUserSubject");

            migrationBuilder.DropTable(
                name: "Semester");

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

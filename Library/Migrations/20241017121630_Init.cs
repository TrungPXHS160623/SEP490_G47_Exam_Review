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
                name: "UserDetail",
                columns: table => new
                {
                    UserDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailFE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetail", x => x.UserDetailId);
                    table.ForeignKey(
                        name: "FK_User_UserDetail",
                        column: x => x.UserId,
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
                    { 1, "Ha Noi", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4576), null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4593) },
                    { 2, "Da Nang", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4595), null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4596) },
                    { 3, "Can Tho", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4597), null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4598) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4599), null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4600) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4601), null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4601) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4825), "Not Assign", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4827) },
                    { 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4829), "Waiting To Assign", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4829) },
                    { 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4830), "Assigned", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4831) },
                    { 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4832), "Reviewing", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4833) },
                    { 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4834), "Exam With Errors", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4835) },
                    { 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4836), "Faultless Exam", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4837) },
                    { 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4838), "Complete", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4839) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5461), null, "/usermanagement", "User Management", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5462) },
                    { 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5463), null, "/Admin/History", "User Log", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5464) },
                    { 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5465), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5466) },
                    { 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5467), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5468) },
                    { 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5469), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5470) },
                    { 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5471), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5472) },
                    { 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5473), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5473) },
                    { 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5476), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5476) },
                    { 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5482), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5483) },
                    { 10, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5475), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5475) },
                    { 11, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5478), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5478) },
                    { 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5479), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5480) },
                    { 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5481), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5481) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5625), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5625) },
                    { 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5628), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5628) },
                    { 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5630), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5630) },
                    { 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5633), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5633) },
                    { 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5635), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5635) },
                    { 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5637), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5637) },
                    { 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5639), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5639) },
                    { 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5641), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5641) },
                    { 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5643), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5643) },
                    { 10, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5645), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5645) },
                    { 11, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5647), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5648) },
                    { 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5649), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5649) },
                    { 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5651), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5651) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5050), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5052) },
                    { 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5053), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5054) },
                    { 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5055), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5055) },
                    { 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5057), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5058) },
                    { 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5059), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5059) },
                    { 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5060), null, "ENM401", "Business English", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5061) },
                    { 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5062), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5063) },
                    { 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5064), null, "ECO201", "International Economics", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5064) },
                    { 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5066), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5066) },
                    { 10, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5067), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5068) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4869), "Admin", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4871) },
                    { 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4872), "Examiner", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4873) },
                    { 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4874), "Lecturer", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4875) },
                    { 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4876), "Head of Department", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4876) },
                    { 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4877), "Curriculum Development", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4878) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5532), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5532) },
                    { 2, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5533), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5534) },
                    { 8, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5535), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5535) },
                    { 9, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5536), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5537) },
                    { 3, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5538), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5538) },
                    { 10, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5539), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5540) },
                    { 11, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5545), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5545) },
                    { 13, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5546), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5547) },
                    { 5, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5550), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5551) },
                    { 4, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5541), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5541) },
                    { 6, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5542), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5543) },
                    { 7, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5543), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5544) },
                    { 12, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5548), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5549) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4905), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4906) },
                    { 2, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4908), true, "lienkt@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4909) },
                    { 3, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4911), true, "hoanglm@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4911) },
                    { 4, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4913), true, "anhnq@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4914) },
                    { 5, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4915), true, "minhnh@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4916) },
                    { 6, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4918), true, "phongtl@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4918) },
                    { 7, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4922), true, "lanhbt@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4923) },
                    { 8, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4925), true, "khoadt@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4925) },
                    { 9, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4927), true, "hoangtm@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4928) },
                    { 10, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4930), true, "minhph@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4930) },
                    { 11, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4932), true, "trangnt@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4933) },
                    { 12, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4940), true, "namlh@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4940) },
                    { 13, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4966), true, "quangnv@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4966) },
                    { 14, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4968), true, "huylt@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4969) },
                    { 15, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4970), true, "tuanpv@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4971) },
                    { 16, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4973), true, "ngocdt@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4974) },
                    { 17, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4975), true, "minhth@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4976) },
                    { 18, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4979), true, "binhlt@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4979) },
                    { 19, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4981), true, "lanhnv@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4982) },
                    { 20, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4984), true, "duongkt@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4984) },
                    { 21, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4986), true, "phuonglt@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4987) },
                    { 22, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4993), true, "phucdt@fpt.edu.vn", 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4993) },
                    { 23, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4995), true, "thanhnt@fpt.edu.vn", 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4996) },
                    { 24, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4998), true, "hungpv@fpt.edu.vn", 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4998) },
                    { 25, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5000), true, "anhpt@fpt.edu.vn", 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5000) },
                    { 26, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5003), true, "truongvq@fpt.edu.vn", 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5003) },
                    { 27, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4935), true, "quanpt@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4936) },
                    { 28, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4920), true, "hunglthe160235@fpt.edu.vn", 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4921) },
                    { 29, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4988), true, "tuanlmhe161245@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4989) },
                    { 30, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4937), true, "trungpxhs160623@fpt.edu.vn", 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4938) },
                    { 31, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4991), true, "tungtkHS163077@fpt.edu.vn", 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(4991) }
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
                    { 1, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5230), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5229), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5227), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5229), 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5231) },
                    { 2, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5235), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5235), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5234), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5234), 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5236) },
                    { 3, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5239), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5239), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5238), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5238), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5240) },
                    { 4, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5242), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5242), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5241), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5243) },
                    { 5, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5246), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5246), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5245), 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5247) },
                    { 6, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5250), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5249), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5249), 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5250) },
                    { 7, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5254), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5253), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5253), 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5254) },
                    { 8, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5257), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5257), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5256), 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5258) },
                    { 9, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5261), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5260), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5260), 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5261) },
                    { 10, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5264), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5264), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5263), 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5265) },
                    { 11, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5268), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5268), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5266), "ENM401_Q1_10_111222", "Block 10 (10 weeks)", 7, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5267), 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5269) },
                    { 12, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5316), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5316), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5314), "ENM401_Q2_5_222111", "Block 10 (10 weeks)", 7, "Reading", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5315), 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5317) },
                    { 13, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5321), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5320), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5319), "ENM401_Q3_7_222333", "Block 10 (10 weeks)", 7, "Writing", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5319), 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5321) },
                    { 14, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5325), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5324), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5323), "ENM401_Q4_9_333111", "Block 10 (10 weeks)", 7, "Listening", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5324), 6, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5325) },
                    { 15, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5328), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5328), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5327), 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5329) },
                    { 16, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5332), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5331), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5330), 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5332) },
                    { 17, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5336), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5335), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5335), 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5336) },
                    { 18, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5339), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5339), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5338), 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5340) },
                    { 19, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5343), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5342), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5342), 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5343) },
                    { 20, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5346), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5346), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5345), 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5347) },
                    { 21, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5349), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5349), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5348), 10, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5350) },
                    { 22, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5353), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5353), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5352), 10, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5354) }
                });

            migrationBuilder.InsertData(
                table: "UserDetail",
                columns: new[] { "UserDetailId", "Address", "CreateDate", "DateOfBirth", "EmailFE", "FullName", "Gender", "IsActive", "PhoneNumber", "ProfilePicture", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5806), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5806), 1 },
                    { 2, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5809), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5810), 2 },
                    { 3, "Đà Nẵng", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5812), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5812), 3 },
                    { 4, "Nha Trang", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5814), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5815), 4 },
                    { 5, "Cần Thơ", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5817), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5817), 5 },
                    { 6, "Huế", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5819), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5820), 6 },
                    { 7, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5821), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Lê", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5822), 28 },
                    { 8, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5825), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fpt.edu.vn", "Lành Bích", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5825), 7 },
                    { 9, "Hải Phòng", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5827), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fpt.edu.vn", "Khoa Đạt", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5827), 8 },
                    { 10, "Đà Nẵng", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5829), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fpt.edu.vn", "Hoàng Tâm", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5830), 9 },
                    { 11, "Nha Trang", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5832), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fpt.edu.vn", "Minh Phúc", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5833), 10 },
                    { 12, "Cần Thơ", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5834), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fpt.edu.vn", "Trạng Nguyên", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5835), 11 },
                    { 13, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5837), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fpt.edu.vn", "Quân Phạm", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5839), 27 },
                    { 14, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5841), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fpt.edu.vn", "Trung Phạm", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5841), 30 },
                    { 15, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5843), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5844), 22 },
                    { 16, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5845), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5846), 23 },
                    { 17, "Đà Nẵng", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5848), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5848), 24 },
                    { 18, "Nha Trang", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5850), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5850), 25 },
                    { 19, "Cần Thơ", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5852), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5853), 26 },
                    { 20, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5855), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fpt.edu.vn", "Nam Lê", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5855), 12 },
                    { 21, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5858), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fpt.edu.vn", "Quang Nguyễn", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5858), 13 },
                    { 22, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5860), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fpt.edu.vn", "Huy Lê", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5861), 14 },
                    { 23, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5863), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fpt.edu.vn", "Tuấn Phạm", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5863), 15 },
                    { 24, "Đà Nẵng", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5865), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fpt.edu.vn", "Ngọc Đình", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5866), 16 },
                    { 25, "Nha Trang", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5868), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fpt.edu.vn", "Minh Thảo", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5868), 17 },
                    { 26, "Cần Thơ", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5870), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fpt.edu.vn", "Bình Lê", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5870), 18 },
                    { 27, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5872), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fpt.edu.vn", "Lan Nguyễn", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5873), 19 },
                    { 28, "Huế", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5875), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fpt.edu.vn", "Dương Khoa", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5875), 20 },
                    { 29, "TP Hồ Chí Minh", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5877), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fpt.edu.vn", "Phương Linh", "Female", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5878), 21 },
                    { 30, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5879), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fpt.edu.vn", "Tuấn Lê", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5880), 29 },
                    { 31, "Hà Nội", new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5882), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fpt.edu.vn", "Tùng Khoa", "Male", true, "0123456789", null, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5882), 31 }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5393), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5395), 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5396) },
                    { 2, 3, 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5398), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5399), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5399) },
                    { 3, 3, 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5400), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5401), 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5402) },
                    { 4, 3, 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5403), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5403), 11, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5404) },
                    { 5, 3, 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5405), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5406), 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5406) },
                    { 6, 3, 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5407), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5408), 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5408) },
                    { 7, 3, 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5410), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5410), 14, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5411) },
                    { 8, 4, 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5412), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5413), 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5414) },
                    { 9, 4, 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5415), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5416), 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5416) },
                    { 10, 4, 7, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5417), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5418), 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5419) },
                    { 11, 4, 27, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5420), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5420), 11, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5421) },
                    { 12, 4, 27, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5422), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5422), 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5423) },
                    { 13, 4, 27, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5424), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5425), 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5425) },
                    { 14, 4, 27, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5426), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5427), 14, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5427) }
                });

            migrationBuilder.InsertData(
                table: "SemesterCampusUserSubject",
                columns: new[] { "CampusUserSubjectId", "SemesterId", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5678), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5679) },
                    { 2, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5680), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5680) },
                    { 3, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5681), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5682) },
                    { 4, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5683), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5683) },
                    { 5, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5684), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5685) },
                    { 11, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5686), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5686) },
                    { 12, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5687), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5687) },
                    { 21, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5688), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5689) },
                    { 31, 1, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5689), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5690) },
                    { 6, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5691), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5691) },
                    { 7, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5692), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5692) },
                    { 8, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5693), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5693) },
                    { 9, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5694), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5695) },
                    { 10, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5695), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5696) },
                    { 16, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5697), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5697) },
                    { 17, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5698), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5698) },
                    { 26, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5699), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5700) },
                    { 36, 2, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5700), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5701) },
                    { 4, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5702), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5702) },
                    { 5, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5703), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5703) },
                    { 14, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5704), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5704) },
                    { 15, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5705), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5706) },
                    { 24, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5707), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5707) },
                    { 25, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5708), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5708) },
                    { 34, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5709), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5709) },
                    { 35, 3, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5711), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5711) },
                    { 1, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5712), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5712) },
                    { 2, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5713), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5713) },
                    { 3, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5714), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5715) },
                    { 4, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5716), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5716) },
                    { 5, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5717), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5717) },
                    { 11, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5718), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5718) },
                    { 12, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5719), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5720) },
                    { 21, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5720), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5721) },
                    { 31, 4, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5722), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5722) },
                    { 1, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5723), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5723) },
                    { 2, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5724), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5724) },
                    { 3, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5725), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5726) },
                    { 4, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5726), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5727) },
                    { 5, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5728), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5728) },
                    { 41, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5729), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5729) },
                    { 42, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5730), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5730) },
                    { 51, 5, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5731), new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5731) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5582), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5583) },
                    { 2, 9, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5586), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5587) },
                    { 3, 11, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5588), 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5589) },
                    { 4, 12, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5590), 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5591) },
                    { 5, 13, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5592), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5593) },
                    { 6, 14, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5594), 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 17, 19, 16, 28, 932, DateTimeKind.Local).AddTicks(5595) }
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
                name: "UQ__UserDeta__1788CC4DC8E24AA8",
                table: "UserDetail",
                column: "UserId",
                unique: true);

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
                name: "UserDetail");

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

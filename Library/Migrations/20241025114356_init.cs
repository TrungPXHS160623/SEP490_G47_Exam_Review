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
                    { 1, "Ha Noi", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3328), null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3348) },
                    { 2, "Da Nang", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3351), null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3352) },
                    { 3, "Can Tho", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3355), null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3356) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3359), null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3360) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3362), null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3363) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3772), "Unassigned", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3774) },
                    { 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3778), "Pending", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3779) },
                    { 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3782), "Assigned", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3783) },
                    { 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3786), "Reviewed", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3787) },
                    { 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3789), "Erroneous", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3790) },
                    { 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3793), "Faultless", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3795) },
                    { 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3800), "Completed", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3801) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5153), null, "/usermanagement", "User Management", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5155) },
                    { 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5159), null, "/Admin/History", "User Log", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5160) },
                    { 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5162), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5163) },
                    { 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5165), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5166) },
                    { 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5168), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5169) },
                    { 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5171), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5172) },
                    { 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5174), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5175) },
                    { 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5180), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5181) },
                    { 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5196), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5197) },
                    { 10, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5177), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5178) },
                    { 11, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5183), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5184) },
                    { 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5186), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5187) },
                    { 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5189), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5190) },
                    { 14, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5192), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5193) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5401), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5402) },
                    { 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5407), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5407) },
                    { 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5410), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5411) },
                    { 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5414), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5415) },
                    { 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5418), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5419) },
                    { 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5422), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5423) },
                    { 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5426), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5426) },
                    { 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5430), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5430) },
                    { 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5434), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5434) },
                    { 10, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5438), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5439) },
                    { 11, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5442), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5442) },
                    { 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5503), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5504) },
                    { 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5507), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5508) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4245), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4246) },
                    { 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4249), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4250) },
                    { 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4252), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4253) },
                    { 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4257), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4257) },
                    { 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4260), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4261) },
                    { 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4263), null, "ENM401", "Business English", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4264) },
                    { 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4266), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4267) },
                    { 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4271), null, "ECO201", "International Economics", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4272) },
                    { 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4382), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4384) },
                    { 10, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4387), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4388) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3848), "Admin", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3850) },
                    { 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3852), "Examiner", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3854) },
                    { 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3856), "Lecturer", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3857) },
                    { 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3859), "Head of Department", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3860) },
                    { 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3863), "Curriculum Development", new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3864) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5244), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5245) },
                    { 2, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5247), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5248) },
                    { 8, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5250), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5251) },
                    { 9, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5253), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5254) },
                    { 14, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5256), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5257) },
                    { 3, 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5258), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5259) },
                    { 10, 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5261), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5262) },
                    { 11, 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5272), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5273) },
                    { 13, 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5275), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5276) },
                    { 5, 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5281), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5282) },
                    { 4, 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5264), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5265) },
                    { 6, 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5267), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5267) },
                    { 7, 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5269), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5270) },
                    { 12, 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5279), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5280) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3920), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3921) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3931), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3932) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3938), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3939) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3945), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3946) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3956), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3957) },
                    { 6, "Huế", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3964), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3965) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3976), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3977) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3982), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3983) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3988), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3989) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3994), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3995) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4002), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4003) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4065), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4066) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4071), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4072) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4077), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4078) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4085), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4086) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4091), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4092) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4098), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4099) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4104), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4105) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4110), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4111) },
                    { 20, "Huế", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4116), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4117) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4123), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4124) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4144), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4145) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4150), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4151) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4156), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4158) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4163), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4164) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4169), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4170) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4051), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4052) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3970), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(3971) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4129), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4130) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4058), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4059) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4137), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4138) }
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
                    { 1, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4732), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4731), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4729), "PRN211_Q1_10_123456", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 5, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4730), 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4734) },
                    { 2, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4744), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4743), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4741), "PRN211_Q2_5_654321", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 5, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4742), 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4745) },
                    { 3, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4752), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4751), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4749), "PRN221_Q1_10_789012", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 6, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4750), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4753) },
                    { 4, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4759), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4758), null, "PRN221_Q2_5_210987", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4757), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4760) },
                    { 5, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4766), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4765), null, "PRN231_Q1_10_345678", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4764), 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4767) },
                    { 6, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4775), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4774), null, "PRN231_Q2_5_876543", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4773), 3, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4776) },
                    { 7, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4782), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4781), null, "MAE101_Q1_10_234567", new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4780), 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4783) },
                    { 8, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4790), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4788), null, "MAE101_Q2_5_765432", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4787), 4, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4791) },
                    { 9, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4797), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4796), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4795), 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4798) },
                    { 10, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4805), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4804), null, "NWC203c_Q2_5_876543", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4803), 5, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4806) },
                    { 11, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4813), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4812), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4810), "ENM401_Q1_10_111222", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4811), 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4814) },
                    { 12, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4821), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4820), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4818), "ENM401_Q2_5_222111", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Reading", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4819), 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4823) },
                    { 13, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4830), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4829), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4827), "ENM401_Q3_7_222333", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Writing", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4828), 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4831) },
                    { 14, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4839), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4838), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4835), "ENM401_Q4_9_333111", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 7, "Listening", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4837), 6, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4839) },
                    { 15, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4846), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4845), null, "ECO121_Q1_10_333444", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4844), 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4847) },
                    { 16, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4853), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4852), null, "ECO121_Q2_5_444333", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4851), 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4854) },
                    { 17, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4860), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4859), null, "ECO201_Q1_10_555666", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4858), 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4861) },
                    { 18, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4868), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4866), null, "ECO201_Q2_5_666555", new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4866), 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4869) },
                    { 19, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4875), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4875), null, "ACC101_Q1_10_777888", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4874), 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4876) },
                    { 20, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4883), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4882), null, "ACC101_Q2_5_888777", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4881), 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4883) },
                    { 21, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4890), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4889), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 10 (10 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4888), 10, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4891) },
                    { 22, 1, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4898), 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4897), null, "MKT101_Q2_5_000999", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Block 5 (5 weeks)", 1, "Multiple Choice", 2, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4896), 10, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4899) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "ExamTestDuration", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4973), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4977), 1, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4979) },
                    { 2, 3, 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4982), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4983), 2, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4984) },
                    { 3, 3, 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4987), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4988), 3, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4989) },
                    { 4, 3, 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4991), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4993), 11, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4994) },
                    { 5, 3, 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4996), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4998), 12, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(4998) },
                    { 6, 3, 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5001), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5002), 13, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5003) },
                    { 7, 3, 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5005), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5007), 14, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5008) },
                    { 8, 4, 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5010), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5012), 1, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5013) },
                    { 9, 4, 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5016), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5017), 2, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5018) },
                    { 10, 4, 7, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5020), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5022), 3, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5023) },
                    { 11, 4, 27, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5081), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5083), 11, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5084) },
                    { 12, 4, 27, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5086), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5088), 12, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5089) },
                    { 13, 4, 27, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5091), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5092), 13, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5093) },
                    { 14, 4, 27, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5095), new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5097), 14, null, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5098) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "FileData", "FileName", "FileSize", "FileType", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5330), null, null, null, null, 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5331) },
                    { 2, 9, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5336), null, null, null, null, 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5337) },
                    { 3, 11, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5340), null, null, null, null, 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5341) },
                    { 4, 12, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5343), null, null, null, null, 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5344) },
                    { 5, 13, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5348), null, null, null, null, 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5349) },
                    { 6, 14, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5351), null, null, null, null, 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 25, 18, 43, 53, 883, DateTimeKind.Local).AddTicks(5352) }
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

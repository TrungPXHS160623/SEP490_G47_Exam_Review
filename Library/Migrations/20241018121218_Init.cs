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
                    { 1, "Ha Noi", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7834), null, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7846) },
                    { 2, "Da Nang", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7849), null, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7849) },
                    { 3, "Can Tho", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7851), null, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7851) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7893), null, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7894) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7895), null, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(7896) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8037), "Not Assign", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8039) },
                    { 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8040), "Waiting To Assign", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8041) },
                    { 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8042), "Assigned", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8043) },
                    { 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8044), "Reviewing", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8045) },
                    { 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8046), "Exam With Errors", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8047) },
                    { 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8048), "Faultless Exam", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8048) },
                    { 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8050), "Complete", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8050) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8709), null, "/usermanagement", "User Management", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8710) },
                    { 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8712), null, "/Admin/History", "User Log", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8712) },
                    { 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8714), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8714) },
                    { 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8715), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8716) },
                    { 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8717), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8717) },
                    { 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8718), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8719) },
                    { 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8720), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8720) },
                    { 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8761), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8761) },
                    { 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8768), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8768) },
                    { 10, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8759), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8759) },
                    { 11, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8762), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8763) },
                    { 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8764), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8764) },
                    { 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8765), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8767) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8886), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2020", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8886) },
                    { 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8889), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2021", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8890) },
                    { 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8891), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2021", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8892) },
                    { 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8893), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2021", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8894) },
                    { 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8896), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2022", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8896) },
                    { 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8898), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2022", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8898) },
                    { 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8900), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2022", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8900) },
                    { 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8901), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2023", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8902) },
                    { 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8903), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2023", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8904) },
                    { 10, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8905), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2023", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8906) },
                    { 11, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8907), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Spring2024", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8908) },
                    { 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8909), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Summer2024", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8910) },
                    { 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8912), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fall2024", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8913) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8273), null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8273) },
                    { 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8275), null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8276) },
                    { 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8277), null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8277) },
                    { 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8278), null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8279) },
                    { 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8280), null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8281) },
                    { 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8282), null, "ENM401", "Business English", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8283) },
                    { 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8284), null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8284) },
                    { 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8285), null, "ECO201", "International Economics", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8286) },
                    { 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8287), null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8288) },
                    { 10, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8289), null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8289) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8075), "Admin", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8077) },
                    { 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8078), "Examiner", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8079) },
                    { 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8080), "Lecturer", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8081) },
                    { 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8082), "Head of Department", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8083) },
                    { 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8084), "Curriculum Development", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8084) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8796), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8800) },
                    { 2, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8802), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8802) },
                    { 8, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8803), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8804) },
                    { 9, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8805), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8805) },
                    { 3, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8806), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8806) },
                    { 10, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8807), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8808) },
                    { 11, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8813), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8813) },
                    { 13, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8814), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8814) },
                    { 5, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8817), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8818) },
                    { 4, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8808), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8809) },
                    { 6, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8810), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8810) },
                    { 7, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8811), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8812) },
                    { 12, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8816), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8816) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8114), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8115) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8118), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8119) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8122), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8123) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8126), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8126) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8129), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8130) },
                    { 6, "Huế", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8132), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8133) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8139), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fpt.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8140) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8143), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fpt.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8143) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8145), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fpt.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8146) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8149), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fpt.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8149) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8153), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fpt.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8153) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8179), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fpt.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8180) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8182), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fpt.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8183) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8186), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fpt.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8186) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8190), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fpt.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8190) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8193), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fpt.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8193) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8196), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fpt.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8196) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8199), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fpt.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8199) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8202), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fpt.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8202) },
                    { 20, "Huế", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8205), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fpt.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8205) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8209), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fpt.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8210) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8219), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8219) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8222), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8222) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8225), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8226) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8229), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8229) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8232), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8232) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8156), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fpt.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8156) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8136), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8137) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8212), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fpt.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8213) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8159), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fpt.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8159) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8215), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fpt.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8216) }
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
                    { 1, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8484), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8483), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8480), "PRN211_Q1_10_123456", "Block 10 (10 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8482), 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8488) },
                    { 2, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8492), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8492), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8490), "PRN211_Q2_5_654321", "Block 5 (5 weeks)", 5, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8491), 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8493) },
                    { 3, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8496), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8495), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8494), "PRN221_Q1_10_789012", "Block 10 (10 weeks)", 6, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8495), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8496) },
                    { 4, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8499), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8499), null, "PRN221_Q2_5_210987", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8498), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8500) },
                    { 5, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8503), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8502), null, "PRN231_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8502), 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8503) },
                    { 6, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8548), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8548), null, "PRN231_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8547), 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8549) },
                    { 7, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8552), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8551), null, "MAE101_Q1_10_234567", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8551), 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8552) },
                    { 8, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8555), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8555), null, "MAE101_Q2_5_765432", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8554), 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8556) },
                    { 9, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8558), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8558), null, "NWC203c_Q1_10_345678", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8558), 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8559) },
                    { 10, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8562), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8561), null, "NWC203c_Q2_5_876543", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8561), 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8562) },
                    { 11, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8569), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8568), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8567), "ENM401_Q1_10_111222", "Block 10 (10 weeks)", 7, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8568), 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8569) },
                    { 12, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8572), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8572), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8571), "ENM401_Q2_5_222111", "Block 10 (10 weeks)", 7, "Reading", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8571), 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8573) },
                    { 13, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8576), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8576), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8575), "ENM401_Q3_7_222333", "Block 10 (10 weeks)", 7, "Writing", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8575), 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8577) },
                    { 14, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8580), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8580), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8579), "ENM401_Q4_9_333111", "Block 10 (10 weeks)", 7, "Listening", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8579), 6, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8581) },
                    { 15, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8584), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8584), null, "ECO121_Q1_10_333444", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8583), 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8585) },
                    { 16, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8588), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8588), null, "ECO121_Q2_5_444333", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8587), 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8589) },
                    { 17, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8591), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8591), null, "ECO201_Q1_10_555666", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8590), 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8592) },
                    { 18, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8595), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8594), null, "ECO201_Q2_5_666555", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8594), 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8595) },
                    { 19, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8598), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8598), null, "ACC101_Q1_10_777888", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8597), 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8599) },
                    { 20, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8602), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8601), null, "ACC101_Q2_5_888777", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8601), 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8602) },
                    { 21, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8606), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8605), null, "MKT101_Q1_10_999000", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8605), 10, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8606) },
                    { 22, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8609), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8608), null, "MKT101_Q2_5_000999", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8608), 10, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8610) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8645), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8646), 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8647) },
                    { 2, 3, 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8649), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8650), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8650) },
                    { 3, 3, 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8651), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8652), 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8653) },
                    { 4, 3, 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8654), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8655), 11, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8655) },
                    { 5, 3, 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8656), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8657), 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8658) },
                    { 6, 3, 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8659), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8660), 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8661) },
                    { 7, 3, 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8662), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8663), 14, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8663) },
                    { 8, 4, 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8664), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8665), 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8666) },
                    { 9, 4, 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8667), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8668), 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8669) },
                    { 10, 4, 7, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8670), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8670), 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8671) },
                    { 11, 4, 27, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8672), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8673), 11, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8673) },
                    { 12, 4, 27, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8674), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8675), 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8676) },
                    { 13, 4, 27, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8677), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8678), 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8679) },
                    { 14, 4, 27, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8680), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8681), 14, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8682) }
                });

            migrationBuilder.InsertData(
                table: "SemesterCampusUserSubject",
                columns: new[] { "CampusUserSubjectId", "SemesterId", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8938), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8939) },
                    { 2, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8941), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8941) },
                    { 3, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8942), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8942) },
                    { 4, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8943), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8944) },
                    { 5, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8945), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8945) },
                    { 11, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8946), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8947) },
                    { 12, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8947), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8948) },
                    { 21, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8949), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8949) },
                    { 31, 1, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8950), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8950) },
                    { 6, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8951), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8952) },
                    { 7, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8952), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8953) },
                    { 8, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8954), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8954) },
                    { 9, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8955), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8956) },
                    { 10, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8957), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8957) },
                    { 16, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8958), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8959) },
                    { 17, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8959), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8960) },
                    { 26, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8961), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8961) },
                    { 36, 2, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8962), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8962) },
                    { 4, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8963), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8963) },
                    { 5, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8965), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8965) },
                    { 14, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8966), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8967) },
                    { 15, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8967), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8968) },
                    { 24, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8969), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8969) },
                    { 25, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8970), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8970) },
                    { 34, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8971), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8972) },
                    { 35, 3, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8973), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8973) },
                    { 1, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8995), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8996) },
                    { 2, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8997), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8997) },
                    { 3, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8998), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8998) },
                    { 4, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8999), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9000) },
                    { 5, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9001), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9001) },
                    { 11, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9002), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9002) },
                    { 12, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9003), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9003) },
                    { 21, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9004), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9005) },
                    { 31, 4, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9005), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9006) },
                    { 1, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9007), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9007) },
                    { 2, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9008), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9008) },
                    { 3, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9009), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9009) },
                    { 4, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9010), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9011) },
                    { 5, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9011), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9012) },
                    { 41, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9013), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9013) },
                    { 42, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9014), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9014) },
                    { 51, 5, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(9016) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8843), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", 8f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8843) },
                    { 2, 9, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8845), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", 9f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8846) },
                    { 3, 11, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8847), 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", 9f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8848) },
                    { 4, 12, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8850), 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", 8f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8850) },
                    { 5, 13, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8852), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", 8f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8853) },
                    { 6, 14, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8855), 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", 8f, new DateTime(2024, 10, 18, 19, 12, 17, 510, DateTimeKind.Local).AddTicks(8856) }
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

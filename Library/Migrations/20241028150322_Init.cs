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
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeanId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                    table.ForeignKey(
                        name: "FK_Faculties_Users_DeanId",
                        column: x => x.DeanId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
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
                    TermDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "InstructorAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignStatusId = table.Column<int>(type: "int", nullable: true),
                    GeneralFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ReportFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_ReportFiles_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "CampusName", "CreateDate", "IsDeleted", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6773), null, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6788) },
                    { 2, "Da Nang", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6790), null, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6790) },
                    { 3, "Can Tho", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6792), null, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6792) },
                    { 4, "Ho Chi Minh", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6794), null, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6794) },
                    { 5, "Quy Nhon", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6795), null, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(6796) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7042), "Unassigned", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7044) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7046), "Pending", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7046) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7048), "Assigned", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7048) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7049), "Reviewed", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7050) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7051), "Erroneous", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7052) },
                    { 6, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7053), "Faultless", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7054) },
                    { 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7056), "Completed", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7056) }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "CreateDate", "DeanId", "Description", "FacultyName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7159), null, "Specializes in training related to information technology and software.", "Faculty of Information Technology", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7160) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7163), null, "Specializes in training in economics, business administration, and finance.", "Faculty of Economics", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7164) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7166), null, "Trains in language, culture, and international communication.", "Faculty of Foreign Languages", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7167) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7169), null, "Specializes in engineering, electronics, and mechanics.", "Faculty of Engineering", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7169) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7171), null, "Trains in management, leadership, and organization.", "Faculty of Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7171) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7853), null, "/usermanagement", "User Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7854) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7855), null, "/Admin/History", "User Log", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7855) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7856), null, "/Examiner/ExamList", "Exam List", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7857) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7858), null, "/HeadDepartment/ExamList", "Exam Assign", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7859) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7860), null, "/Lecture/ExamList", "List Asigned", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7861) },
                    { 6, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7862), null, "/HeadDepartment/Report", "View Report", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7863) },
                    { 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7864), null, "/HeadDepartment/ExamStatus", "Exam Status", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7865) },
                    { 8, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7867), null, "/Admin/CampusManagement", "Campus Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7868) },
                    { 9, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7877), null, "/Admin/SubjectManagement", "Subject Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7877) },
                    { 10, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7866), null, "/Examiner/usermanagement", "Head Department Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7866) },
                    { 11, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7869), null, "/Examiner/Create", "Create Exam", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7869) },
                    { 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7870), null, "/HeadDepartment/lectureManagement", "Lecture Management(UnderContrucst)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7871) },
                    { 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7873), null, "/Examiner/Statistical", "Statistical", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7874) },
                    { 14, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7875), null, "/Admin/SemesterManagement", "Semester Management", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7876) }
                });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "SemesterId", "CreatedDate", "EndDate", "IsActive", "SemesterName", "StartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8005), new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa21", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8006) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8008), new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp21", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8008) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8010), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su21", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8010) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8012), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa21", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8012) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8014), new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp22", new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8014) },
                    { 6, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8016), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su22", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8016) },
                    { 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8018), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa22", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8018) },
                    { 8, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8021), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp23", new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8021) },
                    { 9, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8023), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su23", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8024) },
                    { 10, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8025), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa23", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8026) },
                    { 11, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8027), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sp24", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8028) },
                    { 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8029), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Su24", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8030) },
                    { 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8031), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fa24", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(8032) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7125), "Admin", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7125) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7127), "Examiner", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7127) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7129), "Lecturer", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7129) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7131), "Head of Department", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7131) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7133), "Curriculum Development", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7133) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7904), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7905) },
                    { 2, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7907), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7909) },
                    { 8, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7910), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7911) },
                    { 9, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7912), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7912) },
                    { 14, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7913), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7914) },
                    { 3, 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7915), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7915) },
                    { 10, 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7916), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7917) },
                    { 11, 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7923), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7924) },
                    { 13, 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7925), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7926) },
                    { 5, 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7928), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7929) },
                    { 4, 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7918), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7919) },
                    { 6, 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7920), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7920) },
                    { 7, 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7921), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7921) },
                    { 12, 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7927), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7927) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "FacultyId", "IsDeleted", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7378), 1, null, "PRN211", "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7379) },
                    { 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7381), 1, null, "PRN221", "Advanced Cross-Platform Application Programming With .NET", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7382) },
                    { 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7383), 1, null, "PRN231", "Building Cross-Platform Back-End Application With .NET", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7384) },
                    { 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7385), 1, null, "MAE101", "Mathematics for Engineering", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7386) },
                    { 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7387), 1, null, "NWC203c", "Computer Networking", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7388) },
                    { 6, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7389), 2, null, "ENM401", "Business English", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7390) },
                    { 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7391), 2, null, "ECO121", "Basic Macro Economics", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7392) },
                    { 8, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7393), 2, null, "ECO201", "International Economics", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7393) },
                    { 9, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7395), 2, null, "ACC101", "Principles of Accounting", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7396) },
                    { 10, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7399), 2, null, "MKT101", "Marketing Principles", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7399) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "CampusId", "CreateDate", "DateOfBirth", "EmailFe", "FullName", "Gender", "IsActive", "Mail", "PhoneNumber", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7201), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, true, "admin@fpt.edu.vn", "0123456789", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7202) },
                    { 2, "TP Hồ Chí Minh", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7207), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lienkt@fe.edu.vn", "Liên Kết", false, true, "lienkt@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7207) },
                    { 3, "Đà Nẵng", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7212), new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoanglm@fe.edu.vn", "Hoàng Lâm", true, true, "hoanglm@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7212) },
                    { 4, "Nha Trang", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7215), new DateTime(1995, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anhnq@fe.edu.vn", "Anh Nguyễn", true, true, "anhnq@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7215) },
                    { 5, "Cần Thơ", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7218), new DateTime(1991, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhnh@fe.edu.vn", "Minh Nhân", true, true, "minhnh@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7219) },
                    { 6, "Huế", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7222), new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phongtl@fe.edu.vn", "Phong Tài", true, true, "phongtl@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7222) },
                    { 7, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7228), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhbt@fe.edu.vn", "Lành Bích", false, true, "lanhbt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7229) },
                    { 8, "Hải Phòng", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7233), new DateTime(1988, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khoadt@fe.edu.vn", "Khoa Đạt", true, true, "khoadt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7233) },
                    { 9, "Đà Nẵng", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7236), new DateTime(1987, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoangtm@fe.edu.vn", "Hoàng Tâm", true, true, "hoangtm@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7237) },
                    { 10, "Nha Trang", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7239), new DateTime(1990, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhph@fe.edu.vn", "Minh Phúc", true, true, "minhph@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7240) },
                    { 11, "Cần Thơ", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7243), new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trangnt@fe.edu.vn", "Trạng Nguyên", false, true, "trangnt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7243) },
                    { 12, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7253), new DateTime(1988, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "namlh@fe.edu.vn", "Nam Lê", true, true, "namlh@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7254) },
                    { 13, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7257), new DateTime(1986, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quangnv@fe.edu.vn", "Quang Nguyễn", true, true, "quangnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7257) },
                    { 14, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7287), new DateTime(1985, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huylt@fe.edu.vn", "Huy Lê", true, true, "huylt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7288) },
                    { 15, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7291), new DateTime(1984, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanpv@fe.edu.vn", "Tuấn Phạm", true, true, "tuanpv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7292) },
                    { 16, "Đà Nẵng", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7294), new DateTime(1987, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngocdt@fe.edu.vn", "Ngọc Đình", false, true, "ngocdt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7295) },
                    { 17, "Nha Trang", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7298), new DateTime(1989, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "minhth@fe.edu.vn", "Minh Thảo", false, true, "minhth@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7298) },
                    { 18, "Cần Thơ", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7302), new DateTime(1990, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhlt@fe.edu.vn", "Bình Lê", true, true, "binhlt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7303) },
                    { 19, "TP Hồ Chí Minh", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7305), new DateTime(1991, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lanhnv@fe.edu.vn", "Lan Nguyễn", false, true, "lanhnv@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7306) },
                    { 20, "Huế", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7309), new DateTime(1993, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duongkt@fe.edu.vn", "Dương Khoa", true, true, "duongkt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7309) },
                    { 21, "TP Hồ Chí Minh", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7312), new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuonglt@fe.edu.vn", "Phương Linh", false, true, "phuonglt@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7313) },
                    { 22, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7322), new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Phúc Đạt", true, true, "phucdt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7323) },
                    { 23, "TP Hồ Chí Minh", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7325), new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thanh Nguyễn", false, true, "thanhnt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7326) },
                    { 24, "Đà Nẵng", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7329), new DateTime(1991, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hùng Phát", true, true, "hungpv@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7331) },
                    { 25, "Nha Trang", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7334), new DateTime(1992, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anh Tùng", true, true, "anhpt@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7335) },
                    { 26, "Cần Thơ", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7337), new DateTime(1993, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trương Vĩnh", true, true, "truongvq@fpt.edu.vn", "0123456789", 5, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7338) },
                    { 27, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7246), new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quanpt@fe.edu.vn", "Quân Phạm", true, true, "quanpt@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7246) },
                    { 28, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7225), new DateTime(1995, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hunglthe160235@fe.edu.vn", "Hưng Lê", true, true, "hunglthe160235@fpt.edu.vn", "0123456789", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7226) },
                    { 29, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7316), new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuanlmhe161245@fe.edu.vn", "Tuấn Lê", true, true, "tuanlmhe161245@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7316) },
                    { 30, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7249), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trungpxhs160623@fe.edu.vn", "Trung Phạm", true, true, "trungpxhs160623@fpt.edu.vn", "0123456789", 3, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7250) },
                    { 31, "Hà Nội", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7319), new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungtkHS163077@fe.edu.vn", "Tùng Khoa", true, true, "tungtkHS163077@fpt.edu.vn", "0123456789", 4, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7320) }
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
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDate", "ExamDuration", "ExamStatusId", "ExamType", "SemesterId", "StartDate", "SubjectId", "TermDuration", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7583), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7578), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7577), "PRN211_Q1_10_123456", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 5, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7578), 1, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7584) },
                    { 2, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7589), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7589), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7587), "PRN211_Q2_5_654321", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 5, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7588), 1, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7590) },
                    { 3, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7619), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7618), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7617), "PRN221_Q1_10_789012", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 6, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7618), 2, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7620) },
                    { 4, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7625), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7624), null, "PRN221_Q2_5_210987", new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7624), 2, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7626) },
                    { 5, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7630), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7629), null, "PRN231_Q1_10_345678", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7629), 3, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7631) },
                    { 6, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7634), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7633), null, "PRN231_Q2_5_876543", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7633), 3, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7635) },
                    { 7, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7640), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7639), null, "MAE101_Q1_10_234567", new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7638), 4, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7640) },
                    { 8, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7646), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7645), null, "MAE101_Q2_5_765432", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7644), 4, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7646) },
                    { 9, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7650), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7650), null, "NWC203c_Q1_10_345678", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7649), 5, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7651) },
                    { 10, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7655), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7654), null, "NWC203c_Q2_5_876543", new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7653), 5, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7655) },
                    { 11, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7667), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7666), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7664), "ENM401_Q1_10_111222", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 7, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7664), 6, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7667) },
                    { 12, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7671), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7670), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7669), "ENM401_Q2_5_222111", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 7, "Reading", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7670), 6, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7671) },
                    { 13, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7676), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7675), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7674), "ENM401_Q3_7_222333", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 7, "Writing", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7675), 6, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7676) },
                    { 14, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7680), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7680), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7679), "ENM401_Q4_9_333111", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 7, "Listening", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7679), 6, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7681) },
                    { 15, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7685), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7685), null, "ECO121_Q1_10_333444", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7683), 7, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7685) },
                    { 16, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7690), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7689), null, "ECO121_Q2_5_444333", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7688), 7, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7690) },
                    { 17, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7693), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7693), null, "ECO201_Q1_10_555666", new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7692), 8, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7694) },
                    { 18, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7697), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7697), null, "ECO201_Q2_5_666555", new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7696), 8, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7698) },
                    { 19, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7702), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7701), null, "ACC101_Q1_10_777888", new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7700), 9, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7702) },
                    { 20, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7707), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7706), null, "ACC101_Q2_5_888777", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7706), 9, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7707) },
                    { 21, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7710), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7710), null, "MKT101_Q1_10_999000", new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "90", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7709), 10, "Block 10 (10 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7711) },
                    { 22, 1, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7715), 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7714), null, "MKT101_Q2_5_000999", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "60", 1, "Other", 2, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7714), 10, "Block 5 (5 weeks)", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7715) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignStatusId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "ExamTestDuration", "GeneralFeedback", "IsReady", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7756), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7759), 1, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7759) },
                    { 2, 3, 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7761), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7761), 2, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7762) },
                    { 3, 3, 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7763), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7764), 3, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7764) },
                    { 4, 3, 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7766), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7767), 11, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7767) },
                    { 5, 3, 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7768), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7769), 12, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7770) },
                    { 6, 3, 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7771), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7772), 13, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7772) },
                    { 7, 3, 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7774), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7774), 14, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7775) },
                    { 8, 4, 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7776), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7777), 1, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7777) },
                    { 9, 4, 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7778), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7779), 2, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7780) },
                    { 10, 4, 7, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7782), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7783), 3, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7783) },
                    { 11, 4, 27, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7784), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7785), 11, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7786) },
                    { 12, 4, 27, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7787), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7787), 12, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7788) },
                    { 13, 4, 27, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7814), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7815), 13, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7816) },
                    { 14, 4, 27, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7817), new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7818), 14, null, null, false, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7818) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "AssignmentId", "CreateDate", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7962), 1, "Correct the code snippet by replacing 'Console.Writeline' with 'Console.WriteLine'.", "In PRN211, question 1 contains an incorrect code snippet that causes compilation errors.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7963) },
                    { 2, 9, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7965), 2, "Revise the logic to ensure it follows the proper algorithmic steps.", "In PRN211, question 2 has an outdated logic that leads to incorrect output.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7965) },
                    { 3, 11, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7970), 3, "Provide a more detailed explanation of how supply and demand interact in a market.", "In ENM401, question 1 fails to explain the principle of supply and demand adequately.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7972) },
                    { 4, 12, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7973), 4, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 2 has an error in the calculation of equilibrium price.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7974) },
                    { 5, 13, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7976), 5, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 3 has an error in the calculation.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7976) },
                    { 6, 14, new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7977), 6, "Revise the calculation method to correctly reflect the intersection of supply and demand curves.", "In ENM401, question 4 has an error.", new DateTime(2024, 10, 28, 22, 3, 21, 859, DateTimeKind.Local).AddTicks(7978) }
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
                name: "IX_Faculties_DeanId",
                table: "Faculties",
                column: "DeanId");

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
                name: "IX_ReportFiles_ReportId",
                table: "ReportFiles",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AssignmentId",
                table: "Reports",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacultyId",
                table: "Subjects",
                column: "FacultyId");

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
                name: "ReportFiles");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Reports");

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
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

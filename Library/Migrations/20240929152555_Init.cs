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
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                    { 1, "Hòa Lạc", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8275), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8288) },
                    { 2, "Đà Nẵng", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8290), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8291) },
                    { 3, "Cần Thơ", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8292), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8293) },
                    { 4, "Hồ Chí Minh", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8295), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8295) }
                });

            migrationBuilder.InsertData(
                table: "ExamStatuses",
                columns: new[] { "ExamStatusId", "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8418), "Not Assign", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8420) },
                    { 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8421), "Waiting to Assign", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8422) },
                    { 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8423), "Assigned", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8423) },
                    { 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8424), "Reviewing", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8425) },
                    { 5, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8426), "Finish Review", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8427) },
                    { 6, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8428), "Complete", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8428) }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "CreateDate", "IsProgram", "MenuLink", "MenuName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8716), null, "/usermanagement", "User management", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8716) },
                    { 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8718), null, "/Admin/History", "History", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8718) },
                    { 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8720), null, "/TestDepartment/ExamList", "Exam List", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8720) },
                    { 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8721), null, "/HeadDepartment/ExamList", "Head Department List", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8722) },
                    { 5, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8723), null, "/Lecture/ExamList", "Lecture List", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8723) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "CreateDate", "SubjectCode", "SubjectName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8597), "PRN231", "C# Programming", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8599) },
                    { 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8600), "CSI123", "Computer Science", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8601) },
                    { 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8602), "MLN123", "Machine Learning", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8603) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "CreateDate", "RoleName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8454), "Admin", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8455) },
                    { 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8457), "Examiner", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8458) },
                    { 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8459), "Lecturer", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8460) },
                    { 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8461), "Head of Department", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8461) },
                    { 5, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8463), "Program Developer", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8463) }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId", "CreateDate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8748), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8749) },
                    { 2, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8750), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8751) },
                    { 3, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8752), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8752) },
                    { 4, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8753), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8754) },
                    { 5, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8755), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8755) },
                    { 3, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8756), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8756) },
                    { 4, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8757), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8758) },
                    { 5, 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8759), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8759) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CampusId", "CreateDate", "IsActive", "Mail", "RoleId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8490), true, "admin@fpt.edu.vn", 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8491) },
                    { 2, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8493), true, "examiner@fpt.edu.vn", 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8494) },
                    { 3, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8496), true, "lecturer1@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8497) },
                    { 4, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8499), true, "lecturer2@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8499) },
                    { 5, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8502), true, "lecturer3@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8502) },
                    { 6, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8504), true, "lecturer4@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8505) },
                    { 7, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8507), true, "lecturer5@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8508) },
                    { 8, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8510), true, "lecturer6@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8511) },
                    { 9, 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8513), true, "lecturer7@fpt.edu.vn", 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8514) },
                    { 10, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8516), true, "head@fpt.edu.vn", 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8516) },
                    { 11, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8518), true, "developer@fpt.edu.vn", 5, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8519) },
                    { 12, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8521), true, "trunghp@fpt.edu.vn", 4, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8521) }
                });

            migrationBuilder.InsertData(
                table: "CampusUserSubject",
                columns: new[] { "id", "CampusId", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 2, 2, 1, 5 },
                    { 3, 3, 1, 7 },
                    { 4, 4, 1, 9 },
                    { 5, 1, 2, 3 },
                    { 6, 2, 2, 5 },
                    { 7, 3, 2, 7 },
                    { 8, 4, 2, 9 },
                    { 9, 2, 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "ExamId", "CampusId", "CreateDate", "CreaterId", "EndDate", "EstimatedTimeTest", "ExamCode", "ExamDuration", "ExamStatusId", "ExamType", "StartDate", "SubjectId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8658), 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8658), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8656), "EXAM001", "Block 10 (10 weeks)", 1, "Writing", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8657), 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8659) },
                    { 2, 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8663), 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8663), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8661), "EXAM002", "Block 10 (10 weeks)", 1, "Multiple Choice", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8662), 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8664) },
                    { 3, 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8668), 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8667), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8666), "EXAM003", "Block 5 (5 weeks)", 1, "Multiple Choice", new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8666), 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8668) }
                });

            migrationBuilder.InsertData(
                table: "InstructorAssignments",
                columns: new[] { "AssignmentId", "AssignedUserId", "AssignmentDate", "CreateDate", "ExamId", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8692), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8693), 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8694) },
                    { 2, 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8695), new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8696), 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8696) }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReviewId", "CreateDate", "ExamId", "QuestionNumber", "QuestionSolutionDetail", "ReportContent", "Score", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8782), 1, 1, "Solution explanation 1", "Report 1", 90f, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8783), 3 },
                    { 2, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8785), 2, 2, "Solution explanation 2", "Report 2", 85f, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8786), 3 },
                    { 3, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8787), 3, 3, "Solution explanation 3", "Report 3", 75f, new DateTime(2024, 9, 29, 22, 25, 55, 646, DateTimeKind.Local).AddTicks(8788), 3 }
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

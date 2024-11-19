using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class Innit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Campuses",
                table: "CampusUserFaculty");

            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Faculties",
                table: "CampusUserFaculty");

            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Users",
                table: "CampusUserFaculty");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CampusId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4564));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4567));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4570));

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1362), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1389) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1398), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1399) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1403), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1409), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1415), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(1416) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2251), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2258) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2262), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2263) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 3,
                columns: new[] { "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2266), "Awaiting", new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2268) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2271), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2272) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2274), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2276) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2298), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2304) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2307), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2312) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2325), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2326) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 1,
                columns: new[] { "CreateDate", "ExamStatusId", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4668), 5, new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4671) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4682), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4684) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4724), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4725) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4735), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4736) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4747), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4749) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4758), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4759) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 7,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4770), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4769), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4767), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4772) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 8,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4884), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4882), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4880), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4885) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4893), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4895) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4905), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4906) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4916), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4918) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4928), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4929) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4937), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4938) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4948), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4949) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4958), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4969), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4971) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4980), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4982) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4992), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4994) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 19,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5002), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5001), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5000), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5004) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 20,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5013), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5012), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5010), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5015) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 21,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5025), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5024), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5022), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5026) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 22,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5036), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5035), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5033), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5037) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2427), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2429) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2434), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2435) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2439), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2444), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2445) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2448), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2451) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2454), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2455) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2459), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2463), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2464) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2468), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2469) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2473), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2474) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2603), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2605) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2608), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2609) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2612), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2613) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2617), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2618) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2621), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2622) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2626), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2628) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2631), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2632) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2635), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2636) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 19,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2640), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2641) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 20,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2644), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2645) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 21,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2648), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 22,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2653), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2654) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 23,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2657), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2658) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 24,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2662), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2663) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 25,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2667), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2668) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 26,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2671), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2672) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 27,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2676), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2677) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 28,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2680), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2681) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 29,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2684), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2685) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 30,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2688), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2689) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5478), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5484), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5486) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 8, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5488), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5490) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 9, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5560), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5562) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 14, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5565), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5566) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 3, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5568), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 10, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5573), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5575) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 11, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5593), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5596) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 13, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5600), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5603) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 5, 3 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5613), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5616) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5577), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5578) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 6, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5580), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5583) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 7, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5587), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5589) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 12, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5607), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5609) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5302), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5313) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5317), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5318) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5321), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5323) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5325), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5326) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5329), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5333), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5335) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 7,
                columns: new[] { "CreateDate", "MenuName", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5337), "Exam Status(UnderContrucst)", new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5339) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5347), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5348) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5367), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5368) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5341), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5344) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5350), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5351) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 12,
                columns: new[] { "CreateDate", "MenuName", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5354), "Lecture Management", new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5355) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5358), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5359) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5362), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5363) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5810), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5816) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5822), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5823) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5827), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5828) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5832), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5833) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5837), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5839) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5843), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5942), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5945) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5950), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5952) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5956), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(5957) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3407), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3410) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3415), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3417) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3421), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3422) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3425), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3431), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3437), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3438) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3442), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3445) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3448), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3449) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3453), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3454) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3457), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3458) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3462), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3463) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3466), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3467) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3470), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3474) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3479), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3482) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3487), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3497), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3499) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3506), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3509) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3514), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3517) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 19,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3523), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3526) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 20,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3716), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3723) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 21,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3728), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 22,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3733), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3734) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 23,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3737), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3739) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 24,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3742), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3743) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 25,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3747), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3748) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 26,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3753), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3754) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 27,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3758), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3759) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 28,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3763), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3764) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 29,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3768), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3769) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 30,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3772), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3773) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 31,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3777), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3778) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 32,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3782), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3783) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 33,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3787), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 34,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3791), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3792) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 35,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3796), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3797) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 36,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3802), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3803) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 37,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3806), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3808) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 38,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3811), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3812) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 39,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3816), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3817) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 40,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3821), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3822) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 41,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3826), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3827) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 42,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3830), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3832) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 43,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3835), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3836) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 44,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3840), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3841) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 45,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3845), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3846) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 46,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3850), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3851) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 47,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3856), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3857) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 48,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3862), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3863) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 49,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3866), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3868) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 50,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3871), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3873) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 51,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3876), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3877) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 52,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3880), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3881) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 53,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3885), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3886) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 54,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3889), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3891) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 55,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3894), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3916) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 56,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3927), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3929) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 57,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3934), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3935) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 58,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3939), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 59,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3944), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3945) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 60,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3948), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3950) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 61,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3953), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3955) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 62,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3958), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3959) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 63,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3963), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3964) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 64,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3967), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3969) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 65,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3972), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3973) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 66,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3977), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3978) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 67,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3982), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3983) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 68,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3987), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3988) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 69,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4084), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4087) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 70,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4091), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 71,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4096), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4098) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 72,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4101), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4103) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 73,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4106), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4108) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 74,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4112), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4113) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 75,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4117), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(4119) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2831), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2835) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2839), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2852), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2854) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2857), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2859) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2862), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2863) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(2998), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3004) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3019), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3020) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3027), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3028) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3035), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3036) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3043), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3044) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3052), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3053) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3158), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3169), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3177), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3178) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3185), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3186) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3194), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3195) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3203), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3205) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3214), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3215) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3221), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3223) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3229), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3237), new DateTime(2024, 11, 15, 20, 36, 4, 934, DateTimeKind.Local).AddTicks(3239) });

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Campuses",
                table: "CampusUserFaculty",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Faculties",
                table: "CampusUserFaculty",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Users",
                table: "CampusUserFaculty",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Campuses",
                table: "CampusUserFaculty");

            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Faculties",
                table: "CampusUserFaculty");

            migrationBuilder.DropForeignKey(
                name: "FK_CampusUserFaculty_Users",
                table: "CampusUserFaculty");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CampusId",
                table: "CampusUserFaculty",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6279));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6283));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6285));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6287));

            migrationBuilder.UpdateData(
                table: "CampusUserFaculty",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6289));

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4431), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4453) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4458), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4459) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4462), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4463) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4467), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4468) });

            migrationBuilder.UpdateData(
                table: "Campuses",
                keyColumn: "CampusId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4611), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(4612) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5015), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5018) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5021), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5022) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 3,
                columns: new[] { "CreateDate", "StatusContent", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5024), "Awaiting Lecturer Confirm", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5026) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5028), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5029) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5031), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5032) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5033), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5035) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5037), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5037) });

            migrationBuilder.UpdateData(
                table: "ExamStatuses",
                keyColumn: "ExamStatusId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5039), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 1,
                columns: new[] { "CreateDate", "ExamStatusId", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6341), 4, new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6342) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6351), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6352) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6368), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6369) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6377), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6378) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6386), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6388) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6396), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6397) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 7,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6404), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6402), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6401), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6405) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 8,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6412), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6411), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6410), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6413) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6419), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6428), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6429) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6437), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6438) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6445), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6446) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6454), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6455) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6853), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6855) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6866), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6866) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6874), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6875) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6882), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6884) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6891), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6892) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 19,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6899), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6898), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6897), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6900) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 20,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6907), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6905), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6904), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6908) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 21,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6915), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6914), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6913), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "ExamId",
                keyValue: 22,
                columns: new[] { "CreateDate", "EndDate", "StartDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6984), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6983), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6982), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6985) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5097), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5098) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5101), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5136), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5138) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5140), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5141) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5143), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5144) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5147), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5148) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5150), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5151) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5154), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5155) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5157), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5158) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5161), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5161) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5164), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5165) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5167), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5168) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5170), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5171) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5174), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5175) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5177), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5178) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5180), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5181) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5183), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5184) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5187), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5188) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 19,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5190), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5191) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 20,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5193), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5194) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 21,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5197), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5197) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 22,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5201), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5202) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 23,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5204), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5205) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 24,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5207), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5208) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 25,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5211), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5212) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 26,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5214), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5215) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 27,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5217), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5218) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 28,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5220), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5221) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 29,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5223), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5224) });

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: 30,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5226), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5227) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7174), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7176) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7178), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7179) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 8, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7180), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7181) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 9, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7183), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7184) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 14, 1 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7186), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7187) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 3, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7189), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 10, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7193), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7194) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 11, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7204), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7205) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 13, 2 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7206), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7208) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 5, 3 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7212), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7213) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 4, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7195), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7196) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 6, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7198), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7199) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 7, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7201), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new object[] { 12, 4 },
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7209), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7210) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7079), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7080) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7083), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7084) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7086), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7087) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7089), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7090) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7092), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7093) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7096), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7097) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 7,
                columns: new[] { "CreateDate", "MenuName", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7099), "Exam Status", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7106), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7107) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7122), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7123) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7102), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7104) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7110), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7111) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 12,
                columns: new[] { "CreateDate", "MenuName", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7113), "Lecture Management(UnderContrucst)", new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7114) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7116), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7117) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7119), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7270), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7272) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7275), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7276) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7280), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7281) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7284), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7285) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7288), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7289) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ReportId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7291), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7293) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7351), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7352) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7356), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7357) });

            migrationBuilder.UpdateData(
                table: "Semester",
                keyColumn: "SemesterId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7361), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5700), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5702) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5706), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5707) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5710), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5711) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5715), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5716) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5719), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5723), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5723) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5726), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5727) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5730), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5731) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5734), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5735) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5738), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5739) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5741), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5742) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5745), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5746) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5749), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5753), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5754) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5757), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5758) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5761), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5762) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 17,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5765), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5766) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 18,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5769), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 19,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5773), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5774) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 20,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5776), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5777) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 21,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5780), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5781) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 22,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5784), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5785) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 23,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5787), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 24,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5791), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5792) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 25,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5796), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5797) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 26,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5799), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 27,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5803), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5804) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 28,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5806), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5807) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 29,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5810), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5811) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 30,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5814), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5815) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 31,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5817), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5819) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 32,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5822), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5823) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 33,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5826), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5827) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 34,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5829), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 35,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5834), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5836) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 36,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5838), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5839) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 37,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5843), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 38,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5846), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5847) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 39,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5850), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 40,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5853), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 41,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5859), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 42,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5862), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5863) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 43,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5866), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5867) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 44,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5870), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5871) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 45,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5874), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5875) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 46,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5878), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5879) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 47,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5933), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5934) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 48,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5937), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5938) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 49,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5940), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5941) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 50,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5944), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5945) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 51,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5947), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5948) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 52,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5951), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5952) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 53,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5955), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5956) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 54,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5959), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 55,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5962), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5963) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 56,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5967), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5968) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 57,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5972), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5973) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 58,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5978), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5979) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 59,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5982), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5984) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 60,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5986), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5987) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 61,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5990), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5991) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 62,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5994), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5995) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 63,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5998), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5999) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 64,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6002), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6003) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 65,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6006), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6007) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 66,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6012), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6013) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 67,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6018), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6019) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 68,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6022), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6023) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 69,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6026), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6027) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 70,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6030), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6031) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 71,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6034), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6035) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 72,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6037), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6038) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 73,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6041), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6042) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 74,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6044), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6045) });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 75,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6048), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(6049) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5308), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5313), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5314) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5399), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5402), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5403) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "RoleId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5406), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5407) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5477), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5478) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5485), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5486) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5492), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5493) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5502), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5503) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5509), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5510) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5516), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5517) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5523), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5525) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5530), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5531) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5537), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5538) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5543), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5544) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5550), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5551) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5557), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5558) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5564), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5565) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5571), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5572) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5577), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5578) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 16,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5584), new DateTime(2024, 11, 11, 16, 13, 34, 223, DateTimeKind.Local).AddTicks(5585) });

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Campuses",
                table: "CampusUserFaculty",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "CampusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Faculties",
                table: "CampusUserFaculty",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampusUserFaculty_Users",
                table: "CampusUserFaculty",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstFileRepoID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "SecondFileRepoID",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "GameVariants",
                keyColumn: "GameVariantID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("3ed714f1-99d9-4985-af8a-1a562f616148"));

            migrationBuilder.UpdateData(
                table: "ScoreTables",
                keyColumn: "ScoreTableID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("b88f1b44-e9be-4900-ba14-32f425f9b10f"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("595c2ef0-1e75-4a73-b054-64872be7a1a3"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 2,
                column: "DataGuidID",
                value: new Guid("6c3f853c-4cab-4e0d-a33a-2cf93b7a29ba"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 13, 12, 42, 49, 450, DateTimeKind.Local).AddTicks(9336), new Guid("c9cebd53-3809-46ea-802a-ceb30b797ea4") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 13, 12, 42, 49, 454, DateTimeKind.Local).AddTicks(1321), new Guid("5126b943-19d8-48af-a8ff-332b5f1a304d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstFileRepoID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondFileRepoID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GameVariants",
                keyColumn: "GameVariantID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("d3929b90-a6fc-4c64-afd9-27ec58aeb754"));

            migrationBuilder.UpdateData(
                table: "ScoreTables",
                keyColumn: "ScoreTableID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("262bcab6-87e0-4b59-99fd-ac0210ac9d87"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("18e1536f-7802-4f23-a38a-47f39354cce7"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 2,
                column: "DataGuidID",
                value: new Guid("a95081c6-65dc-4712-8484-fc77affa597f"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 12, 19, 24, 5, 647, DateTimeKind.Local).AddTicks(3404), new Guid("d9c0685e-59fb-4d01-ab90-3bd415568fc8") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 12, 19, 24, 5, 650, DateTimeKind.Local).AddTicks(2929), new Guid("efd0175d-24c3-4e52-8b35-ecc1bbacce30") });
        }
    }
}

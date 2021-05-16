using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirstFileRepos_Carts_CartID",
                table: "FirstFileRepos");

            migrationBuilder.UpdateData(
                table: "GameVariants",
                keyColumn: "GameVariantID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("00a7a387-fcf3-4e9c-943f-92964054347e"));

            migrationBuilder.UpdateData(
                table: "ScoreTables",
                keyColumn: "ScoreTableID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("b928a487-8342-4f31-99f6-61c3e48cf595"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 1,
                column: "DataGuidID",
                value: new Guid("1714301c-729e-4607-9054-275b29059a1f"));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 2,
                column: "DataGuidID",
                value: new Guid("89f3966c-d9df-469b-ab52-076e2b282b5d"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 13, 13, 56, 25, 264, DateTimeKind.Local).AddTicks(5447), new Guid("8ab5fb2b-4de0-476b-ae97-6d00462295f1") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DataGuidID" },
                values: new object[] { new DateTime(2021, 5, 13, 13, 56, 25, 266, DateTimeKind.Local).AddTicks(8284), new Guid("efac2860-bba9-4554-99be-7731f1431226") });

            migrationBuilder.AddForeignKey(
                name: "FK_FirstFileRepos_Carts_CartID",
                table: "FirstFileRepos",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirstFileRepos_Carts_CartID",
                table: "FirstFileRepos");

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

            migrationBuilder.AddForeignKey(
                name: "FK_FirstFileRepos_Carts_CartID",
                table: "FirstFileRepos",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID");
        }
    }
}

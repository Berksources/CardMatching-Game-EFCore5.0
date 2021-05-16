using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryIsActive = table.Column<bool>(type: "bit", nullable: false),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "GameVariants",
                columns: table => new
                {
                    GameVariantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameQuestionCount = table.Column<int>(type: "int", nullable: false),
                    GameScoreCount = table.Column<int>(type: "int", nullable: false),
                    GameIsOver = table.Column<bool>(type: "bit", nullable: false),
                    GameIsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVariants", x => x.GameVariantID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    FirstFileRepoID = table.Column<int>(type: "int", nullable: false),
                    SecondFileRepoID = table.Column<int>(type: "int", nullable: false),
                    CartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Carts_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserLastScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    UserToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleID");
                });

            migrationBuilder.CreateTable(
                name: "FirstFileRepos",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePhotoIsDefault = table.Column<bool>(type: "bit", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstFileRepos", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_FirstFileRepos_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID");
                });

            migrationBuilder.CreateTable(
                name: "ScoreTables",
                columns: table => new
                {
                    ScoreTableID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ScoreData = table.Column<int>(type: "int", nullable: false),
                    DataGuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreTables", x => x.ScoreTableID);
                    table.ForeignKey(
                        name: "FK_ScoreTables_Users_ScoreTableID",
                        column: x => x.ScoreTableID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.InsertData(
                table: "GameVariants",
                columns: new[] { "GameVariantID", "CreatedDate", "DataGuidID", "GameIsCompleted", "GameIsOver", "GameQuestionCount", "GameScoreCount", "ModifiedDate" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d3929b90-a6fc-4c64-afd9-27ec58aeb754"), false, false, 0, 0, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleID", "CreatedDate", "DataGuidID", "ModifiedDate", "UserRoleName" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("18e1536f-7802-4f23-a38a-47f39354cce7"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleID", "CreatedDate", "DataGuidID", "ModifiedDate", "UserRoleName" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a95081c6-65dc-4712-8484-fc77affa597f"), null, "Oyuncu" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "DataGuidID", "ModifiedDate", "UserEMail", "UserIsActive", "UserIsEmailConfirmed", "UserLastScore", "UserName", "UserPassword", "UserRoleID", "UserSurname", "UserToken" },
                values: new object[] { 1, new DateTime(2021, 5, 12, 19, 24, 5, 647, DateTimeKind.Local).AddTicks(3404), new Guid("d9c0685e-59fb-4d01-ab90-3bd415568fc8"), null, "AdminPaneli@gmail.com", true, true, null, "Admin", "AdminGitHub", 1, "Admin", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "DataGuidID", "ModifiedDate", "UserEMail", "UserIsActive", "UserIsEmailConfirmed", "UserLastScore", "UserName", "UserPassword", "UserRoleID", "UserSurname", "UserToken" },
                values: new object[] { 2, new DateTime(2021, 5, 12, 19, 24, 5, 650, DateTimeKind.Local).AddTicks(2929), new Guid("efd0175d-24c3-4e52-8b35-ecc1bbacce30"), null, "Kullanici@gmail.com", true, true, null, "Kullanici", "KullaniciGitHub", 2, "Kullanici", null });

            migrationBuilder.InsertData(
                table: "ScoreTables",
                columns: new[] { "ScoreTableID", "CreatedDate", "DataGuidID", "ModifiedDate", "ScoreData", "UserID" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("262bcab6-87e0-4b59-99fd-ac0210ac9d87"), null, 7600, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CategoryID",
                table: "Carts",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FirstFileRepos_CartID",
                table: "FirstFileRepos",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleID",
                table: "Users",
                column: "UserRoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstFileRepos");

            migrationBuilder.DropTable(
                name: "GameVariants");

            migrationBuilder.DropTable(
                name: "ScoreTables");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}

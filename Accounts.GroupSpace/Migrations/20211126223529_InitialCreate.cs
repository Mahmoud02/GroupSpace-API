using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounts.GroupSpace.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Subject = table.Column<string>(type: "text", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "text", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "text", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "text", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityCode = table.Column<string>(type: "text", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityCodeExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpirationDate", "Subject", "Username" },
                values: new object[] { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "1", "7535dd74-0200-4f88-80fa-7f267f0525c4", "test@yahoo.com", "AQAAAAEAACcQAAAAEMLD4V/YzeYdz2zwPWcnC9fByQYR+TArwGQRkkBy43Dk1FAxXrelTJOzpkFYSj6d7Q==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d860efca-22d9-47fd-8249-791ba61b07c7", "Mahmoud" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("a4ffed4d-4d03-4e6b-b76c-54266ff5f6a8"), "d66ef4c4-0c49-4d7a-87f3-36c37fa96429", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

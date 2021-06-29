using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupSpace.DAL.Migrations
{
    public partial class RoleTypeOfGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleTypesGroup",
                columns: table => new
                {
                    RoleTypeGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypesGroup", x => x.RoleTypeGroupId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "RoleTypesGroup",
                columns: new[] { "RoleTypeGroupId", "Text" },
                values: new object[] { 1, "User" });

            migrationBuilder.InsertData(
                table: "RoleTypesGroup",
                columns: new[] { "RoleTypeGroupId", "Text" },
                values: new object[] { 2, "Moderator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleTypesGroup");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupSpace.DAL.Migrations
{
    public partial class groupType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTypes",
                columns: table => new
                {
                    GroupTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTypes", x => x.GroupTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "GroupTypes",
                columns: new[] { "GroupTypeId", "Text" },
                values: new object[,]
                {
                    { 1, "Humour" },
                    { 2, "Science" },
                    { 3, "Travel" },
                    { 4, "Buy & sell" },
                    { 5, "Business" },
                    { 6, "Style" },
                    { 7, "Animals" },
                    { 8, "Sport" },
                    { 9, "fitness" },
                    { 10, "Education" },
                    { 11, "Arts" },
                    { 12, "Entertainment" },
                    { 13, "Food & drink" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupTypes",
                principalColumn: "GroupTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupTypes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupTypeId",
                table: "Groups");
        }
    }
}

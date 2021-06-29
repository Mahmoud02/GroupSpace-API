using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupSpace.DAL.Migrations
{
    public partial class ReportPostUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportPost_Groups_GroupId",
                table: "ReportPost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportPost_Posts_PostId",
                table: "ReportPost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportPost_Users_UserId",
                table: "ReportPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportPost",
                table: "ReportPost");

            migrationBuilder.RenameTable(
                name: "ReportPost",
                newName: "ReportPosts");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPost_UserId",
                table: "ReportPosts",
                newName: "IX_ReportPosts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPost_PostId",
                table: "ReportPosts",
                newName: "IX_ReportPosts_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPost_GroupId",
                table: "ReportPosts",
                newName: "IX_ReportPosts_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "NumOfTimes",
                table: "ReportPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportPosts",
                table: "ReportPosts",
                column: "ReportPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPosts_Groups_GroupId",
                table: "ReportPosts",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPosts_Posts_PostId",
                table: "ReportPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPosts_Users_UserId",
                table: "ReportPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportPosts_Groups_GroupId",
                table: "ReportPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportPosts_Posts_PostId",
                table: "ReportPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportPosts_Users_UserId",
                table: "ReportPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportPosts",
                table: "ReportPosts");

            migrationBuilder.DropColumn(
                name: "NumOfTimes",
                table: "ReportPosts");

            migrationBuilder.RenameTable(
                name: "ReportPosts",
                newName: "ReportPost");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPosts_UserId",
                table: "ReportPost",
                newName: "IX_ReportPost_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPosts_PostId",
                table: "ReportPost",
                newName: "IX_ReportPost_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportPosts_GroupId",
                table: "ReportPost",
                newName: "IX_ReportPost_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportPost",
                table: "ReportPost",
                column: "ReportPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPost_Groups_GroupId",
                table: "ReportPost",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPost_Posts_PostId",
                table: "ReportPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPost_Users_UserId",
                table: "ReportPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

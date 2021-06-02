using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class PhotoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_User_UserModelId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_User_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_User_AppUserId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserModelId",
                table: "Photos",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_User_UserModelId",
                table: "Photos",
                column: "UserModelId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatorId",
                table: "Documents",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_CreatorId",
                table: "Documents",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_CreatorId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_CreatorId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Documents");
        }
    }
}

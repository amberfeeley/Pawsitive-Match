using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawsitiveMatch.Migrations
{
    /// <inheritdoc />
    public partial class AddPetsToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InCartOfUserId",
                table: "Pet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_InCartOfUserId",
                table: "Pet",
                column: "InCartOfUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_User_InCartOfUserId",
                table: "Pet",
                column: "InCartOfUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_User_InCartOfUserId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_InCartOfUserId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "InCartOfUserId",
                table: "Pet");
        }
    }
}

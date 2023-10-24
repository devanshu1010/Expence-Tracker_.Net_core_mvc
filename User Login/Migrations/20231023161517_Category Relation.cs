using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Login.Migrations
{
    public partial class CategoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_User_Id",
                table: "Categories",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_User_Id",
                table: "Categories",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onUpdate: ReferentialAction.NoAction,
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_User_Id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_User_Id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Categories");
        }
    }
}

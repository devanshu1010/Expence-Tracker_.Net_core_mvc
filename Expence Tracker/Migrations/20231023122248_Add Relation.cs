using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Login.Migrations
{
    public partial class AddRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_User_Id",
                table: "Transactions",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_User_Id",
                table: "Transactions",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_User_Id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_User_Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

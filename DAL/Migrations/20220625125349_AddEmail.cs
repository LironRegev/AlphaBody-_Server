using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class AddEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.Sql("UPDATE users SET email='liron@gmail.com' WHERE username = 'lironregevuser'");
            migrationBuilder.Sql("UPDATE users SET email='dor@gmail.com' WHERE username = 'dorbs'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }
    }
}

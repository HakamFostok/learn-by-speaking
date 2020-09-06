using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnBySpeaking.Infra.Data.Migrations
{
    public partial class x4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Question",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Question");
        }
    }
}

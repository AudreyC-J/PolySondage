using Microsoft.EntityFrameworkCore.Migrations;

namespace PolySondage.Data.Migrations
{
    public partial class changeNameChoiceTotalVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Choices",
                newName: "TotalVotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalVotes",
                table: "Choices",
                newName: "Vote");
        }
    }
}

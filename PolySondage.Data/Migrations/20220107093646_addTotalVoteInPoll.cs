using Microsoft.EntityFrameworkCore.Migrations;

namespace PolySondage.Data.Migrations
{
    public partial class addTotalVoteInPoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberTotalVote",
                table: "Polls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberTotalVote",
                table: "Polls");
        }
    }
}

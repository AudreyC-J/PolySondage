using Microsoft.EntityFrameworkCore.Migrations;

namespace PolySondage.Data.Migrations
{
    public partial class tableRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Polls_PollIdPoll",
                table: "Choices");

            migrationBuilder.AlterColumn<int>(
                name: "PollIdPoll",
                table: "Choices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Choices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_IdUser",
                table: "Votes",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Polls_PollIdPoll",
                table: "Choices",
                column: "PollIdPoll",
                principalTable: "Polls",
                principalColumn: "IdPoll",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Polls_IdPoll",
                table: "Votes",
                column: "IdPoll",
                principalTable: "Polls",
                principalColumn: "IdPoll",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_IdUser",
                table: "Votes",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Polls_PollIdPoll",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Polls_IdPoll",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_IdUser",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_IdUser",
                table: "Votes");

            migrationBuilder.AlterColumn<int>(
                name: "PollIdPoll",
                table: "Choices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "Choices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Polls_PollIdPoll",
                table: "Choices",
                column: "PollIdPoll",
                principalTable: "Polls",
                principalColumn: "IdPoll",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

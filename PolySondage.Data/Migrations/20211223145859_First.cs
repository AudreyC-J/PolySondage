using Microsoft.EntityFrameworkCore.Migrations;

namespace PolySondage.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    IdPoll = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => new { x.IdPoll, x.IdUser });
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    IdPoll = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    CreatorIdUser = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unique = table.Column<bool>(type: "bit", nullable: false),
                    Activate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.IdPoll);
                    table.ForeignKey(
                        name: "FK_Polls_Users_CreatorIdUser",
                        column: x => x.CreatorIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    IdChoice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPoll = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    PollIdPoll = table.Column<int>(type: "int", nullable: true),
                    VoteIdPoll = table.Column<int>(type: "int", nullable: true),
                    VoteIdUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.IdChoice);
                    table.ForeignKey(
                        name: "FK_Choices_Polls_PollIdPoll",
                        column: x => x.PollIdPoll,
                        principalTable: "Polls",
                        principalColumn: "IdPoll",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Choices_Votes_VoteIdPoll_VoteIdUser",
                        columns: x => new { x.VoteIdPoll, x.VoteIdUser },
                        principalTable: "Votes",
                        principalColumns: new[] { "IdPoll", "IdUser" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_PollIdPoll",
                table: "Choices",
                column: "PollIdPoll");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_VoteIdPoll_VoteIdUser",
                table: "Choices",
                columns: new[] { "VoteIdPoll", "VoteIdUser" });

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatorIdUser",
                table: "Polls",
                column: "CreatorIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PolySondage.Data.Migrations
{
    public partial class changementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    IdPoll = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorIdUser = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    IdVote = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollIdPoll = table.Column<int>(type: "int", nullable: true),
                    UserIdUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.IdVote);
                    table.ForeignKey(
                        name: "FK_Votes_Polls_PollIdPoll",
                        column: x => x.PollIdPoll,
                        principalTable: "Polls",
                        principalColumn: "IdPoll",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserIdUser",
                        column: x => x.UserIdUser,
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
                    PollIdPoll = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    VoteIdVote = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.IdChoice);
                    table.ForeignKey(
                        name: "FK_Choices_Polls_PollIdPoll",
                        column: x => x.PollIdPoll,
                        principalTable: "Polls",
                        principalColumn: "IdPoll",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Choices_Votes_VoteIdVote",
                        column: x => x.VoteIdVote,
                        principalTable: "Votes",
                        principalColumn: "IdVote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_PollIdPoll",
                table: "Choices",
                column: "PollIdPoll");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_VoteIdVote",
                table: "Choices",
                column: "VoteIdVote");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatorIdUser",
                table: "Polls",
                column: "CreatorIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PollIdPoll",
                table: "Votes",
                column: "PollIdPoll");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserIdUser",
                table: "Votes",
                column: "UserIdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaguesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Seasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "MatchParticipations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_LeagueId",
                table: "Seasons",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchParticipations_TeamId",
                table: "MatchParticipations",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchParticipations_Teams_TeamId",
                table: "MatchParticipations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchParticipations_Teams_TeamId",
                table: "MatchParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Leagues_LeagueId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_LeagueId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_MatchParticipations_TeamId",
                table: "MatchParticipations");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "MatchParticipations");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Seasons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

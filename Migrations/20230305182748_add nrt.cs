using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaKursWork.Migrations
{
    public partial class addnrt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challengers_Challenges_ChallengeId",
                table: "Challengers");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeId",
                table: "Challengers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Challengers_Challenges_ChallengeId",
                table: "Challengers",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challengers_Challenges_ChallengeId",
                table: "Challengers");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeId",
                table: "Challengers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Challengers_Challenges_ChallengeId",
                table: "Challengers",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

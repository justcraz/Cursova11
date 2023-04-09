using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaKursWork.Migrations
{
    public partial class addcontra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contraindications",
                table: "Meds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contraindications",
                table: "Meds");
        }
    }
}

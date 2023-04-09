using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaKursWork.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommodityGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    authenticationKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabratoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryEmployees_Laboratories_LabratoryId",
                        column: x => x.LabratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartExploring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabratoryId = table.Column<int>(type: "int", nullable: false),
                    CommodityGroupId = table.Column<int>(type: "int", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meds_CommodityGroups_CommodityGroupId",
                        column: x => x.CommodityGroupId,
                        principalTable: "CommodityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meds_Laboratories_LabratoryId",
                        column: x => x.LabratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meds_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scientists",
                columns: table => new
                {
                    LaboratoryEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleForDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectionDevelopment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scientists", x => x.LaboratoryEmployeeId);
                    table.ForeignKey(
                        name: "FK_Scientists_LaboratoryEmployees_LaboratoryEmployeeId",
                        column: x => x.LaboratoryEmployeeId,
                        principalTable: "LaboratoryEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechStaffs",
                columns: table => new
                {
                    LaboratoryEmployeeId = table.Column<int>(type: "int", nullable: false),
                    MaintainsDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasYourInstruments = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechStaffs", x => x.LaboratoryEmployeeId);
                    table.ForeignKey(
                        name: "FK_TechStaffs_LaboratoryEmployees_LaboratoryEmployeeId",
                        column: x => x.LaboratoryEmployeeId,
                        principalTable: "LaboratoryEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallegesStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScientistId = table.Column<int>(type: "int", nullable: false),
                    MedsId = table.Column<int>(type: "int", nullable: false),
                    TechStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Meds_MedsId",
                        column: x => x.MedsId,
                        principalTable: "Meds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challenges_Scientists_ScientistId",
                        column: x => x.ScientistId,
                        principalTable: "Scientists",
                        principalColumn: "LaboratoryEmployeeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Challenges_TechStaffs_TechStaffId",
                        column: x => x.TechStaffId,
                        principalTable: "TechStaffs",
                        principalColumn: "LaboratoryEmployeeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Challengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraindications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challengers_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challengers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Challengers_ChallengeId",
                table: "Challengers",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_MedsId",
                table: "Challenges",
                column: "MedsId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ScientistId",
                table: "Challenges",
                column: "ScientistId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_TechStaffId",
                table: "Challenges",
                column: "TechStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryEmployees_LabratoryId",
                table: "LaboratoryEmployees",
                column: "LabratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Meds_CommodityGroupId",
                table: "Meds",
                column: "CommodityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Meds_LabratoryId",
                table: "Meds",
                column: "LabratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Meds_UnitMeasureId",
                table: "Meds",
                column: "UnitMeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challengers");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Meds");

            migrationBuilder.DropTable(
                name: "Scientists");

            migrationBuilder.DropTable(
                name: "TechStaffs");

            migrationBuilder.DropTable(
                name: "CommodityGroups");

            migrationBuilder.DropTable(
                name: "UnitMeasures");

            migrationBuilder.DropTable(
                name: "LaboratoryEmployees");

            migrationBuilder.DropTable(
                name: "Laboratories");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twoday_Internship_Task.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enclosures",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosures", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Species = table.Column<string>(type: "TEXT", nullable: false),
                    Food = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    EnclosureName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Species);
                    table.ForeignKey(
                        name: "FK_Animals_Enclosures_EnclosureName",
                        column: x => x.EnclosureName,
                        principalTable: "Enclosures",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnclosureObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    EnclosureName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnclosureObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnclosureObjects_Enclosures_EnclosureName",
                        column: x => x.EnclosureName,
                        principalTable: "Enclosures",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureName",
                table: "Animals",
                column: "EnclosureName");

            migrationBuilder.CreateIndex(
                name: "IX_EnclosureObjects_EnclosureName",
                table: "EnclosureObjects",
                column: "EnclosureName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "EnclosureObjects");

            migrationBuilder.DropTable(
                name: "Enclosures");
        }
    }
}

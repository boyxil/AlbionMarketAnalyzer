using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbionMarket.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniqueName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LocalizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Description_Localizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "Localizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Description_LocalizationId",
                table: "Description",
                column: "LocalizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropTable(
                name: "Localizations");
        }
    }
}

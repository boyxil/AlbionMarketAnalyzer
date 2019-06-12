using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbionMarket.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniqueName = table.Column<string>(nullable: true),
                    ShopCategory = table.Column<string>(nullable: true),
                    ShopSubCategory = table.Column<string>(nullable: true),
                    Kind = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    MaxStackSize = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    UnlockedToCraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    LocalizationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniqueName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.LocalizationId);
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    DescriptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(nullable: true),
                    DescriptionText = table.Column<string>(nullable: true),
                    LocalizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.DescriptionId);
                    table.ForeignKey(
                        name: "FK_Description_Localizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "Localizations",
                        principalColumn: "LocalizationId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Items");

            migrationBuilder.DropTable(
                name: "Localizations");
        }
    }
}

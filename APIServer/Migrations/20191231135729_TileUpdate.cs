using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class TileUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TileProperties");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Catalogs_sql_rowid",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "sql_deleted",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "sql_rowid",
                table: "Catalogs");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Tiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Tiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetURL",
                table: "Tiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "TargetURL",
                table: "Tiles");

            migrationBuilder.AddColumn<string>(
                name: "sql_deleted",
                table: "Catalogs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sql_rowid",
                table: "Catalogs",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Catalogs_sql_rowid",
                table: "Catalogs",
                column: "sql_rowid");

            migrationBuilder.CreateTable(
                name: "TileProperties",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    TileCodigo = table.Column<string>(maxLength: 50, nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    TargetURL = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileProperties", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_TileProperties_Tiles_TileCodigo",
                        column: x => x.TileCodigo,
                        principalTable: "Tiles",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TileProperties_TileCodigo",
                table: "TileProperties",
                column: "TileCodigo");
        }
    }
}

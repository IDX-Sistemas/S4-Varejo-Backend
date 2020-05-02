using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class TileUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tiles_sql_rowid",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "sql_deleted",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "sql_rowid",
                table: "Tiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sql_deleted",
                table: "Tiles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sql_rowid",
                table: "Tiles",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tiles_sql_rowid",
                table: "Tiles",
                column: "sql_rowid");
        }
    }
}

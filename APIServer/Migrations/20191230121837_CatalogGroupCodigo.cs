using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class CatalogGroupCodigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Catalogs_CatalogRowId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Tiles_TileRowId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupItems_Groups_GroupRowId",
                table: "GroupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupItems_Tiles_TileRowId",
                table: "GroupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TileProperties_Tiles_TileRowId",
                table: "TileProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tiles",
                table: "Tiles");

            migrationBuilder.DropIndex(
                name: "IX_TileProperties_TileRowId",
                table: "TileProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupItems_GroupRowId",
                table: "GroupItems");

            migrationBuilder.DropIndex(
                name: "IX_GroupItems_TileRowId",
                table: "GroupItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_CatalogRowId",
                table: "CatalogItems");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_TileRowId",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "TileID",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "TileRowId",
                table: "TileProperties");

            migrationBuilder.DropColumn(
                name: "GroupRowId",
                table: "GroupItems");

            migrationBuilder.DropColumn(
                name: "TileRowId",
                table: "GroupItems");

            migrationBuilder.DropColumn(
                name: "CatalogRowId",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "TileRowId",
                table: "CatalogItems");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Tiles",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TileCodigo",
                table: "TileProperties",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Groups",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroupCodigo",
                table: "GroupItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TileCodigo",
                table: "GroupItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Catalogs",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CatalogCodigo",
                table: "CatalogItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TileCodigo",
                table: "CatalogItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tiles",
                table: "Tiles",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tiles_sql_rowid",
                table: "Tiles",
                column: "sql_rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Groups_sql_rowid",
                table: "Groups",
                column: "sql_rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs",
                column: "Codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Catalogs_sql_rowid",
                table: "Catalogs",
                column: "sql_rowid");

            migrationBuilder.CreateIndex(
                name: "IX_TileProperties_TileCodigo",
                table: "TileProperties",
                column: "TileCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_GroupCodigo",
                table: "GroupItems",
                column: "GroupCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_TileCodigo",
                table: "GroupItems",
                column: "TileCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogCodigo",
                table: "CatalogItems",
                column: "CatalogCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_TileCodigo",
                table: "CatalogItems",
                column: "TileCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Catalogs_CatalogCodigo",
                table: "CatalogItems",
                column: "CatalogCodigo",
                principalTable: "Catalogs",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Tiles_TileCodigo",
                table: "CatalogItems",
                column: "TileCodigo",
                principalTable: "Tiles",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupItems_Groups_GroupCodigo",
                table: "GroupItems",
                column: "GroupCodigo",
                principalTable: "Groups",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupItems_Tiles_TileCodigo",
                table: "GroupItems",
                column: "TileCodigo",
                principalTable: "Tiles",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TileProperties_Tiles_TileCodigo",
                table: "TileProperties",
                column: "TileCodigo",
                principalTable: "Tiles",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Catalogs_CatalogCodigo",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Tiles_TileCodigo",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupItems_Groups_GroupCodigo",
                table: "GroupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupItems_Tiles_TileCodigo",
                table: "GroupItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TileProperties_Tiles_TileCodigo",
                table: "TileProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tiles",
                table: "Tiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tiles_sql_rowid",
                table: "Tiles");

            migrationBuilder.DropIndex(
                name: "IX_TileProperties_TileCodigo",
                table: "TileProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Groups_sql_rowid",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupItems_GroupCodigo",
                table: "GroupItems");

            migrationBuilder.DropIndex(
                name: "IX_GroupItems_TileCodigo",
                table: "GroupItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Catalogs_sql_rowid",
                table: "Catalogs");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_CatalogCodigo",
                table: "CatalogItems");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_TileCodigo",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Tiles");

            migrationBuilder.DropColumn(
                name: "TileCodigo",
                table: "TileProperties");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupCodigo",
                table: "GroupItems");

            migrationBuilder.DropColumn(
                name: "TileCodigo",
                table: "GroupItems");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Catalogs");

            migrationBuilder.DropColumn(
                name: "CatalogCodigo",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "TileCodigo",
                table: "CatalogItems");

            migrationBuilder.AddColumn<string>(
                name: "TileID",
                table: "Tiles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TileRowId",
                table: "TileProperties",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GroupRowId",
                table: "GroupItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TileRowId",
                table: "GroupItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CatalogRowId",
                table: "CatalogItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TileRowId",
                table: "CatalogItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tiles",
                table: "Tiles",
                column: "sql_rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "sql_rowid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs",
                column: "sql_rowid");

            migrationBuilder.CreateIndex(
                name: "IX_TileProperties_TileRowId",
                table: "TileProperties",
                column: "TileRowId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_GroupRowId",
                table: "GroupItems",
                column: "GroupRowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_TileRowId",
                table: "GroupItems",
                column: "TileRowId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogRowId",
                table: "CatalogItems",
                column: "CatalogRowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_TileRowId",
                table: "CatalogItems",
                column: "TileRowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Catalogs_CatalogRowId",
                table: "CatalogItems",
                column: "CatalogRowId",
                principalTable: "Catalogs",
                principalColumn: "sql_rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Tiles_TileRowId",
                table: "CatalogItems",
                column: "TileRowId",
                principalTable: "Tiles",
                principalColumn: "sql_rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupItems_Groups_GroupRowId",
                table: "GroupItems",
                column: "GroupRowId",
                principalTable: "Groups",
                principalColumn: "sql_rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupItems_Tiles_TileRowId",
                table: "GroupItems",
                column: "TileRowId",
                principalTable: "Tiles",
                principalColumn: "sql_rowid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TileProperties_Tiles_TileRowId",
                table: "TileProperties",
                column: "TileRowId",
                principalTable: "Tiles",
                principalColumn: "sql_rowid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

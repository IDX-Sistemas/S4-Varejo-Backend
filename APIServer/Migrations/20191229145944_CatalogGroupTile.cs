using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class CatalogGroupTile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    IsPreset = table.Column<string>(nullable: true),
                    IsVisible = table.Column<string>(nullable: true),
                    IsGroupLocked = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    TileID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    TileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    CatalogRowId = table.Column<long>(nullable: false),
                    TileRowId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Catalogs_CatalogRowId",
                        column: x => x.CatalogRowId,
                        principalTable: "Catalogs",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Tiles_TileRowId",
                        column: x => x.TileRowId,
                        principalTable: "Tiles",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupItems",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    GroupRowId = table.Column<long>(nullable: false),
                    TileRowId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupItems", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_GroupItems_Groups_GroupRowId",
                        column: x => x.GroupRowId,
                        principalTable: "Groups",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupItems_Tiles_TileRowId",
                        column: x => x.TileRowId,
                        principalTable: "Tiles",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TileProperties",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    TargetURL = table.Column<string>(nullable: true),
                    TileRowId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileProperties", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_TileProperties_Tiles_TileRowId",
                        column: x => x.TileRowId,
                        principalTable: "Tiles",
                        principalColumn: "sql_rowid",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_TileProperties_TileRowId",
                table: "TileProperties",
                column: "TileRowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "GroupItems");

            migrationBuilder.DropTable(
                name: "TileProperties");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Tiles");

           
        }
    }
}

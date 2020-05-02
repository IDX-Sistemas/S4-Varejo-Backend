using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class AplicativosModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "GroupItems");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Tiles");

            migrationBuilder.CreateTable(
                name: "Aplicativos",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 200, nullable: false),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Icone = table.Column<string>(nullable: true),
                    TargetURL = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicativos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 50, nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "ModuloAplicativos",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    ModuloCodigo = table.Column<string>(maxLength: 50, nullable: false),
                    AplicativoCodigo = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloAplicativos", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_ModuloAplicativos_Aplicativos_AplicativoCodigo",
                        column: x => x.AplicativoCodigo,
                        principalTable: "Aplicativos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuloAplicativos_Modulos_ModuloCodigo",
                        column: x => x.ModuloCodigo,
                        principalTable: "Modulos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuloAplicativos_AplicativoCodigo",
                table: "ModuloAplicativos",
                column: "AplicativoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloAplicativos_ModuloCodigo",
                table: "ModuloAplicativos",
                column: "ModuloCodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuloAplicativos");

            migrationBuilder.DropTable(
                name: "Aplicativos");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 50, nullable: false),
                    sql_deleted = table.Column<string>(nullable: true),
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsGroupLocked = table.Column<string>(nullable: true),
                    IsPreset = table.Column<string>(nullable: true),
                    IsVisible = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Codigo);
                    table.UniqueConstraint("AK_Groups_sql_rowid", x => x.sql_rowid);
                });

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 50, nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    TargetURL = table.Column<string>(nullable: true),
                    TileType = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatalogCodigo = table.Column<string>(maxLength: 50, nullable: false),
                    sql_deleted = table.Column<string>(nullable: true),
                    TileCodigo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Catalogs_CatalogCodigo",
                        column: x => x.CatalogCodigo,
                        principalTable: "Catalogs",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Tiles_TileCodigo",
                        column: x => x.TileCodigo,
                        principalTable: "Tiles",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupItems",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupCodigo = table.Column<string>(maxLength: 50, nullable: false),
                    sql_deleted = table.Column<string>(nullable: true),
                    TileCodigo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupItems", x => x.sql_rowid);
                    table.ForeignKey(
                        name: "FK_GroupItems_Groups_GroupCodigo",
                        column: x => x.GroupCodigo,
                        principalTable: "Groups",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupItems_Tiles_TileCodigo",
                        column: x => x.TileCodigo,
                        principalTable: "Tiles",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogCodigo",
                table: "CatalogItems",
                column: "CatalogCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_TileCodigo",
                table: "CatalogItems",
                column: "TileCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_GroupCodigo",
                table: "GroupItems",
                column: "GroupCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_GroupItems_TileCodigo",
                table: "GroupItems",
                column: "TileCodigo");
        }
    }
}

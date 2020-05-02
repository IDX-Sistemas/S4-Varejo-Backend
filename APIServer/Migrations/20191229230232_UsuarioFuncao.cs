using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class UsuarioFuncao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioFuncao",
                columns: table => new
                {
                    FuncaoId = table.Column<string>(maxLength: 15, nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioFuncao", x => x.FuncaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_arq_cai_DAT",
                table: "arq_cai",
                column: "DAT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioFuncao");

            migrationBuilder.DropIndex(
                name: "IX_arq_cai_DAT",
                table: "arq_cai");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class UsuarioXFuncao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FUNCAO_ID",
                table: "cad_ace",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cad_ace_FUNCAO_ID",
                table: "cad_ace",
                column: "FUNCAO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_cad_ace_UsuarioFuncao_FUNCAO_ID",
                table: "cad_ace",
                column: "FUNCAO_ID",
                principalTable: "UsuarioFuncao",
                principalColumn: "FuncaoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cad_ace_UsuarioFuncao_FUNCAO_ID",
                table: "cad_ace");

            migrationBuilder.DropIndex(
                name: "IX_cad_ace_FUNCAO_ID",
                table: "cad_ace");

            migrationBuilder.DropColumn(
                name: "FUNCAO_ID",
                table: "cad_ace");
        }
    }
}

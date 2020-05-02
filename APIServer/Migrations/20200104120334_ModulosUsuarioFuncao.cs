using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class ModulosUsuarioFuncao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioFuncaoId",
                table: "Modulos",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_UsuarioFuncaoId",
                table: "Modulos",
                column: "UsuarioFuncaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_UsuarioFuncao_UsuarioFuncaoId",
                table: "Modulos",
                column: "UsuarioFuncaoId",
                principalTable: "UsuarioFuncao",
                principalColumn: "FuncaoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_UsuarioFuncao_UsuarioFuncaoId",
                table: "Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_UsuarioFuncaoId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "UsuarioFuncaoId",
                table: "Modulos");
        }
    }
}

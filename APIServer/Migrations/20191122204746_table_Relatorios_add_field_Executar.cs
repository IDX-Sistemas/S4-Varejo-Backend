using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class table_Relatorios_add_field_Executar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Executar",
                table: "RELATORIOS",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "Executar",
                table: "RELATORIOS");
        }
    }
}

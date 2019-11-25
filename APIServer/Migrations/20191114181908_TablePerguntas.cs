using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class TablePerguntas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERGUNTAS",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false),
                    Componente = table.Column<string>(maxLength: 50, nullable: false),
                    Obrigatorio = table.Column<string>(maxLength: 1, nullable: false),
                    Inativo = table.Column<string>(maxLength: 1, nullable: false),
                    Resposta = table.Column<string>(maxLength: 50, nullable: true),
                    Lista = table.Column<string>(maxLength: 150, nullable: true),
                    ValueHelp = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERGUNTAS", x => x.sql_rowid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PERGUNTAS");
        }
    }
}

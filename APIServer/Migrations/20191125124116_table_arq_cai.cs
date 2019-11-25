using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class table_arq_cai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arq_cai",
                columns: table => new
                {
                    sql_rowid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sql_deleted = table.Column<string>(nullable: true),
                    LOJ = table.Column<string>(nullable: true),
                    DAT = table.Column<DateTime>(nullable: true),
                    VIS = table.Column<double>(nullable: true),
                    ENT = table.Column<double>(nullable: true),
                    CRE = table.Column<double>(nullable: true),
                    PRA = table.Column<double>(nullable: true),
                    CHE = table.Column<double>(nullable: true),
                    ACR = table.Column<double>(nullable: true),
                    DSC = table.Column<double>(nullable: true),
                    JUR = table.Column<double>(nullable: true),
                    DSP = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arq_cai", x => x.sql_rowid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "arq_cai");
        }
    }
}

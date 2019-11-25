using Microsoft.EntityFrameworkCore.Migrations;

namespace IdxSistemas.AppServer.Migrations
{
    public partial class table_con_rec_update_fla_ent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE con_rec SET FLA_ENT='N' WHERE FLA_ENT IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace cloudscribe.Core.Storage.EFCore.PostgreSql.Migrations
{
    public partial class cscore20200512 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''");

            migrationBuilder.CreateIndex(
                name: "ix_cs_user_normalized_email_site_id",
                table: "cs_user",
                columns: new[] { "normalized_email", "site_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cs_user_normalized_user_name_site_id",
                table: "cs_user",
                columns: new[] { "normalized_user_name", "site_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_cs_user_normalized_email_site_id",
                table: "cs_user");

            migrationBuilder.DropIndex(
                name: "ix_cs_user_normalized_user_name_site_id",
                table: "cs_user");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }
    }
}

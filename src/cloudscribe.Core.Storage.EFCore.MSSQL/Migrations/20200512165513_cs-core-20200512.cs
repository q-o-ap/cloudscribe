using Microsoft.EntityFrameworkCore.Migrations;

namespace cloudscribe.Core.Storage.EFCore.MSSQL.Migrations
{
    public partial class cscore20200512 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_cs_User_NormalizedEmail_SiteId",
                table: "cs_User",
                columns: new[] { "NormalizedEmail", "SiteId" },
                unique: true,
                filter: "[NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_cs_User_NormalizedUserName_SiteId",
                table: "cs_User",
                columns: new[] { "NormalizedUserName", "SiteId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cs_User_NormalizedEmail_SiteId",
                table: "cs_User");

            migrationBuilder.DropIndex(
                name: "IX_cs_User_NormalizedUserName_SiteId",
                table: "cs_User");
        }
    }
}

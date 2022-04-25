using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DapperDemo.Migrations
{
    public partial class correctIdforcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Companies",
                newName: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Companies",
                newName: "CompanyID");
        }
    }
}

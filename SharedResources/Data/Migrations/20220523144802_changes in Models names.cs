using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministartionWebsite.Data.Migrations
{
    public partial class changesinModelsnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "CustomerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Customers",
                newName: "Name");
        }
    }
}

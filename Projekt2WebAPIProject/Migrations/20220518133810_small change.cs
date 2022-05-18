using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class smallchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TidsRegistrerings_Projects_ProjectId",
                table: "TidsRegistrerings");

            migrationBuilder.DropColumn(
                name: "ProjektId",
                table: "TidsRegistrerings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TidsRegistrerings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TidsRegistrerings_Projects_ProjectId",
                table: "TidsRegistrerings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TidsRegistrerings_Projects_ProjectId",
                table: "TidsRegistrerings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TidsRegistrerings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjektId",
                table: "TidsRegistrerings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TidsRegistrerings_Projects_ProjectId",
                table: "TidsRegistrerings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }
    }
}

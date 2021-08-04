using Microsoft.EntityFrameworkCore.Migrations;

namespace Waffle.Inc.Migrations
{
    public partial class UniqueNumeroChapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumeroChapa",
                table: "Funcionarios",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_NumeroChapa",
                table: "Funcionarios",
                column: "NumeroChapa",
                unique: true,
                filter: "[NumeroChapa] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_NumeroChapa",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroChapa",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}

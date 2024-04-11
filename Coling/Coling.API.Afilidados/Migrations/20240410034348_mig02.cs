using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coling.API.Afilidados.Migrations
{
    /// <inheritdoc />
    public partial class mig02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Idprofesion",
                table: "ProfesionAfiliados",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Idprofesion",
                table: "ProfesionAfiliados",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

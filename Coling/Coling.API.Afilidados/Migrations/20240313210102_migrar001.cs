using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coling.API.Afilidados.Migrations
{
    /// <inheritdoc />
    public partial class migrar001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSocials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSocials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Afiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPERSONA = table.Column<int>(type: "int", nullable: false),
                    fechaafiliacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoAfiliado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nrotituloprovisional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Afiliados_Personas_IDPERSONA",
                        column: x => x.IDPERSONA,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direccions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPERSONA = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direccions_Personas_IDPERSONA",
                        column: x => x.IDPERSONA,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPERSONA = table.Column<int>(type: "int", nullable: false),
                    nrotelefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Personas_IDPERSONA",
                        column: x => x.IDPERSONA,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaTipoSocials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDTIPOSOCIAL = table.Column<int>(type: "int", nullable: false),
                    IDPERSONA = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaTipoSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonaTipoSocials_Personas_IDPERSONA",
                        column: x => x.IDPERSONA,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaTipoSocials_TipoSocials_IDTIPOSOCIAL",
                        column: x => x.IDTIPOSOCIAL,
                        principalTable: "TipoSocials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesionAfiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDAFILIADO = table.Column<int>(type: "int", nullable: false),
                    Idprofesion = table.Column<int>(type: "int", nullable: false),
                    fechaasignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nrosellosib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionAfiliados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesionAfiliados_Afiliados_IDAFILIADO",
                        column: x => x.IDAFILIADO,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afiliados_IDPERSONA",
                table: "Afiliados",
                column: "IDPERSONA");

            migrationBuilder.CreateIndex(
                name: "IX_Direccions_IDPERSONA",
                table: "Direccions",
                column: "IDPERSONA");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTipoSocials_IDPERSONA",
                table: "PersonaTipoSocials",
                column: "IDPERSONA");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTipoSocials_IDTIPOSOCIAL",
                table: "PersonaTipoSocials",
                column: "IDTIPOSOCIAL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionAfiliados_IDAFILIADO",
                table: "ProfesionAfiliados",
                column: "IDAFILIADO");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_IDPERSONA",
                table: "Telefonos",
                column: "IDPERSONA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direccions");

            migrationBuilder.DropTable(
                name: "PersonaTipoSocials");

            migrationBuilder.DropTable(
                name: "ProfesionAfiliados");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "TipoSocials");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}

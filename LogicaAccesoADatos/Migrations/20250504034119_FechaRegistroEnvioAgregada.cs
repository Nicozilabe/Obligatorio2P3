using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class FechaRegistroEnvioAgregada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seguimiento",
                table: "Envios");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistroEnvio",
                table: "Envios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ComentarioEnvio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    EnvioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioEnvio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioEnvio_Envios_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "Envios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComentarioEnvio_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioEnvio_EmpleadoId",
                table: "ComentarioEnvio",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioEnvio_EnvioId",
                table: "ComentarioEnvio",
                column: "EnvioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioEnvio");

            migrationBuilder.DropColumn(
                name: "FechaRegistroEnvio",
                table: "Envios");

            migrationBuilder.AddColumn<int>(
                name: "Seguimiento",
                table: "Envios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class ClienteEntidadEnvio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Envios");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Envios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Envios_ClienteID",
                table: "Envios",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Usuarios_ClienteID",
                table: "Envios",
                column: "ClienteID",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Usuarios_ClienteID",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_ClienteID",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Envios");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Envios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

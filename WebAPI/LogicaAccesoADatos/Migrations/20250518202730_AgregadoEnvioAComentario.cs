using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoEnvioAComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Envios_EnvioId",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "EnvioId",
                table: "Comentarios",
                newName: "EnvioID");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_EnvioId",
                table: "Comentarios",
                newName: "IX_Comentarios_EnvioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Envios_EnvioID",
                table: "Comentarios",
                column: "EnvioID",
                principalTable: "Envios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Envios_EnvioID",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "EnvioID",
                table: "Comentarios",
                newName: "EnvioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_EnvioID",
                table: "Comentarios",
                newName: "IX_Comentarios_EnvioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Envios_EnvioId",
                table: "Comentarios",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id");
        }
    }
}

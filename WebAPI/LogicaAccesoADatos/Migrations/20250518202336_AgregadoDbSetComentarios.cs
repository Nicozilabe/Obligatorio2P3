using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoDbSetComentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComentarioEnvio_Envios_EnvioId",
                table: "ComentarioEnvio");

            migrationBuilder.DropForeignKey(
                name: "FK_ComentarioEnvio_Usuarios_EmpleadoId",
                table: "ComentarioEnvio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComentarioEnvio",
                table: "ComentarioEnvio");

            migrationBuilder.RenameTable(
                name: "ComentarioEnvio",
                newName: "Comentarios");

            migrationBuilder.RenameIndex(
                name: "IX_ComentarioEnvio_EnvioId",
                table: "Comentarios",
                newName: "IX_Comentarios_EnvioId");

            migrationBuilder.RenameIndex(
                name: "IX_ComentarioEnvio_EmpleadoId",
                table: "Comentarios",
                newName: "IX_Comentarios_EmpleadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Envios_EnvioId",
                table: "Comentarios",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_EmpleadoId",
                table: "Comentarios",
                column: "EmpleadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Envios_EnvioId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_EmpleadoId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.RenameTable(
                name: "Comentarios",
                newName: "ComentarioEnvio");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_EnvioId",
                table: "ComentarioEnvio",
                newName: "IX_ComentarioEnvio_EnvioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_EmpleadoId",
                table: "ComentarioEnvio",
                newName: "IX_ComentarioEnvio_EmpleadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComentarioEnvio",
                table: "ComentarioEnvio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComentarioEnvio_Envios_EnvioId",
                table: "ComentarioEnvio",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComentarioEnvio_Usuarios_EmpleadoId",
                table: "ComentarioEnvio",
                column: "EmpleadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}

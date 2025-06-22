using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class modificadoDeleteBehaviorAccion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Usuarios_AfectadoId",
                table: "Acciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Usuarios_AfectadoId",
                table: "Acciones",
                column: "AfectadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acciones_Usuarios_AfectadoId",
                table: "Acciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Acciones_Usuarios_AfectadoId",
                table: "Acciones",
                column: "AfectadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

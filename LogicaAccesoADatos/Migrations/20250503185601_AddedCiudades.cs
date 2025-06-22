using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class AddedCiudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion_Ciudad_Nombre",
                table: "Agencias");

            migrationBuilder.AddColumn<int>(
                name: "Direccion_CiudadId",
                table: "Agencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencias_Direccion_CiudadId",
                table: "Agencias",
                column: "Direccion_CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencias_Ciudades_Direccion_CiudadId",
                table: "Agencias",
                column: "Direccion_CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencias_Ciudades_Direccion_CiudadId",
                table: "Agencias");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Agencias_Direccion_CiudadId",
                table: "Agencias");

            migrationBuilder.DropColumn(
                name: "Direccion_CiudadId",
                table: "Agencias");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Ciudad_Nombre",
                table: "Agencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

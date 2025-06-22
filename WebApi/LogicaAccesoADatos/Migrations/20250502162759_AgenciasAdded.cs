using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class AgenciasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Numero = table.Column<int>(type: "int", nullable: false),
                    Direccion_Ciudad_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_CodigoPostal = table.Column<int>(type: "int", nullable: false),
                    Ubicacion_Latitud = table.Column<double>(type: "float", nullable: false),
                    Ubicacion_Longitud = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencias");
        }
    }
}

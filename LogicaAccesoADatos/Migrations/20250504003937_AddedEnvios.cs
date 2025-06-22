using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoADatos.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnvios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencias_Ciudades_Direccion_CiudadId",
                table: "Agencias");

            migrationBuilder.RenameColumn(
                name: "Direccion_CiudadId",
                table: "Agencias",
                newName: "CiudadId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencias_Direccion_CiudadId",
                table: "Agencias",
                newName: "IX_Agencias_CiudadId");

            migrationBuilder.AlterColumn<int>(
                name: "CiudadId",
                table: "Agencias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tracking = table.Column<int>(type: "int", nullable: false),
                    EmpleadoResponableId = table.Column<int>(type: "int", nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    EstadoEnvio = table.Column<int>(type: "int", nullable: false),
                    Seguimiento = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AgenciaId = table.Column<int>(type: "int", nullable: true),
                    CiudadId = table.Column<int>(type: "int", nullable: true),
                    Direccion_Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion_Numero = table.Column<int>(type: "int", nullable: true),
                    Direccion_CodigoPostal = table.Column<int>(type: "int", nullable: true),
                    EnvioEficiente = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envios_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envios_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envios_Usuarios_EmpleadoResponableId",
                        column: x => x.EmpleadoResponableId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Envios_AgenciaId",
                table: "Envios",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_CiudadId",
                table: "Envios",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_EmpleadoResponableId",
                table: "Envios",
                column: "EmpleadoResponableId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_Tracking",
                table: "Envios",
                column: "Tracking",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencias_Ciudades_CiudadId",
                table: "Agencias",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencias_Ciudades_CiudadId",
                table: "Agencias");

            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.RenameColumn(
                name: "CiudadId",
                table: "Agencias",
                newName: "Direccion_CiudadId");

            migrationBuilder.RenameIndex(
                name: "IX_Agencias_CiudadId",
                table: "Agencias",
                newName: "IX_Agencias_Direccion_CiudadId");

            migrationBuilder.AlterColumn<int>(
                name: "Direccion_CiudadId",
                table: "Agencias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agencias_Ciudades_Direccion_CiudadId",
                table: "Agencias",
                column: "Direccion_CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

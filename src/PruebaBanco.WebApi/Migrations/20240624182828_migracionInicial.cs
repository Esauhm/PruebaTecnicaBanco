using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaBanco.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class migracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTitular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTarjeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoOtorgado = table.Column<double>(type: "float", nullable: true),
                    PorcentajeInteres = table.Column<double>(type: "float", nullable: true),
                    PorcentajeInteresMinimo = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesTarjeta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaTransaccion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    TipoTransaccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTarjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesTarjeta", x => x.id);
                    table.ForeignKey(
                        name: "FK_TransaccionesTarjeta_Tarjetas_IdTarjeta",
                        column: x => x.IdTarjeta,
                        principalTable: "Tarjetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesTarjeta_IdTarjeta",
                table: "TransaccionesTarjeta",
                column: "IdTarjeta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransaccionesTarjeta");

            migrationBuilder.DropTable(
                name: "Tarjetas");
        }
    }
}

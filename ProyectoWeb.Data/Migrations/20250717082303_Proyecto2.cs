using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Proyecto2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Proyecto",
                type: "nvarchar(2500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaPalabra",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTexto",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiniDescripcion",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Portada",
                table: "Proyecto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Programacion",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Secciones",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tecnologias",
                table: "Proyecto",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Proyecto",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WebActivo",
                table: "Proyecto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "MetaPalabra",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "MetaTexto",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "MiniDescripcion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Portada",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Programacion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Secciones",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Tecnologias",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "WebActivo",
                table: "Proyecto");
        }
    }
}
